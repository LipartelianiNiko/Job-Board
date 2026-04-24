namespace JobBoard.Models
{
    public enum Role { Seeker, Employer, Admin }
    public class User
    {
        //each user has id, email, passwor, role.both roles have this, but employer doesnt need name at all.
        public int Id { get; set; } 
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
