using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;

//create job based on recieved DTO, save the job, return creaed job
namespace JobBoard.Services
{
    public class JobsService
    {
        private readonly AppDbContext _db;


        public JobsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<JobResponseDto> CreateJob(CreateJobDto dto, int UserId) 
        { 
            //Get the employer's profile from database using userId from JWT token
            var employerProfile= await _db.EmployerProfiles.FirstOrDefaultAsync(u => u.UserId ==UserId);
            if (employerProfile == null) throw new Exception("Employer profile not found");

            //Create Job object from DTO
            //Assign EmployerProfileId
            var Job = new Job
            {

                EmployerProfileId = employerProfile.Id,
                Title = dto.Title,
                Description = dto.Description,
                SalaryMin = dto.SalaryMin,
                SalaryMax = dto.SalaryMax,
                City = dto.City,
                Category = dto.Category,
                EmploymentType = dto.EmploymentType
            };

            //Save to database
            _db.Jobs.Add(Job);
            await _db.SaveChangesAsync();

            //Return JobResponseDto
            return new JobResponseDto
            {
                Id = Job.Id,
                CompanyName = employerProfile.CompanyName, 
                Title = Job.Title,
                Description = Job.Description,
                SalaryMin = Job.SalaryMin,
                SalaryMax = Job.SalaryMax,
                City = Job.City,
                Category = Job.Category,
                EmploymentType = Job.EmploymentType,
                Status = Job.Status,
                CreatedAt = Job.CreatedAt
            };
        }
    }
}
