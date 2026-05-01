using JobBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.DTOs.JobsDTOs
{
    public class UpdateJobDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string? City { get; set; }
        public JobCategory? Category { get; set; }
        public EmploymentType? EmploymentType { get; set; }
    }
}
