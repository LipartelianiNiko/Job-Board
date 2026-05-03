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
    public class JobsController : ControllerBase
    {
        private readonly JobsService _jobsService;
        public JobsController(JobsService jobsService)
        {
            _jobsService = jobsService;
        }


        //----------------POST create a job------------------//

        [Authorize(Roles = "Employer")]
        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJob(CreateJobDto dto)
        {
                //Extract userId from token
                var userId = int.Parse(User.FindFirst("userId")!.Value);

                //Call service
                var result = await _jobsService.CreateJob(dto, userId);

                //Return 201 Created
                return Created("", result);
           

        }

        //----------------GET all jobs--------------------//

        [HttpGet("jobs")]
        public async Task<IActionResult> GetJobs(
        [FromQuery] string? city,
        [FromQuery] int? category,
        [FromQuery] int? employmentType,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {

                var result = await _jobsService.GetAllJobs(city, category, employmentType, search, page, pageSize);

                return Ok(result);
            

        }

        //----------------------GET one job-------------//

        [HttpGet("jobs/{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
           
                var result = await _jobsService.GetJobById(id);

                return Ok(result);
           
        }

        //--------------------PATHC a job, modify---------------//
        [Authorize(Roles = "Employer")]
        [HttpPatch("jobs/{id}")]
        public async Task<IActionResult> UpdateJob(int id, UpdateJobDto dto)
        {

            
                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _jobsService.UpdateJob(id, dto, userId);
                return Ok(result);


        }


        //-------------------Update Job status---------------------//

        [Authorize(Roles = "Employer")]
        [HttpPatch("jobs/{id}/status")]
        public async Task<IActionResult> UpdateJobStatus(int id, UpdateJobStatusDto dto)
        {


                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _jobsService.UpdateJobStatus(id, dto, userId);
                return Ok(result);


        }

        //--------------DELETE a job------------//
        [Authorize(Roles = "Employer")]
        [HttpDelete("jobs/{id}/delete")]
        public async Task<IActionResult> DeleteJob(int id)
        {


                var userId = int.Parse(User.FindFirst("userId")!.Value);
                await _jobsService.DeleteJob(id, userId);
                return NoContent();


        }

        //-------GET GetEmployerJobs — employer sees all their listings----//
        [Authorize(Roles = "Employer")]
        [HttpGet("employer/jobs")]
        public async Task<IActionResult> GetEmployerJobs(
        [FromQuery] string? city,
        [FromQuery] int? category,
        [FromQuery] int? employmentType,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {

                var userId = int.Parse(User.FindFirst("userId")!.Value);
                var result = await _jobsService.GetEmployerJobs(userId, city, category, employmentType, search, page, pageSize);

                return Ok(result);


        }



    }
}


