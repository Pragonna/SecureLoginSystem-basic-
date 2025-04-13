using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class SecurityDetails : Entity
    {
        public string OTPCode { get; set; }
        public DateTime OTPExpiryDate { get; set; }
        public Guid UserId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public string? IPAddress { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
