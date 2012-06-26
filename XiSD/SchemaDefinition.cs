using System.IO;

namespace XiSD
{
  public class SchemaDefinition
  {
    public string SchemaPath { get; private set; }
    public string Namespace { get; private set; }

    public SchemaDefinition (string path, string ns)
    {
      SchemaPath = path;
      Namespace = ns;
    }

    public string OutputPath
    {
      get { return Path.ChangeExtension (SchemaPath, ".generated.cs"); }
    }
  }
}