using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.ExtensibleStorage;
using System;

namespace RevitAddin.StorageExample.Services
{
    public abstract class StorageFactory<T>
    {
        public abstract Guid Guid { get; }
        public abstract string SchemaName { get; }
        public abstract string FieldName { get; }
        public void Save(Element element, T data)
        {
            using (var entity = this.GetSchemaEntity(element))
            {
                entity.Set(this.FieldName, data);
                element.SetEntity(entity);
            }
        }
        public T Load(Element element)
        {
            using (var entity = this.GetSchemaEntity(element))
            {
                var storage = entity.Get<T>(this.FieldName);
                return storage;
            }
        }

        public void Reset(Element element)
        {
            using (var entity = this.GetSchemaEntity(element))
            {
                element.DeleteEntity(entity.Schema);
            }
        }

        private Entity GetSchemaEntity(Element element)
        {
            var schema = Schema.Lookup(Guid);

            if (schema == null)
            {
                schema = this.CreateSchema(Guid);
            }

            var entity = element.GetEntity(schema);
            if (entity.Schema == null)
            {
                entity = new Entity(schema);
            }

            return entity;
        }

        private Schema CreateSchema(Guid Guid)
        {
            SchemaBuilder builder = new SchemaBuilder(Guid);
            builder.SetReadAccessLevel(AccessLevel.Public);
            builder.SetWriteAccessLevel(AccessLevel.Public);
            builder.SetSchemaName(this.SchemaName);
            builder.AddSimpleField(this.FieldName, typeof(T));
            return builder.Finish();
        }
    }
}