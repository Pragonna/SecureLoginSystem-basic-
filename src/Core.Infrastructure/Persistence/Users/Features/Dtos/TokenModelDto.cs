namespace Core.Infrastructure.Persistence.Users.Features.Dtos
{
    public class TokenModelDto
    {
        public string RefreshToken { get; set; }
        public string AccessToken{ get; set; }
    }
}
