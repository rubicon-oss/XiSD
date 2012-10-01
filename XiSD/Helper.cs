// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
