import { useNavigate } from "react-router-dom";

export default function JobCard({job}){
    const navigate=useNavigate();

    return(
        <div className="job-card" onClick={()=>{navigate(`/jobs/${job.id}`)}}>
            <div class="company-logo">{job.companyName.substring(0, 2).toUpperCase()}</div>
            <div className="job-body">
                <div className="job-title">{job.title}</div>
                    <div className="job-meta">
                        <span>{job.companyName}</span><span className="job-meta-dot"></span>
                        <span>{job.city}</span><span className="job-meta-dot"></span>
                        <span>{job.date}</span>
                    </div>
            </div>
            <div className="job-tags">
                <span className="tag tag-type">{job.employmentType}</span>
                <span className="tag tag-city">{job.city}</span>
            </div>
            <span className="job-salary">{job.salaryMin}$</span>
            <span className="job-salary">{job.salaryMax}$</span>

            <button className="job-save-btn {savedJobs.has(j.id)?'saved':''}" onClick={() => {}}>♡</button>
        </div>
    );
}