using JobBoard.DTOs;
using JobBoard.DTOs.ApplicationDTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using JobBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;



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
            
                //Extract userId from token
                var userId = int.Parse(User.FindFirst("userId")!.Value);

                //Call service
                var result = await _applicationService.CreateApplication(userId, jobId, dto);

                //Return 201 Created
                return Created("", result);
            

        }

        //------GET seeker get all of their applications sent----//
        [Authorize(Roles = "Seeker")]
        [HttpGet("seeker/applications")]
        public async Task<IActionResult> GetSeekerApps(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
           
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _applicationService.GetAllApps(userId, page, pageSize);

                return Ok(result);
            

        }


        //-----------DELETE withdraw/remove a application-----//
        [Authorize(Roles = "Seeker")]
        [HttpDelete("applications/{id}")]
        public async Task<IActionResult> DeleteApplications(int id)
        {

           
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                await _applicationService.WithdrawApp(id, userId);
                return NoContent();
            

        }


        [Authorize(Roles = "Employer")]
        [HttpGet("employer/jobs/{jobId}/applications")]
        public async Task<IActionResult> GetJobApps(
            int jobId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
           
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _applicationService.GetAppsOfJob(userId, jobId, page, pageSize);

                return Ok(result);
            
        }

        [Authorize(Roles = "Employer")]
        [HttpPatch("applications/{appId}/status")]
        public async Task<IActionResult> UpdateAppStatus(int appId, UpdateAppStatusDto dto)
        {
           
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _applicationService.UpdateAppStatus(userId, appId, dto );

                return Ok(result);


        }

    }

}
