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


        //-------------GET all saved jobs of a seeker-----------//
        public async Task<JobsListResponseDto> GetAllSavedJobs(
            int userId,
           string? city, int? category, int? employmentType,
           string? search, int page, int pageSize
            )
        {
            var seekerProfile = await _db.SeekersProfiles
                .FirstOrDefaultAsync(sp => sp.UserId == userId);
            if (seekerProfile == null) throw new Exception("Seeker profile not found");

            //query for jobs
            var query = _db.SavedJobs
                .Include(sj => sj.Job)
                    .ThenInclude(j => j.EmployerProfile)
                .Where(sj => sj.SeekerProfile.UserId == userId)
                .OrderByDescending(sj => sj.SavedAt)
                .AsQueryable();

            // apply filters if provided
            if (!string.IsNullOrEmpty(city))
                query = query.Where(sj => sj.Job.City == city);

            if (category.HasValue)
                query = query.Where(sj => (int)sj.Job.Category == category.Value);

            if (employmentType.HasValue)
                query = query.Where(sj => (int)sj.Job.EmploymentType == employmentType.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(sj => sj.Job.Title.Contains(search) ||
                                           sj.Job.Description.Contains(search));

            var totalCount = await query.CountAsync();

            // apply pagination
            var savedJobs = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // map to response DTOs
            var savedDtos = savedJobs.Select(sj => new JobResponseDto
            {
                Id = sj.Job.Id,
                Title = sj.Job.Title,
                Description = sj.Job.Description,
                SalaryMin = sj.Job.SalaryMin,
                SalaryMax = sj.Job.SalaryMax,
                City = sj.Job.City,
                Category = sj.Job.Category,
                EmploymentType = sj.Job.EmploymentType,
                Status = sj.Job.Status,
                CreatedAt = sj.Job.CreatedAt,
                CompanyName = sj.Job.EmployerProfile.CompanyName
            }).ToList();

            return new JobsListResponseDto
            {
                Jobs = savedDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }


        //----------DELETE unsave a job-----------//
        public async Task UnsaveJob(int jobId, int userId)
        {
            var seekerProfile = await _db.SeekersProfiles
                .FirstOrDefaultAsync(sp => sp.UserId == userId);
            if (seekerProfile == null) throw new Exception("Seeker profile not found");

            var savedJob = await _db.SavedJobs
                .FirstOrDefaultAsync(sj => sj.JobId == jobId && sj.SeekerProfileId == seekerProfile.Id);

            if (savedJob == null) throw new Exception("Saved job not found");

            _db.SavedJobs.Remove(savedJob);
            await _db.SaveChangesAsync();
        }
    }
    }
