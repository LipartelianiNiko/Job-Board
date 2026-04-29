using JobBoard.Models;

//shape of data we return on response to atuhentication, includes name,  token givem, role

namespace JobBoard.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public Role Role { get; set; } 
        public string Token { get; set; } = string.Empty;
    }
}
