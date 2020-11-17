namespace Entities.Models
{
    public class ShapedEntity<K> : DomainEntity<K>
    {
        public ShapedEntity()
        {
            Entity = new Entity();
        }
        
        public Entity Entity { get; set; }
    }
}