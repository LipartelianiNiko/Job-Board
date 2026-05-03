using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;



namespace JobBoard.Services
{
    public class SavedJobsService
    {
        private readonly AppDbContext _db;


        public SavedJobsService(AppDbContext db)
        {
            _db = db;
        }


        //-----POST save a job, add it to a saved jobs collections of seeker profile---//

        public async Task<JobResponseDto> SaveJobById(int userId, int jobId)
        {
            //verify and get seeker profile
            var seekerProfile = await _db.SeekersProfiles
                .FirstOrDefaultAsync(sp => sp.UserId == userId);
            if (seekerProfile == null) throw new Exception("Seeker profile not found");

           //query db for the job
            var Job = await _db.Jobs
                .Include(j => j.EmployerProfile)
                .FirstOrDefaultAsync(u => u.Id == jobId);

            if (Job == null) throw new Exception("Job not found");

            //prevent duplicates
            var existing = await _db.SavedJobs
                .FirstOrDefaultAsync(sj => sj.SeekerProfileId == seekerProfile.Id && sj.JobId == jobId);
            if (existing != null) throw new Exception("Job already saved");

            //create a SavedJob object
            var SavedJob = new SavedJob
            {
                SeekerProfileId = seekerProfile.Id,
                JobId = jobId
            };


            _db.SavedJobs.Add(SavedJob);
            await _db.SaveChangesAsync();

            return new JobResponseDto
            {
                Id = Job.Id,
                CompanyName = Job.EmployerProfile.CompanyName,
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
