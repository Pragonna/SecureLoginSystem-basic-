namespace Core.Infrastructure.Persistence.Users.Features.Dtos
{
    public class RefreshTokenDto
    {
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
