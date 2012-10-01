



// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.5.0.40152
//    <NameSpace>XiSD</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><PascalCase>False</PascalCase><BaseClassName>EntityBase</BaseClassName><IncludeSerializeMethod>True</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><OrderXMLAttrib>False</OrderXMLAttrib><EnableEncoding>False</EnableEncoding><AutomaticProperties>True</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><InitializeFields>All</InitializeFields><GenerateAllTypes>True</GenerateAllTypes>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace XiSD
{
  using System;
  using System.Diagnostics;
  using System.Xml.Serialization;
  using System.Collections;
  using System.Xml.Schema;
  using System.ComponentModel;
  using System.IO;
  using System.Text;
  using System.Collections.Generic;


  [System.CodeDom.Compiler.GeneratedCodeAttribute ("System.Xml", "4.0.30319.17626")]
  [System.SerializableAttribute ()]
  [System.Diagnostics.DebuggerStepThroughAttribute ()]
  [System.ComponentModel.DesignerCategoryAttribute ("code")]
  [System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://xisd.at/GenerateConfig.xsd")]
  [System.Xml.Serialization.XmlRootAttribute ("XiSDGenerateConfig", Namespace = "http://xisd.at/GenerateConfig.xsd", IsNullable = false)]
  public partial class GenerateConfig
  {

    private List<Schema> schemasField;

    private static System.Xml.Serialization.XmlSerializer serializer;

    [System.Xml.Serialization.XmlAttributeAttribute ()]
    public string basePath { get; set; }
    [System.Xml.Serialization.XmlAttributeAttribute ()]
    [System.ComponentModel.DefaultValueAttribute ("xsd")]
    public string baseExtension { get; set; }
    [System.Xml.Serialization.XmlAttributeAttribute ()]
    [System.ComponentModel.DefaultValueAttribute (".generated.cs")]
    public string generatedExtension { get; set; }
    [System.Xml.Serialization.XmlAttributeAttribute ()]
    [System.ComponentModel.DefaultValueAttribute (false)]
    public bool includeDataContractAttributes { get; set; }

    public GenerateConfig ()
    {
      this.schemasField = new List<Schema> ();
      this.baseExtension = "xsd";
      this.generatedExtension = ".generated.cs";
      this.includeDataContractAttributes = false;
    }

    [System.Xml.Serialization.XmlArrayItemAttribute (IsNullable = false)]
    public List<Schema> Schemas
    {
      get
      {
        return this.schemasField;
      }
    }

    private static System.Xml.Serialization.XmlSerializer Serializer
    {
      get
      {
        if ((serializer == null))
        {
          serializer = new System.Xml.Serialization.XmlSerializer (typeof (GenerateConfig));
        }
        return serializer;
      }
    }

    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current GenerateConfig object into an XML document
    /// </summary>
    /// <returns>string XML value</returns>
    public virtual string Serialize ()
    {
      System.IO.StreamReader streamReader = null;
      System.IO.MemoryStream memoryStream = null;
      try
      {
        memoryStream = new System.IO.MemoryStream ();
        Serializer.Serialize (memoryStream, this);
        memoryStream.Seek (0, System.IO.SeekOrigin.Begin);
        streamReader = new System.IO.StreamReader (memoryStream);
        return streamReader.ReadToEnd ();
      }
      finally
      {
        if ((streamReader != null))
        {
          streamReader.Dispose ();
        }
        if ((memoryStream != null))
        {
          memoryStream.Dispose ();
        }
      }
    }

    /// <summary>
    /// Deserializes workflow markup into an GenerateConfig object
    /// </summary>
    /// <param name="xml">string workflow markup to deserialize</param>
    /// <param name="obj">Output GenerateConfig object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize (string xml, out GenerateConfig obj, out System.Exception exception)
    {
      exception = null;
      obj = default (GenerateConfig);
      try
      {
        obj = Deserialize (xml);
        return true;
      }
      catch (System.Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    public static bool Deserialize (string xml, out GenerateConfig obj)
    {
      System.Exception exception = null;
      return Deserialize (xml, out obj, out exception);
    }

    public static GenerateConfig Deserialize (string xml)
    {
      System.IO.StringReader stringReader = null;
      try
      {
        stringReader = new System.IO.StringReader (xml);
        return ((GenerateConfig) (Serializer.Deserialize (System.Xml.XmlReader.Create (stringReader))));
      }
      finally
      {
        if ((stringReader != null))
        {
          stringReader.Dispose ();
        }
      }
    }

    /// <summary>
    /// Serializes current GenerateConfig object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile (string fileName, out System.Exception exception)
    {
      exception = null;
      try
      {
        SaveToFile (fileName);
        return true;
      }
      catch (System.Exception e)
      {
        exception = e;
        return false;
      }
    }

    public virtual void SaveToFile (string fileName)
    {
      System.IO.StreamWriter streamWriter = null;
      try
      {
        string xmlString = Serialize ();
        System.IO.FileInfo xmlFile = new System.IO.FileInfo (fileName);
        streamWriter = xmlFile.CreateText ();
        streamWriter.WriteLine (xmlString);
        streamWriter.Close ();
      }
      finally
      {
        if ((streamWriter != null))
        {
          streamWriter.Dispose ();
        }
      }
    }

    /// <summary>
    /// Deserializes xml markup from file into an GenerateConfig object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output GenerateConfig object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile (string fileName, out GenerateConfig obj, out System.Exception exception)
    {
      exception = null;
      obj = default (GenerateConfig);
      try
      {
        obj = LoadFromFile (fileName);
        return true;
      }
      catch (System.Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    public static bool LoadFromFile (string fileName, out GenerateConfig obj)
    {
      System.Exception exception = null;
      return LoadFromFile (fileName, out obj, out exception);
    }

    public static GenerateConfig LoadFromFile (string fileName)
    {
      System.IO.FileStream file = null;
      System.IO.StreamReader sr = null;
      try
      {
        file = new System.IO.FileStream (fileName, FileMode.Open, FileAccess.Read);
        sr = new System.IO.StreamReader (file);
        string xmlString = sr.ReadToEnd ();
        sr.Close ();
        file.Close ();
        return Deserialize (xmlString);
      }
      finally
      {
        if ((file != null))
        {
          file.Dispose ();
        }
        if ((sr != null))
        {
          sr.Dispose ();
        }
      }
    }
    #endregion
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute ("System.Xml", "4.0.30319.17626")]
  [System.SerializableAttribute ()]
  [System.Diagnostics.DebuggerStepThroughAttribute ()]
  [System.ComponentModel.DesignerCategoryAttribute ("code")]
  [System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://xisd.at/GenerateConfig.xsd")]
  public partial class Schema
  {

    private static System.Xml.Serialization.XmlSerializer serializer;

    public string SourcePath { get; set; }
    public string TargetNamespace { get; set; }
    public string TargetXmlNamespace { get; set; }

    private static System.Xml.Serialization.XmlSerializer Serializer
    {
      get
      {
        if ((serializer == null))
        {
          serializer = new System.Xml.Serialization.XmlSerializer (typeof (Schema));
        }
        return serializer;
      }
    }

    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current Schema object into an XML document
    /// </summary>
    /// <returns>string XML value</returns>
    public virtual string Serialize ()
    {
      System.IO.StreamReader streamReader = null;
      System.IO.MemoryStream memoryStream = null;
      try
      {
        memoryStream = new System.IO.MemoryStream ();
        Serializer.Serialize (memoryStream, this);
        memoryStream.Seek (0, System.IO.SeekOrigin.Begin);
        streamReader = new System.IO.StreamReader (memoryStream);
        return streamReader.ReadToEnd ();
      }
      finally
      {
        if ((streamReader != null))
        {
          streamReader.Dispose ();
        }
        if ((memoryStream != null))
        {
          memoryStream.Dispose ();
        }
      }
    }

    /// <summary>
    /// Deserializes workflow markup into an Schema object
    /// </summary>
    /// <param name="xml">string workflow markup to deserialize</param>
    /// <param name="obj">Output Schema object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize (string xml, out Schema obj, out System.Exception exception)
    {
      exception = null;
      obj = default (Schema);
      try
      {
        obj = Deserialize (xml);
        return true;
      }
      catch (System.Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    public static bool Deserialize (string xml, out Schema obj)
    {
      System.Exception exception = null;
      return Deserialize (xml, out obj, out exception);
    }

    public static Schema Deserialize (string xml)
    {
      System.IO.StringReader stringReader = null;
      try
      {
        stringReader = new System.IO.StringReader (xml);
        return ((Schema) (Serializer.Deserialize (System.Xml.XmlReader.Create (stringReader))));
      }
      finally
      {
        if ((stringReader != null))
        {
          stringReader.Dispose ();
        }
      }
    }

    /// <summary>
    /// Serializes current Schema object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile (string fileName, out System.Exception exception)
    {
      exception = null;
      try
      {
        SaveToFile (fileName);
        return true;
      }
      catch (System.Exception e)
      {
        exception = e;
        return false;
      }
    }

    public virtual void SaveToFile (string fileName)
    {
      System.IO.StreamWriter streamWriter = null;
      try
      {
        string xmlString = Serialize ();
        System.IO.FileInfo xmlFile = new System.IO.FileInfo (fileName);
        streamWriter = xmlFile.CreateText ();
        streamWriter.WriteLine (xmlString);
        streamWriter.Close ();
      }
      finally
      {
        if ((streamWriter != null))
        {
          streamWriter.Dispose ();
        }
      }
    }

    /// <summary>
    /// Deserializes xml markup from file into an Schema object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output Schema object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile (string fileName, out Schema obj, out System.Exception exception)
    {
      exception = null;
      obj = default (Schema);
      try
      {
        obj = LoadFromFile (fileName);
        return true;
      }
      catch (System.Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    public static bool LoadFromFile (string fileName, out Schema obj)
    {
      System.Exception exception = null;
      return LoadFromFile (fileName, out obj, out exception);
    }

    public static Schema LoadFromFile (string fileName)
    {
      System.IO.FileStream file = null;
      System.IO.StreamReader sr = null;
      try
      {
        file = new System.IO.FileStream (fileName, FileMode.Open, FileAccess.Read);
        sr = new System.IO.StreamReader (file);
        string xmlString = sr.ReadToEnd ();
        sr.Close ();
        file.Close ();
        return Deserialize (xmlString);
      }
      finally
      {
        if ((file != null))
        {
          file.Dispose ();
        }
        if ((sr != null))
        {
          sr.Dispose ();
        }
      }
    }
    #endregion
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute ("System.Xml", "4.0.30319.17626")]
  [System.SerializableAttribute ()]
  [System.Diagnostics.DebuggerStepThroughAttribute ()]
  [System.ComponentModel.DesignerCategoryAttribute ("code")]
  [System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://xisd.at/GenerateConfig.xsd")]
  [System.Xml.Serialization.XmlRootAttribute (Namespace = "http://xisd.at/GenerateConfig.xsd", IsNullable = true)]
  public partial class Schemas
  {

    private List<Schema> schemaField;

    private static System.Xml.Serialization.XmlSerializer serializer;

    public Schemas ()
    {
      this.schemaField = new List<Schema> ();
    }

    [System.Xml.Serialization.XmlElementAttribute ("Schema")]
    public List<Schema> Schema
    {
      get
      {
        return this.schemaField;
      }
    }

    private static System.Xml.Serialization.XmlSerializer Serializer
    {
      get
      {
        if ((serializer == null))
        {
          serializer = new System.Xml.Serialization.XmlSerializer (typeof (Schemas));
        }
        return serializer;
      }
    }

    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current Schemas object into an XML document
    /// </summary>
    /// <returns>string XML value</returns>
    public virtual string Serialize ()
    {
      System.IO.StreamReader streamReader = null;
      System.IO.MemoryStream memoryStream = null;
      try
      {
        memoryStream = new System.IO.MemoryStream ();
        Serializer.Serialize (memoryStream, this);
        memoryStream.Seek (0, System.IO.SeekOrigin.Begin);
        streamReader = new System.IO.StreamReader (memoryStream);
        return streamReader.ReadToEnd ();
      }
      finally
      {
        if ((streamReader != null))
        {
          streamReader.Dispose ();
        }
        if ((memoryStream != null))
        {
          memoryStream.Dispose ();
        }
      }
    }

    /// <summary>
    /// Deserializes workflow markup into an Schemas object
    /// </summary>
    /// <param name="xml">string workflow markup to deserialize</param>
    /// <param name="obj">Output Schemas object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize (string xml, out Schemas obj, out System.Exception exception)
    {
      exception = null;
      obj = default (Schemas);
      try
      {
        obj = Deserialize (xml);
        return true;
      }
      catch (System.Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    public static bool Deserialize (string xml, out Schemas obj)
    {
      System.Exception exception = null;
      return Deserialize (xml, out obj, out exception);
    }

    public static Schemas Deserialize (string xml)
    {
      System.IO.StringReader stringReader = null;
      try
      {
        stringReader = new System.IO.StringReader (xml);
        return ((Schemas) (Serializer.Deserialize (System.Xml.XmlReader.Create (stringReader))));
      }
      finally
      {
        if ((stringReader != null))
        {
          stringReader.Dispose ();
        }
      }
    }

    /// <summary>
    /// Serializes current Schemas object into file
    /// </summary>
    /// <param name="fileName">full path of outupt xml file</param>
    /// <param name="exception">output Exception value if failed</param>
    /// <returns>true if can serialize and save into file; otherwise, false</returns>
    public virtual bool SaveToFile (string fileName, out System.Exception exception)
    {
      exception = null;
      try
      {
        SaveToFile (fileName);
        return true;
      }
      catch (System.Exception e)
      {
        exception = e;
        return false;
      }
    }

    public virtual void SaveToFile (string fileName)
    {
      System.IO.StreamWriter streamWriter = null;
      try
      {
        string xmlString = Serialize ();
        System.IO.FileInfo xmlFile = new System.IO.FileInfo (fileName);
        streamWriter = xmlFile.CreateText ();
        streamWriter.WriteLine (xmlString);
        streamWriter.Close ();
      }
      finally
      {
        if ((streamWriter != null))
        {
          streamWriter.Dispose ();
        }
      }
    }

    /// <summary>
    /// Deserializes xml markup from file into an Schemas object
    /// </summary>
    /// <param name="fileName">string xml file to load and deserialize</param>
    /// <param name="obj">Output Schemas object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    public static bool LoadFromFile (string fileName, out Schemas obj, out System.Exception exception)
    {
      exception = null;
      obj = default (Schemas);
      try
      {
        obj = LoadFromFile (fileName);
        return true;
      }
      catch (System.Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    public static bool LoadFromFile (string fileName, out Schemas obj)
    {
      System.Exception exception = null;
      return LoadFromFile (fileName, out obj, out exception);
    }

    public static Schemas LoadFromFile (string fileName)
    {
      System.IO.FileStream file = null;
      System.IO.StreamReader sr = null;
      try
      {
        file = new System.IO.FileStream (fileName, FileMode.Open, FileAccess.Read);
        sr = new System.IO.StreamReader (file);
        string xmlString = sr.ReadToEnd ();
        sr.Close ();
        file.Close ();
        return Deserialize (xmlString);
      }
      finally
      {
        if ((file != null))
        {
          file.Dispose ();
        }
        if ((sr != null))
        {
          sr.Dispose ();
        }
      }
    }
    #endregion
  }
}
