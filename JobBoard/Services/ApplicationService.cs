using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.DTOs.ApplicationDTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;


namespace JobBoard.Services
{
    public class ApplicationService
    {

        private readonly AppDbContext _db;


        public ApplicationService(AppDbContext db)
        {
            _db = db;
        }

        //POST create Application, apply to a job. 
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
            var application = new Models.Application
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

        //-----------------GET all of the single seeker's Applications--------//
        public async Task<AppListResponseDto> GetAllApps(int userId, int page, int pageSize)
        {
            // query for user's applications
            var query = _db.Applications
                .Include(a => a.Job)
                    .ThenInclude(j => j.EmployerProfile)
                .Where(a => a.SeekerProfile.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .AsQueryable();

            //count total amout
            var totalCount = await query.CountAsync();

            //apply pagination
            var applications = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

          

            // map to response DTOs
            var appsDtos = applications.Select(j => new ApplicationResponseDto
            {
                Id = j.Id,
                JobId = j.JobId,
                JobTitle = j.Job.Title,
                CompanyName = j.Job.EmployerProfile.CompanyName,
                CoverLetter = j.CoverLetter,
                Status = j.Status,
                CreatedAt = j.CreatedAt
            }).ToList();
           

            return new AppListResponseDto
            {
                Applications = appsDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        //----------withdraw, DELETE application-----------//
        public async Task WithdrawApp(int id, int userId)
        {
            var application = await _db.Applications
                .Include(a => a.SeekerProfile)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (application == null) throw new Exception("Job not found");
            if (application.SeekerProfile.UserId != userId) throw new Exception("Unauthorized!");

            _db.Applications.Remove(application);
            await _db.SaveChangesAsync();
        }


        //-----GET show employer list of all aplications on a job-----//
        public async Task<EmployerAppsListResponseDto> GetAppsOfJob(int userId, int jobId, int page, int pageSize)
        {
            // verify job exists and belongs to this employer
            var job = await _db.Jobs
                .Include(j => j.EmployerProfile)
                .FirstOrDefaultAsync(j => j.Id == jobId);

            if (job == null) throw new Exception("Job not found");
            if (job.EmployerProfile.UserId != userId) throw new Exception("Unauthorized");

            // query applications for this job
            var query = _db.Applications
                .Include(a => a.SeekerProfile)
                    .ThenInclude(sp => sp.User)
                .Where(a => a.JobId == jobId)
                .OrderByDescending(a => a.CreatedAt)
                .AsQueryable();

            //count total amout
            var totalCount = await query.CountAsync();

            //apply pagination
            var applications = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // map to response DTOs
            var appsDtos = applications.Select(a => new EmployerAppResponseDto
            {
                Id = a.Id,
                SeekerName = a.SeekerProfile.User.FullName,
                SeekerEmail = a.SeekerProfile.User.Email,
                CoverLetter = a.CoverLetter,
                Status = a.Status,
                AppliedAt = a.CreatedAt
            }).ToList();


            return new EmployerAppsListResponseDto
            {
                Applications = appsDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }



        //------PATCH change applications status----//
        public async Task<EmployerAppResponseDto> UpdateAppStatus(int userId, int applicationId, UpdateAppStatusDto dto)
        {
            var application = await _db.Applications
            .Include(a => a.Job)
                .ThenInclude(j => j.EmployerProfile)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null) throw new Exception("Application not found");
            if (application.Job.EmployerProfile.UserId != userId) throw new Exception("Unauthorized");

            application.Status = dto.Status;
            application.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return new EmployerAppResponseDto
            {
                Id = application.Id,
                SeekerName = application.SeekerProfile.User.FullName,
                SeekerEmail = application.SeekerProfile.User.Email,
                CoverLetter = application.CoverLetter,
                Status = application.Status,
                AppliedAt = application.CreatedAt
            };
        }




    }
}
