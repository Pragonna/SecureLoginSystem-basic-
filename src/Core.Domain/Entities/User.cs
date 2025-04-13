using Core.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public Image? Image { get; set; }
        public SecurityDetails? SecurityDetails { get; set; }
        public ICollection<UserAndUserRole> UserAndUserRoles { get; set; }
        public User()
        {
            UserAndUserRoles = new HashSet<UserAndUserRole>();
        }
        public User(string email, string firstName = null, string lastName = null, DateTime dateOfBirth = default, Gender gender = default, string country = null, string? bio = null, Image? image = null):this()
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Country = country;
            Bio = bio;
            Image = image;
        }
    }
}
