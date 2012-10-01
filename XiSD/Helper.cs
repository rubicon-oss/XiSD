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
