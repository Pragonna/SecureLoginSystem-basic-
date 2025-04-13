namespace Core.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; } = null;
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }
       
    }
}
