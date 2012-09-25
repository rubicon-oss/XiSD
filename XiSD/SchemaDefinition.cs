using System.IO;

namespace XiSD
{
  public class SchemaDefinition
  {
    public string SchemaPath { get; private set; }
    public string Namespace { get; private set; }
    public string XmlNamespace { get; private set; }

    public SchemaDefinition (string path, string ns, string xmlns)
    {
      SchemaPath = path;
      Namespace = ns;
      XmlNamespace = xmlns;
    }

    public string OutputPath
    {
      get { return Path.ChangeExtension (SchemaPath, ".generated.cs"); }
    }
  }
}