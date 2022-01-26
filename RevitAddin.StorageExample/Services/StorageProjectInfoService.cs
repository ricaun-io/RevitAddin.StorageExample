using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitAddin.StorageExample.Services
{
    public class StorageProjectInfoService : StorageProjectInfoFactory<string>
    {
        public override Guid Guid => new Guid("1E832DE4-C3F8-4DAE-84AF-450BAF593AC3");
        public override string FieldName => "Text";
        public override string SchemaName => "StorageProjectInfoService";
        public override string VendorId => "ricaun";
    }
}