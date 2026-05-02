using JobBoard.Models;

namespace JobBoard.DTOs.ApplicationDTOs
{
    public class EmployerAppResponseDto
    {
        //seekers details and cover letter.
        public int Id { get; set; }//seekers id
        public string SeekerName { get; set; } = string.Empty;
        public string SeekerEmail { get; set; } = string.Empty;
        public string? CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedAt { get; set; }//smae as createdAt
    }
}
