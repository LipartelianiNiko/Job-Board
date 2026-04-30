using System.ComponentModel.DataAnnotations;
using JobBoard.Models;


//data shape of incoming data for login request, must have email and passwrod, name and role not neccessary
//no separate login dtos required for roles, both log in with just email and password.
namespace JobBoard.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Required]
        public string Password { get; set; }=string.Empty;

    }
}
