using JobBoard.DTOs;
using JobBoard.DTOs.ApplicationDTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using JobBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace JobBoard.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationService _applicationService;
        public ApplicationsController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [Authorize(Roles = "Seeker")]
        [HttpPost("jobs/{jobId}/apply")]
        public async Task<IActionResult> CreateApplication(CreateApplicationDto dto, int jobId)
        {
            try
            {
                //Extract userId from token
                var userId = int.Parse(User.FindFirst("userId")!.Value);

                //Call service
                var result = await _applicationService.CreateApplication(userId, jobId, dto);

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
