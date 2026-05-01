using JobBoard.DTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    [ApiController]
    [Route("api")]
    public class JobsController : ControllerBase
    {
        private readonly JobsService _jobsService;
        public JobsController(JobsService jobsService)
        {
            _jobsService = jobsService;
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJob(CreateJobDto dto)
        {
            try{
                //Extract userId from token
                var userId = int.Parse(User.FindFirst("userId").Value);

                //Call service
                var result = await _jobsService.CreateJob(dto, userId);

                //Return 201 Created
                return Created("", result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

    }
}


