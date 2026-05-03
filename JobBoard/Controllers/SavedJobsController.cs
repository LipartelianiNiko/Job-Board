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

        

    }
}
