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
            try
            {
                //Extract userId from token
                var userId = int.Parse(User.FindFirst("userId")!.Value);

                //Call service
                var result = await _savedJobsService.SaveJobById(userId, jobId);

                //Return 201 Created
                return Created("", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

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
            try
            {
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _savedJobsService.GetAllSavedJobs(userId, city, category, employmentType, search, page, pageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }



    }
}
