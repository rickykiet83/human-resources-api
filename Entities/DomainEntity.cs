
namespace Entities
{
    public class DomainEntity<T>
    {
        public T Id { get; set; }
        
        // True if domain has an identity
        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}