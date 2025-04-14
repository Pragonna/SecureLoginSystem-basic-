using Core.Domain.Entities;
using Core.Domain.Entities.Enums;
using Core.Infrastructure.Securities.Models;

namespace Core.Infrastructure.Persistence.Users.Features.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public Image? Image { get;set;}
    }
}
