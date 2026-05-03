using JobBoard.Models;

namespace JobBoard.DTOs.AuthDTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string? CompanyName { get; set; }
    }
}
