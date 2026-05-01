using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.DTOs.JobsDTOs;
using JobBoard.DTOs.JobsDTOs.JobBoard.DTOs.JobsDTOs;
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

        //-------------------create and return a job---------------------------------//
        public async Task<JobResponseDto> CreateJob(CreateJobDto dto, int UserId) 
        { 
            //Get the employer's profile from database using userId from JWT token
            var employerProfile= await _db.EmployerProfiles.FirstOrDefaultAsync(u => u.UserId ==UserId);
            if (employerProfile == null) throw new Exception("Employer profile not found");
            if (dto.SalaryMin < 0) throw new Exception("Salary cannot be negative");
            if (dto.SalaryMin > dto.SalaryMax) throw new Exception("SalaryMin cannot exceed SalaryMax");

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

        //--------------------get all jobs---------------------//
        public async Task<JobsListResponseDto> GetAllJobs(
            string? city, int? category, int? employmentType,
            string? search, int page, int pageSize)
        {
            // start with all open jobs
            var query = _db.Jobs
                .Include(j => j.EmployerProfile)
                .Where(j => j.Status == JobStatus.Open)
                .AsQueryable();

            // apply filters if provided
            if (!string.IsNullOrEmpty(city))
                query = query.Where(j => j.City == city);

            if (category.HasValue)
                query = query.Where(j => (int)j.Category == category.Value);

            if (employmentType.HasValue)
                query = query.Where(j => (int)j.EmploymentType == employmentType.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(j => j.Title.Contains(search) ||
                                          j.Description.Contains(search));

            // get total count before pagination
            var totalCount = await query.CountAsync();

            // apply pagination
            var jobs = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // map to response DTOs
            var jobDtos = jobs.Select(j => new JobResponseDto
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                SalaryMin = j.SalaryMin,
                SalaryMax = j.SalaryMax,
                City = j.City,
                Category = j.Category,
                EmploymentType = j.EmploymentType,
                Status = j.Status,
                CreatedAt = j.CreatedAt,
                CompanyName = j.EmployerProfile.CompanyName
            }).ToList();

            return new JobsListResponseDto
            {
                Jobs = jobDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }


        //---------get a single job-------------//

        public async Task<JobResponseDto> GetJobById(int id)
        {

            var Job = await _db.Jobs
                .Include(j => j.EmployerProfile)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (Job == null) throw new Exception("Job not found");


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
