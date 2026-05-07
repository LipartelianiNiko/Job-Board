import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getJobsById } from '../api';

export default function JobDetails( ) {
    const [job, setJob] = useState(null);
    const { id } = useParams();
    console.log('id:', id);

    const navigate = useNavigate();

    useEffect(() => {
    getJobsById(id).then(res => {
        setJob(res.data);  
    });
    }, [id]);

    if (!job) return <div>Loading...</div>
  return(
    <>
    <div style={{ paddingTop: '60px' }}>
    <div className="detail-wrap">
    <div>

     <button className="back-btn" onClick={() => navigate('/')}>← Back to Jobs</button>
      <div className="detail-header">
        <div className="detail-logo" id="detailLogo">{job.companyName.substring(0, 2).toUpperCase()}</div>
        <div>
          <div className="detail-title" id="detailTitle">{job.title}</div>
          <div className="detail-company" id="detailCompany">{job.companyName} · {job.city}</div>
          <div className="job-tags" id="detailTags"></div>
        </div>
      </div>
      <div className="detail-section">
        <h3>About the Role</h3>
        <p id="detailDesc">{job.description}</p>
      </div>
    </div>

    <div className="detail-sidebar">
      <div className="sidebar-meta">
        <div className="meta-row"><span className="meta-label">Salary</span><span className="meta-value" id="detailSalary">${job.salaryMin} – ${job.salaryMax}</span></div>
        <div className="meta-row"><span className="meta-label">Type</span><span className="meta-value" id="detailType">{job.employmentType}</span></div>
        <div className="meta-row"><span className="meta-label">Category</span><span className="meta-value" id="detailCat">{job.category}</span></div>
        <div className="meta-row"><span className="meta-label">City</span><span className="meta-value" id="detailCity">{job.city}</span></div>
        <div className="meta-row"><span className="meta-label">Posted</span>
          <span className="meta-value" id="detailDate">
            {new Date(job.createdAt)
            .toLocaleDateString('en-GB', { day: 'numeric', month: 'short', year: 'numeric' })}
          </span>
        </div>
        <div className="meta-row"><span className="meta-label">Status</span><span className="badge badge-open">{job.status}</span></div>
      </div>
      <div className="sidebar-actions">
        <button className="btn btn-primary" style={{ width: '100%', justifyContent: 'center' }} >Apply Now</button>
        <button className="btn btn-ghost" id="detailSaveBtn" style={{ width: '100%', justifyContent: 'center' }} >♡ Save Job</button>
      </div>
    </div>
  </div>
</div>

    </>
  );
}