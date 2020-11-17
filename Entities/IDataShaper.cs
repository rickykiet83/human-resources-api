using System.Collections.Generic;
using Entities.Models;

namespace Entities
{
    public interface IDataShaper<T, K>
    {
        IEnumerable<ShapedEntity<K>> ShapeData(IEnumerable<T> entities, string fieldsString);
        ShapedEntity<K> ShapeData(T entity, string fieldsString);
    }
}