using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace XiSD
{
  public static class Helper
  {
    public static IEnumerable<XmlSchemaType> GetIncludedTypes (IEnumerable<XmlSchemaInclude> includes)
    {
      var includedTypes = new List<XmlSchemaType> ();

      foreach (var include in includes)
      {
        includedTypes.AddRange (include.Schema.Items.OfType<XmlSchemaType> ());
        includedTypes.AddRange (GetIncludedTypes (include.Schema.Includes.OfType<XmlSchemaInclude> ()));
      }

      return includedTypes;
    }

    public static IEnumerable<XmlSchemaInclude> GetSchemaIncludes (XmlSchemaSet set)
    {
      var schemaIncludes = new List<XmlSchemaInclude> ();

      foreach (XmlSchema schema in set.Schemas ())
        schemaIncludes.AddRange (GetSchemaIncludes (schema));

      return schemaIncludes;
    }

    private static IEnumerable<XmlSchemaInclude> GetSchemaIncludes (XmlSchema schema)
    {
      var schemaIncludes = new List<XmlSchemaInclude> ();

      foreach (XmlSchemaInclude include in schema.Includes)
      {
        schemaIncludes.AddRange (GetSchemaIncludes (include.Schema));
        schemaIncludes.Add (include);
      }

      return schemaIncludes;
    }
  }
}
