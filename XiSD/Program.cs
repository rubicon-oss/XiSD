// This file is part of the MixinXRef project
// Copyright (c) rubicon IT GmbH, www.rubicon.eu
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.CSharp;

namespace XiSD
{
  public static class Program
  {
    private const string DefaultConfigPath = "config.xisd";
    
    private static readonly Dictionary<string, string> SchemaDictionary = new Dictionary<string, string>();

    public static int Main(string[] args)
    {
      var configFile = args.FirstOrDefault();
      if (configFile == null)
      {
        ShowWarning ("No config file specified. Using '" + DefaultConfigPath + "' as default");
        configFile = DefaultConfigPath;
      }

      if (!File.Exists (configFile))
      {
        ShowWarning("Invalid config file specified. Using '" + DefaultConfigPath +"' as default");
        configFile = DefaultConfigPath;
      }

      if (!File.Exists (configFile))
      {
        ShowError("Config file could not be loaded.");
        return -1;
      }

      var config = GenerateConfig.LoadFromFile(configFile);

      IList<SchemaDefinition> schemaDefinitions = new List<SchemaDefinition>();
      var index = 1;
      foreach (var schema in config.Schemas)
      {
        var path = schema.SourcePath + "." + config.baseExtension;
        if (config.basePath != null)
          path = Path.Combine(config.basePath, path);

        if (!File.Exists(path))
        {
          ShowError(string.Format("Invalid schema path on position {0}: The given schema file '{1}' could not be found!", index, path));
          return -1;
        }

        SchemaDictionary.Add(Path.GetFullPath(path), schema.TargetNamespace);
        schemaDefinitions.Add (new SchemaDefinition (path, schema.TargetNamespace, schema.TargetXmlNamespace));

        index++;
      }

      ProcessSchemas(schemaDefinitions, config.includeDataContractAttributes);

      return 0;
    }

    private static void ShowWarning(string message)
    {
      Console.Write("XiSD WARNING: ");
      Console.WriteLine(message);
    }

    private static void ShowError(string message)
    {
      Console.Error.Write("XiSD ERROR:   ");
      Console.Error.WriteLine(message);
    }

    private static void ProcessSchemas(IEnumerable<SchemaDefinition> schemas, bool includeDataContractAttributes)
    {
      CodeDomProvider provider = new CSharpCodeProvider();

      foreach (var schema in schemas)
      {
        var ns = ProcessSchema(schema, includeDataContractAttributes);

        using (var sw = new StreamWriter(schema.OutputPath, false))
          provider.GenerateCodeFromNamespace(ns, sw, new CodeGeneratorOptions());
      }
    }

    private static CodeNamespace ProcessSchema(SchemaDefinition schemaDefinition, bool includeDataContractAttributes)
    {
      var ns = new CodeNamespace(schemaDefinition.Namespace);

      var reader = XmlReader.Create(schemaDefinition.SchemaPath);
      var xsd = XmlSchema.Read(reader, Validate);

      var schemas = new XmlSchemas();

      var schemaSet = new XmlSchemaSet();
      schemaSet.Add(xsd);
      schemaSet.Compile();

      foreach (XmlSchema schema in schemaSet.Schemas())
        schemas.Add(schema);

      const CodeGenerationOptions generationOptions = CodeGenerationOptions.GenerateOrder;
      var exporter = new XmlCodeExporter(ns);
      var importer = new XmlSchemaImporter(schemas, generationOptions, new ImportContext(new CodeIdentifiers(), false));

      foreach (var mapping in
        xsd.Items.OfType<XmlSchemaType>().Select(item => importer.ImportSchemaType(item.QualifiedName)))
      {
        exporter.ExportTypeMapping(mapping);
      }

      var includes = Helper.GetSchemaIncludes(schemaSet);

      FilterIncludedTypes(ns, xsd);
      AddIncludeImports(ns, includes);
      RemoveXmlRootAttributeForNoneRootTypes(ns, schemaSet);
      if (includeDataContractAttributes)
        AddDataContractAttributes(ns, schemaDefinition.XmlNamespace);
      
      return ns;
    }

    private static void AddDataContractAttributes(CodeNamespace ns, string xmlNamespace)
    {
      foreach (var type in ns.Types.OfType<CodeTypeDeclaration> ())
      {
        if(xmlNamespace != null)
        {
          type.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.Serialization.DataContract",
                                                                 new CodeAttributeArgument("Namespace",
                                                                                           new CodePrimitiveExpression(
                                                                                             xmlNamespace))));
        }
        else
        {
          type.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.Serialization.DataContract"));
        }

        foreach (CodeTypeMember member in type.Members)
        {
          var memberAttribute =
            member.CustomAttributes.OfType<CodeAttributeDeclaration>().FirstOrDefault(
              a =>
              a.Name == "System.Xml.Serialization.XmlElementAttribute" ||
              a.Name == "System.Xml.Serialization.XmlAttributeAttribute" ||
              a.Name == "System.Xml.Serialization.XmlArrayAttribute");
          if (memberAttribute != null)
          {
            var orderArgument =
              memberAttribute.Arguments.OfType<CodeAttributeArgument>().SingleOrDefault(a => a.Name == "Order");

            if (orderArgument != null)
              member.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.Serialization.DataMember", orderArgument));
            else
              member.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.Serialization.DataMember"));
          }

          foreach (var elementAttribute in member.CustomAttributes.OfType<CodeAttributeDeclaration> ().Where (a => a.Name == "System.Xml.Serialization.XmlElementAttribute"))
          {
            var typeOfArguments =
              elementAttribute.Arguments.OfType<CodeAttributeArgument>().Where(a => a.Value is CodeTypeOfExpression).
                Select(a => (CodeTypeOfExpression) a.Value);

            foreach (var typeOfArgument in typeOfArguments)
              type.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.Serialization.KnownType",
                                                                     new CodeAttributeArgument(
                                                                       new CodeTypeOfExpression(typeOfArgument.Type))));
          }

          if (type.IsEnum)
            member.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.Serialization.EnumMember"));
        }
      }
    }

    private static void RemoveXmlRootAttributeForNoneRootTypes(CodeNamespace ns, XmlSchemaSet schemaSet)
    {
      var elementTypes =
        new HashSet<string>(schemaSet.GlobalElements.Values.OfType<XmlSchemaElement>().Select(e => e.Name));

      foreach(var type in ns.Types.OfType<CodeTypeDeclaration>())
        if (!elementTypes.Contains (type.Name))
        {
          var attributesToRemove =
            type.CustomAttributes.OfType<CodeAttributeDeclaration> ().Where (a => a.Name == "System.Xml.Serialization.XmlRootAttribute").ToArray ();
          foreach (var attributeToRemove in attributesToRemove)
            type.CustomAttributes.Remove(attributeToRemove);
        }
    }

    private static void FilterIncludedTypes(CodeNamespace ns, XmlSchema schema)
    {
      var types = ns.Types.OfType<CodeTypeDeclaration>().ToArray();

      foreach (var type in types)
      {
        if (!ContainsTypeName(schema, type))
          ns.Types.Remove(type);
      }
    }

    private static bool ContainsTypeName (XmlSchema schema, CodeTypeDeclaration type)
    {
      foreach (var item in schema.Items)
        if ((item is XmlSchemaType && ((XmlSchemaType) item).Name == type.Name) ||
            (item is XmlSchemaElement && ((XmlSchemaElement) item).Name == type.Name))
          return true;

      return false;
    }

    private static void AddIncludeImports(CodeNamespace ns, IEnumerable<XmlSchemaInclude> includes)
    {
      foreach (var includeNamespace in
        includes.Select(include => include.Schema.SourceUri.Replace("file:///", "").Replace('/', '\\')).Select(
          includedFile => SchemaDictionary[includedFile]).Where(includeNamespace => includeNamespace != ns.Name))
      {
        ns.Imports.Add(new CodeNamespaceImport(includeNamespace));
      }
    }

    private static void Validate(object sender, ValidationEventArgs e)
    {
    }
  }
}