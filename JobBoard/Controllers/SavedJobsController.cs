using JobBoard.DTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using JobBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JobBoard.Controllers
{
    [ApiController]
    [Route("api")]
    public class SavedJobsController:ControllerBase
    {

        private readonly SavedJobsService _savedJobsService;
        public SavedJobsController(SavedJobsService savedJobsService)
        {
            _savedJobsService = savedJobsService;
        }

        //-------POST save a job with id-----//
        [Authorize(Roles = "Seeker")]
        [HttpPost("jobs/{jobId}/save")]
        public async Task<IActionResult> CreateJob( int jobId)
        {

                //Extract userId from token
                var userId = int.Parse(User.FindFirst("userId")!.Value);

                //Call service
                var result = await _savedJobsService.SaveJobById(userId, jobId);

                //Return 201 Created
                return Created("", result);


        }

        //----------GET all saved jobs of a seeker-----------------//
        [Authorize(Roles = "Seeker")]
        [HttpGet("seeker/savedJobs")]
        public async Task<IActionResult> GetEmployerJobs(
        [FromQuery] string? city,
        [FromQuery] int? category,
        [FromQuery] int? employmentType,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
   
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _savedJobsService.GetAllSavedJobs(userId, city, category, employmentType, search, page, pageSize);

                return Ok(result);


        }

        //----------DELETE unsave a job-----------//
        [Authorize(Roles = "Seeker")]
        [HttpDelete("seeker/savedJobs/{jobId}")]
        public async Task<IActionResult> UnsaveJob(int jobId)
        {

                var userId = int.Parse(User.FindFirst("userId")!.Value);
                await _savedJobsService.UnsaveJob(jobId, userId);
                return NoContent();

        }
        



    }
}
