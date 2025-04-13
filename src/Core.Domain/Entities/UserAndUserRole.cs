using Core.Domain.Entities;

public class UserAndUserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public int RoleId { get; set; }
    public UserRole Role { get; set; }
}
