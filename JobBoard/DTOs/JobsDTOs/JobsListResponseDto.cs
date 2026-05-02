using JobBoard.Models;


    namespace JobBoard.DTOs.JobsDTOs
    {
        public class JobsListResponseDto
        {
            public List<JobResponseDto> Jobs { get; set; } = new List<JobResponseDto>();
            public int TotalCount { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
        }
    }

