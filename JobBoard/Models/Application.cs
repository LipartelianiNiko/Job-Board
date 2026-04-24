namespace JobBoard.Models
{
    public enum ApplicationStatus { Pending, Reviewed, Shortlisted, Rejected, Accepted }

    public class Application
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int SeekerProfileId { get; set; }
        public string? CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Job Job { get; set; } = null!;
        public SeekerProfile SeekerProfile { get; set; } = null!;
    }
}
