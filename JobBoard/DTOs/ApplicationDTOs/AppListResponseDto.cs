using JobBoard.DTOs.ApplicationDTOs;

namespace JobBoard.DTOs.ApplicationDTOs
{
    public class AppListResponseDto
    {
        public List<ApplicationResponseDto> Applications { get; set; } = new List<ApplicationResponseDto>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
