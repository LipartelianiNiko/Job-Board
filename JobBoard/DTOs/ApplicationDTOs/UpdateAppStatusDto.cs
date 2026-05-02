using JobBoard.Models;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.DTOs.ApplicationDTOs
{
    public class UpdateAppStatusDto
    {

        [Required]
        public ApplicationStatus Status { get; set; }
    }
}
