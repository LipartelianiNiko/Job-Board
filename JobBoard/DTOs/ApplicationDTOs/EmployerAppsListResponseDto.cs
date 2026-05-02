using JobBoard.Models;

namespace JobBoard.DTOs.ApplicationDTOs
{
    public class EmployerAppsListResponseDto
    {
            public List<EmployerAppResponseDto> Applications { get; set; } = new List<EmployerAppResponseDto>();
            public int TotalCount { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
       
    }
}
