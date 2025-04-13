namespace Core.Domain.Entities
{
    public class UserRole
    {
        public UserRole(int id, string role)
        {
            Id = id;
            Role = role;
        }

        public int Id { get; set; }
        public string Role { get; set; }
        public ICollection<UserAndUserRole> UserAndUserRoles { get; set; }

    }
}
