using JobBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.DTOs.Jobs
{
    public class CreateJobDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string? City { get; set; }

        [Required]
        public JobCategory Category { get; set; }

        [Required]
        public EmploymentType EmploymentType { get; set; }
    }
}
