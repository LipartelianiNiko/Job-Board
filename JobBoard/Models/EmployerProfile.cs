namespace JobBoard.Models
{
    public class EmployerProfile
    {

        //id, company name, email, jobs list, city. 

        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Job> Jobs { get; set; } = new List<Job>();

    }
}
