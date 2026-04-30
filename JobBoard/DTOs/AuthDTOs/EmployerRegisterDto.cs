using JobBoard.Models;
using System.ComponentModel.DataAnnotations;


//separate registration dto  for employers with added comapny name field
namespace JobBoard.DTOs.AuthDTOs
{
    public class EmployerRegisterDto
    {
        [Required]//name must be inlcuded in the recieved data
        public string FullName { get; set; } = string.Empty;

        [Required]//eamil must be inlcuded in the recieved data
        [EmailAddress]//must be and email
        public string Email { get; set; } = string.Empty;

        [Required]//password must be inlcuded in the recieved data
        [MinLength(8)]//must be 8 chars long minimum
        public string Password { get; set; } = string.Empty;


        [Required]
        public string CompanyName {  get; set; }   = string.Empty;
    }
}
