using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace JobBoard.Models
{
    public class SeekerProfile
    {

        //separate class for profile, one for seekers, one for emplyers, they both have user model though
        //seeker has id, name, email recuired, could have phone and other contact, list of  applications,list of saved jobs. 
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }= string.Empty;

        public string Email {  get; set; }= string.Empty;

        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public string? ResumeUrl { get; set; }
        public string? City { get; set; }
        public string? LinkedInUrl { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();



    }
}
