using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.DTOs.ApplicationDTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;


namespace JobBoard.Services
{
    public class ApplicationService
    {

        private readonly AppDbContext _db;


        public ApplicationService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ApplicationResponseDto> CreateApplication(int Userid, int Jobid, CreateApplicationDto dto)
        {
            //ckeck token to make sure its seeker and that it exists.
            var seekerProfile = await _db.SeekersProfiles.FirstOrDefaultAsync(u => u.UserId == Userid);
            if (seekerProfile == null) throw new Exception("Seeker profile not found");

            //ckeck job id to verify that job exists adn retrive job
            var job = await _db.Jobs
                .Include(j => j.EmployerProfile)
                .FirstOrDefaultAsync(u => u.Id == Jobid);

            if (job == null) throw new Exception("Job not found");

            //check if user already applied, no duplicate applications allowed.
            var existing = await _db.Applications
                 .FirstOrDefaultAsync(a => a.SeekerProfileId == seekerProfile.Id && a.JobId == Jobid);
            if (existing != null) throw new Exception("Already applied to this job");

            //check if job status is open
            if (job.Status != JobStatus.Open) throw new Exception("Job is not open");

            //create application object, user userid of seeker to get seeker profile and assign it to application
            var application = new Application
            {
                JobId = Jobid,
                SeekerProfileId = seekerProfile.Id,
                CoverLetter = dto.CoverLetter
            };



            //adding the application to the job's applications' collection is automatically done by EF core.
            //same way , no need to add seekerprofile to application object
            _db.Applications.Add(application);
            await _db.SaveChangesAsync();

            return new ApplicationResponseDto
            {
                Id = application.Id,
                JobId = application.JobId,
                JobTitle = job.Title,
                CompanyName = job.EmployerProfile.CompanyName,
                CoverLetter = application.CoverLetter,
                Status = application.Status,
                CreatedAt = application.CreatedAt
            };


        }

    }
}
