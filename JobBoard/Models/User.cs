namespace JobBoard.Models
{
    public enum Role { Seeker, Employer, Admin }
    public class User
    {
        //each user has id, email, passwor, role.both roles have this, but employer doesnt need name at all.
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //each user is either employer or seeker. 
        //user object is same for each role but additional profile object is created accroding to the role.
        //user object holds its coresponding role profile object, "?" indicates that attribute can be null.
        public SeekerProfile? SeekerProfile { get; set; }
        public EmployerProfile? EmployerProfile { get; set; }

    }
}
