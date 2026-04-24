using static System.Net.Mime.MediaTypeNames;

namespace JobBoard.Models
{
    public class SavedJob
    {
        public int Id { get; set; }
        public int SeekerProfileId { get; set; }
        public int JobId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        public SeekerProfile SeekerProfile { get; set; } = null!;
        public Job Job { get; set; } = null!;
    }
}
