import { useState } from "react";
import { updateJob } from "../api";


export default function EditJobModal({ onClose, job, onUpdated }) {

    const [title, setTitle] = useState(job.title);
    const [description, setDescription] = useState(job.description);
    const [category, setCategory] = useState(job.category);
    const [employmentType, setEmploymentType] = useState(job.employmentType);
    const [salaryMin, setSalaryMin] = useState(job.salaryMin || '');
    const [salaryMax, setSalaryMax] = useState(job.salaryMax || '');
    const [city, setCity] = useState(job.city || '');

    const [success, setSuccess] = useState(false);

    
    async function edit() {
    const jobData = { title, description, category, employmentType, salaryMin: Number(salaryMin), salaryMax: Number(salaryMax), city };
    const res = await updateJob(job.id, jobData);
    onUpdated(res.data);
    setSuccess(true);
    setTimeout(() => onClose(), 1500);
    }
    

     if (success) return (
    <div className="modal-overlay">
        <div className="modal" style={{ textAlign: 'center' }}>
        <div style={{ fontSize: '32px', marginBottom: '12px' }}>✓</div>
        <div className="modal-title">Job Edited</div>
        <div className="modal-sub">Good luck!</div>
        </div>
    </div>
    );


return(
    <>
    <div className="modal-overlay" id="createJobModal" onClick={onClose}>
    <div className="modal" onClick={e => e.stopPropagation()} style={{maxWidth:'520px'}}>
        <button className="modal-close" onClick={onClose}>✕</button>
        <div className="modal-title">Edit the Job</div>
        <div className="modal-sub">Edit the job</div>
        <div className="form-group"><label className="form-label">Job Title</label>
            <input className="form-input" type="text" placeholder="e.g. Senior Backend Developer" 
            value={title} 
            onChange={e => setTitle(e.target.value)} 
            />
        </div>
        <div className="form-group"><label className="form-label">Description</label>
            <textarea className="form-textarea" placeholder="Role overview, responsibilities, requirements..."
                value={description} 
                onChange={e => setDescription(e.target.value)} 
            />
        </div>
        <div className="form-row">
        <div className="form-group"><label className="form-label">Category</label>
            <select className="form-select" value={category} onChange={e => setCategory(e.target.value)}>
                <option value="Technology">Technology</option>
                <option value="Finance">Finance</option>
                <option value="Marketing">Marketing</option>
                <option value="Design">Design</option>
                <option value="Sales">Sales</option>
                <option value="HR">HR</option>
                <option value="Management">Management</option>
                <option value="Legal">Legal</option>
                <option value="Operations">Operations</option>
                <option value="Other">Other</option>
            </select>
        </div>
        <div className="form-group"><label className="form-label">Employment Type</label>
            <select className="form-select" value={employmentType} onChange={e => setEmploymentType(e.target.value)}>
                <option value="FullTime">Full-time</option>
                <option value="PartTime">Part-time</option>
                <option value="Contract">Contract</option>
                <option value="Internship">Internship</option>
            </select>
        </div>
        </div>
        <div className="form-row">
            <div className="form-group"><label className="form-label">Min Salary ($)</label>
                <input className="form-input" type="number" placeholder="2000"
                    value={salaryMin} 
                    onChange={e => setSalaryMin(e.target.value)}
                />
            </div>
            <div className="form-group"><label className="form-label">Max Salary ($)</label>
                <input className="form-input" type="number" placeholder="4000"
                    value={salaryMax} 
                    onChange={e => setSalaryMax(e.target.value)} 
                />
            </div>
        </div>
        <div className="form-group"><label className="form-label">City</label>
            <input className="form-input" type="text" placeholder="Tbilisi, Batumi, Remote..."
                value={city} onChange={e => setCity(e.target.value)}
            />
        </div>
        <div className="modal-footer">
        <button className="btn btn-primary" style={{flex:2}} onClick={edit}>Publish Edited Listing</button>
        </div>
    </div>
    </div>
</>
)
}