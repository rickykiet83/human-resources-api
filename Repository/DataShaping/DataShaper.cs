using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entities;
using Entities.Models;

namespace Repository.DataShaping
{
    public class DataShaper<T, K> : IDataShaper<T, K> where T : class
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        
        public IEnumerable<ShapedEntity<K>> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return GetData(entities, requiredProperties);
        }

        public ShapedEntity<K> ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            return GetDataForEntity(entity, requiredProperties);
        }

        private ShapedEntity<K> GetDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ShapedEntity<K>();

            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }
            
            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (K) objectProperty.GetValue(entity);

            return shapedObject;
        }

        private IEnumerable<ShapedEntity<K>> GetData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity<K>>();

            foreach (var entity in entities)
            {
                var shapedObject = GetDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }

            return shapedData;
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties
                        .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                            StringComparison.InvariantCultureIgnoreCase));
                    
                    if (property == null)
                        continue;
                    
                    requiredProperties.Add(property);
                }
            }
            else
            {
                requiredProperties = Properties.ToList();
            }

            return requiredProperties;
        }
    }
}