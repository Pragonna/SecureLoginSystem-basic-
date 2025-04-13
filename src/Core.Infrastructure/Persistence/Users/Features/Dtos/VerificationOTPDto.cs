namespace Core.Infrastructure.Persistence.Users.Features.Dtos
{
    public class VerificationOTPDto
    {
        public bool IsValid { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? AccessTokenExpiryDate { get; set; }
        public string? Email { get; set; }

    }
}
