namespace Core.Domain.Entities
{
    public class Image : Entity
    {
        public Guid UserId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] ContentData { get; set; }
        public long CapacityMB { get; set; }
        public long Length { get; set; }
        public User User { get; set; }
    }
}
