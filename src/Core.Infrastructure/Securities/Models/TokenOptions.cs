namespace Core.Infrastructure.Securities.Models
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecretKey { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
