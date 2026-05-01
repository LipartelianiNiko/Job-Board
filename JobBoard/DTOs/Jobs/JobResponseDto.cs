using JobBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.DTOs.Jobs
{
    public class JobResponseDto
    {

        public int Id { get; set; }
        public JobCategory Category { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string? City { get; set; }
        public JobCategory Category { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Draft;
    }
}
