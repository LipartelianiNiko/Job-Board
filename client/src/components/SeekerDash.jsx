import { useEffect, useState } from "react";
import { getMyApplications } from "../api";
import { getSavedJobs } from "../api";

export default function SeekerDashboard(){
    const [applications, setApplications]=useState([]);
    const [savedJobs, setSavedJobs]=useState([]);



    const [appPage, setAppPage] = useState(1);
    const [savedPage, setSavedPage] = useState(1);

    const [appTotalPages, setAppTotalPages] = useState(1);
    const [savedTotalPages, setSavedTotalPages] = useState(1);

    const [activeTab, setActiveTab] = useState('applications');

    useEffect(() => {
        getMyApplications({page:appPage})
        .then(res => {
            setApplications(res.data.applications);
            setAppTotalPages(res.data.totalPages);
            setAppPage(res.data.page);
        })
        .catch(err => console.log(err));
    }, [appPage]);

    useEffect(() => {
        getSavedJobs({page:savedPage})
        .then(res => {
            setSavedJobs(res.data.jobs);
            setSavedTotalPages(res.data.totalPages);
            setSavedPage(res.data.page);
        })
        .catch(err => console.log(err));
    }, [savedPage]);

    function getStatusClass(status) {
        switch (status) {
            case "Pending":
            return "badge-pending";
            case "Shortlisted":
            return "badge-shortlisted";
            case "Accepted":
            return "badge-accepted";
            case "Rejected":
            return "badge-rejected";
            default:
            return "badge-draft";
        }
    }

    return(
    <div style={{ paddingTop: "80px" }}>
    <div  id="seekerDash">
        <div className="dashboard-wrap">

            <div className="dash-header">
            <div><div className="dash-title">My Dashboard</div><div className="dash-sub">Track your applications and saved roles</div></div>
            </div>

            <div className="dash-tabs">
            <button className={`dash-tab ${activeTab === 'applications' ? 'active' : ''}`} onClick={() => setActiveTab('applications')}>Applications</button>
            <button className={`dash-tab ${activeTab === 'saved' ? 'active' : ''}`} onClick={() => setActiveTab('saved')}>Saved Jobs</button>
            </div>

        {/* APPLICATIONS TAB */}
        {activeTab === "applications" && (
            <div id="appsTab">
                <div className="data-list">
                {applications.map(app => (
                <div className="data-row" key={app.id} onClick={()=>{alert("hello")}}>
                    
                    <div className="data-info">
                    <div className="data-title">{app.jobTitle}</div>

                    <div className="data-sub">
                        <span>{app.companyName}</span>
                    </div>
                    </div>

                    <span className="data-date">
                    {new Date(app.createdAt).toLocaleDateString()}
                    </span>

                    <span className={`badge ${getStatusClass(app.status)}`}>
                    {app.status}
                    </span>

                </div>
            ))}
            </div>
            </div>
        )}

        {/* SAVED JOBS TAB */}
        {activeTab === "saved" && (
        <div id="savedTab">
            <div className="data-list">
                {savedJobs.map(job => (
                    <div className="data-row" key={job.id}>

                        <div className="data-info">
                        <div className="data-title">{job.title}</div>

                        <div className="data-sub">
                            <span>{job.companyName}</span>
                            <span>·</span>
                            <span>{job.city}</span>
                            <span>·</span>
                            <span>{job.employmentType}</span>
                            <span>·</span>
                            <span>${job.salaryMin}-${job.salaryMax}</span>
                        </div>
                        </div>

                        <span className="data-date">
                            {new Date(job.createdAt).toLocaleDateString()}
                        </span>

                        <span className={`badge ${getStatusClass(job.status)}`}>
                            {job.status}
                        </span>

                    </div>
                    ))}
                </div>
        </div>
    
        )}

        {/*pagination, based on page count retuned form backedn(page=totalcount/10) create buttons,
       eacch button on click updates page, sets it to num(assigned page numebr to button) and triggers useeffect
       then useeffect passes in parameters in getJobs(), page is one of them, and page is set to num*/}

        {activeTab === "applications" && (
            <div className="pagination" style={{marginBottom:'100px'}}>
                {Array.from({ length: appTotalPages }, (_, i) => i + 1).map(num=>(
                    <button
                        key={num}
                        className={`page-btn ${appPage === num ? 'active' : ''}`}
                        onClick={() => setAppPage(num)}>
                        {num}
                    </button>
                ))}
            </div>
        )}

        {activeTab === "saved" && (
            <div className="pagination" style={{marginBottom:'100px'}}>
            {Array.from({ length: savedTotalPages }, (_, i) => i + 1).map(num=>(
                <button
                    key={num}
                    className={`page-btn ${savedPage === num ? 'active' : ''}`}
                    onClick={() => setSavedPage(num)}>
                    {num}
                </button>
            ))}
            </div>
        )}
    </div>
    </div>
</div>

    )
}