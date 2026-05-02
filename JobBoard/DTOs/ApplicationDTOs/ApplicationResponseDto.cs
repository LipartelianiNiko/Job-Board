using JobBoard.Models;

namespace JobBoard.DTOs.ApplicationDTOs
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }

        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        public string? CoverLetter { get; set; }
        public ApplicationStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; } 

      
    }
}
