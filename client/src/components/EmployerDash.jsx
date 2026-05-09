import { useEffect, useState } from "react";
import { getEmployerJobs, updateJobStatus } from "../api";
import { getEmployerJobApplications } from "../api";
import { useSearchParams } from 'react-router-dom';
import { updateApplicationStatus } from "../api";
import { deleteJob } from "../api";
import EditJobModal from "./editJobModal";


export default function EmployerDashboard({ onCreateJobClick }){
    const [editingJob, setEditingJob] = useState(null);
    const [jobs, setJobs]=useState([]);
    const [applications, setApplications]=useState([]);

    //for returnng applicatns on a lisingg
    const [selectedJobId, setSelectedJobId] = useState(null);


    //for swithcing tabs
    const [searchParams, setSearchParams] = useSearchParams();
    const activeTab = searchParams.get('tab') || 'listings';

    const [listingsPage, setListingsPage] = useState(1);
    const [appsPage, setAppsPage] = useState(1);

    const [listingsTotalPages, setListingsTotalPages] = useState(1);
    const [appsTotalPages, setAppsTotalPages] = useState(1);

    //useeffect for lisitngs
    useEffect(() => {
        getEmployerJobs({page:listingsPage})
        .then(res => {
            setJobs(res.data.jobs);
            setListingsTotalPages(res.data.totalPages);
            setListingsPage(res.data.page);
        })
        .catch(err => console.log(err));
    }, [listingsPage]);

    useEffect(() => {
        if (!selectedJobId) return;
        getEmployerJobApplications(selectedJobId, { page: appsPage })
            .then(res => {
            setApplications(res.data.applications);
            setAppsTotalPages(res.data.totalPages);
            })
            .catch(err => console.log(err));
    }, [selectedJobId, appsPage]);



    function getStatusClassJobs(status) {
        switch (status) {
            case "Open":
            return "badge-open";
            case "Draft":
            return "badge-draft";
            case "Closed":
            return "badge-closed";
            default:
            return "badge-draft";
        }
    }

    async function  handleStatusChange(appId, statusUpdate) {
        if (!window.confirm('Change Status of this application?')) return;

        await updateApplicationStatus(appId, {status: statusUpdate});
        setApplications(prev => prev.map(a => 
        a.id === appId ? { ...a, status: statusUpdate } : a
        ));   
    }

    async function  handleJobStatusChange(jobId, statusUpdate) {
        if (!window.confirm('Change Status of this application?')) return;

        await updateJobStatus(jobId, {status: statusUpdate});
        setJobs(prev => prev.map(a => 
        a.id === jobId ? { ...a, status: statusUpdate } : a
        ));   
    }

    async function handleDeleteJOb(jobId) {
        if (!window.confirm('Delete this Job?')) return;
        await deleteJob(jobId);
        setJobs(prev => prev.filter(j => j.id !== jobId));
    }

    return(
    <>
    <div style={{ paddingTop: "80px" }}>
    <div  id="employerDash">
        <div className="dashboard-wrap">
            <div className="dash-header">
            <div><div className="dash-title">Employer Dashboard</div><div className="dash-sub">Manage your listings and review applications</div></div>
            <button className="btn btn-primary" onClick={onCreateJobClick}>+ Post New Job</button>
            </div>
            <div className="dash-tabs">
            <button className={`dash-tab ${activeTab === 'listings' ? 'active' : ''}`} onClick={() => setSearchParams({ tab: 'listings' })}>My Listings</button>
            <button className={`dash-tab ${activeTab === 'applications' ? 'active' : ''}`} onClick={() => setSearchParams({ tab: 'applicants' })}>Applicants</button>
            </div>

            {/* LISTINGS TAB */}
            {activeTab === "listings" && (
            <div id="listingsTab">
                <div className="data-list">
                    {jobs.map(job => (
                    <div className="data-row"  style={{ cursor: 'pointer' }} key={job.id} 
                        onClick={() => {setSelectedJobId(job.id);setSearchParams({ tab: 'applicants' });
                    }}>

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
                            applicants
                        </span>


                        <select 
                            className={`badge ${getStatusClassJobs(job.status)}`}
                            value={job.status}
                            onClick={(e) => e.stopPropagation()}
                            onChange={(e)=>{e.stopPropagation(); handleJobStatusChange(job.id, e.target.value)}}
                            >
                                <option value="Draft">Draft</option>
                                <option value="Open">Open</option>
                                <option value="Closed">Closed</option>
                        </select>

                        <button className="icon-btn" onClick={(e) => { e.stopPropagation(); setEditingJob(job); }}>
                            ✎
                        </button>

                        <button className="icon-btn icon-btn-danger" onClick={(e) => { e.stopPropagation(); handleDeleteJOb(job.id)}}>
                            ✕
                        </button>

                        </div>

                    </div>
                ))}
                </div>
            </div>
            )}

            {activeTab === "applicants" && (
            <div id="applicantsTab" >
                {!selectedJobId ? (
                    <div className="empty-state">
                        <div className="empty-icon">📋</div>
                        <div className="empty-text">Select a listing to view applicants</div>
                    </div>
                ) : (
                <div className="data-list">
                    {applications.map(app => (
                            <div className="data-row" key={app.id}>
                                <div className="applicant-avatar">
                                {app.seekerName.substring(0, 2).toUpperCase()}
                                </div>
                                <div className="data-info">
                                <div className="data-title">{app.seekerName}</div>
                                <div className="data-sub">
                                    <span>{app.seekerEmail}</span>
                                    <span>·</span>
                                    <span>{new Date(app.appliedAt).toLocaleDateString()}</span>
                                </div>
                                </div>
                                <select 
                                className="status-select"
                                value={app.status}
                                onChange={(e)=>{handleStatusChange(app.id, e.target.value)}}
                                >
                                    <option value="Pending">Pending</option>
                                    <option value="Reviewed">Reviewed</option>
                                    <option value="Shortlisted">Shortlisted</option>
                                    <option value="Rejected">Rejected</option>
                                    <option value="Accepted">Accepted</option>
                                </select>
                            </div>
                    ))}{/*map*/}
                    </div>
                )}{/*conditional*/}
            </div>
            )}
        </div>
       

        {/*PAGINATION FOR LITINGS*/}
        {activeTab === "listings" && (
            <div className="pagination" style={{marginBottom:'100px'}}>
            {Array.from({ length: listingsTotalPages }, (_, i) => i + 1).map(num=>(
                <button
                    key={num}
                    className={`page-btn ${listingsPage === num ? 'active' : ''}`}
                    onClick={() => setListingsPage(num)}>
                    {num}
                </button>
            ))}
            </div>
        )}

        
        {/*PAGINATION FOR APPLICANTS*/}
        {activeTab === "applicants" && (
            <div className="pagination" style={{marginBottom:'100px'}}>
            {Array.from({ length: appsTotalPages }, (_, i) => i + 1).map(num=>(
                <button
                    key={num}
                    className={`page-btn ${appsPage === num ? 'active' : ''}`}
                    onClick={() => setAppsPage(num)}>
                    {num}
                </button>
            ))}
            </div>
        )}
        
    </div>
    </div>
    {editingJob && <EditJobModal job={editingJob} onClose={() => setEditingJob(null)} onUpdated={(updatedJob) => {
        setJobs(prev => prev.map(j => j.id === updatedJob.id ? updatedJob : j));
        setEditingJob(null);
    }} />}
    </>
    )
}