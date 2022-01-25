using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace RevitAddin.StorageExample.Services
{
    public class StorageTextService : StorageFactory<string>
    {
        public override Guid Guid => new Guid("1E832DE4-C3F8-4DAE-84AF-450BAF593AC4");
        public override string FieldName => "Text";
        public override string SchemaName => "StorageTextService";
    }
}