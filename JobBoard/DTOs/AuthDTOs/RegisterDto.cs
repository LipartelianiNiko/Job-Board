using System.ComponentModel.DataAnnotations;
using JobBoard.Models;

//data shape that user will be sending when registerin, will have name, email, password and role.
namespace JobBoard.DTOs
{
    public class RegisterDto
    {
        [Required]//name must be inlcuded in the recieved data
        public string Name {  get; set; }=string.Empty;

        [Required]//eamil must be inlcuded in the recieved data
        [EmailAddress]//must be and email
        public string Email { get; set; }  = string.Empty;

        [Required]//password must be inlcuded in the recieved data
        [MinLength(8)]//must be 8 chars long minimum
        public string Password { get; set; }= string.Empty;

        [Required]//role must be included in the recieved data
        public Role Role { get; set; }    
    }
}
