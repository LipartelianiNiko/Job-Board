using JobBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.DTOs.JobsDTOs
{
    public class UpdateJobStatusDto
    {
        [Required]
        public JobStatus Status { get; set; }
    }
}
