import { useEffect, useState } from "react";
import { getEmployerJobs } from "../api";



export default function EmployerDashboard(){
        const [jobs, setJobs]=useState([]);
        const [totalPages, setTotalPages] = useState(1);
        const [page, setPage] = useState(1);

        useEffect(() => {
            getEmployerJobs({page})
                .then(res => {
                setJobs(res.data.jobs);
                setTotalPages(res.data.totalPages);
                setPage(res.data.page);
                })
                .catch(err => console.log(err));
        }, [page]);

    return(

    <div style={{ paddingTop: "80px" }}>
    <div  id="employerDash">
        <div className="dashboard-wrap">
            <div className="dash-header">
            <div><div className="dash-title">Employer Dashboard</div><div className="dash-sub">Manage your listings and review applications</div></div>
            <button className="btn btn-primary" onClick={()=>{}}>+ Post New Job</button>
            </div>
            <div className="dash-tabs">
            <button className="dash-tab active" onClick={()=>{}}>My Listings</button>
            <button className="dash-tab" onClick={()=>{}}>Applicants</button>
            </div>

            <div id="listingsTab">
                <div className="data-list">
                    {jobs.map(job => (
                        <div className="data-row" key={job.id} onClick={()=>{alert("hello emp")}}>

                        <div className="data-info">
                        <div className="data-title">
                            {job.title}
                        </div>

                        <div className="data-sub">
                            <span>{job.city}</span>
                            <span>·</span>
                            <span>{job.employmentType}</span>
                            <span>·</span>
                            <span>
                            ${job.salaryMin}–${job.salaryMax}
                            </span>
                        </div>
                        </div>

                        <div className="data-actions">

                        <span className="app-count">
                            {job.applicantCount} applicants
                        </span>

                        <span className={`badge badge-${job.status.toLowerCase()}`}>
                            {job.status}
                        </span>

                        <button className="icon-btn">
                            ✎
                        </button>

                        <button className="icon-btn icon-btn-danger">
                            ✕
                        </button>

                        </div>

                    </div>
                ))}
                </div>
            </div>

            <div id="applicantsTab" className="hidden">
            <div className="data-list">
            </div>
            </div>
        </div>

        <div className="pagination" style={{marginBottom:'100px'}}>
          {Array.from({ length: totalPages }, (_, i) => i + 1).map(num=>(
            <button
                key={num}
                className={`page-btn ${page === num ? 'active' : ''}`}
                onClick={() => setPage(num)}>
                {num}
            </button>
          ))}
        </div>
        
    </div>
    </div>
    )
}