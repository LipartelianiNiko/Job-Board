using static System.Net.Mime.MediaTypeNames;

namespace JobBoard.Models
{

    public enum EmploymentType { FullTime, PartTime, Contract, Internship }
    public enum JobStatus { Draft, Open, Closed }

    public enum JobCategory
    {
        Technology,
        Finance,
        Marketing,
        Design,
        Sales,
        HR,
        Operations,
        Legal,
        Management,
        Other
    }
    public class Job
    {
        //job has id, employer id, description, title, salary range, location, category, and more

        public int Id { get; set; }
        public int EmployerProfileId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string? City { get; set; }
        public JobCategory Category { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public EmployerProfile EmployerProfile { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();
    }
}
