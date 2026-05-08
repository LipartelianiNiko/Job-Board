import { useState } from "react";
import { applyToJob } from "../api";


export default function ApplyModal({onClose , jobId}){
    const [coverLetter, setCoverLetter]=useState('')

    //for successs message
    const [success, setSuccess] = useState(false);

    async function submitApplication() {
    await applyToJob(jobId, { coverLetter });
    setSuccess(true);
    setTimeout(() => {
        onClose();
    }, 1500);
    }

    if (success) return (
    <div className="modal-overlay">
        <div className="modal" style={{ textAlign: 'center' }}>
        <div style={{ fontSize: '32px', marginBottom: '12px' }}>✓</div>
        <div className="modal-title">Application Submitted</div>
        <div className="modal-sub">Good luck!</div>
        </div>
    </div>
    );

    return(
        <div className="modal-overlay" id="applyModal" onClick={onClose}>
            <div className="modal" onClick={e => e.stopPropagation()}>
                <button className="modal-close" onClick={onClose}>✕</button>
                <div className="modal-title">Apply for role</div>
                <div className="modal-sub" id="applyJobName">Senior Backend Developer at TBC Tech</div>
                <div className="form-group"><label className="form-label">Cover Letter <span style={{color:'var(--ink4)',fontWeight:'400'}}>(optional)</span></label>
                    <textarea className="form-textarea" placeholder="Tell them why you're a great fit..."
                        value={coverLetter} 
                        onChange={e => setCoverLetter(e.target.value)} 
                    />
                </div>

                <div className="form-group"><label className="form-label">Resume URL <span style={{color:'var(--ink4)',fontWeight:'400'}}>(optional)</span></label>
                    <input className="form-input" type="url" placeholder="Link to your CV or LinkedIn"/>
                </div>

                <div className="modal-footer">
                    <button className="btn btn-ghost" style={{flex:1}} onClick={onClose}>Cancel</button>
                    <button className="btn btn-primary" style={{flex:2}} onClick={submitApplication}>Submit Application</button>
                </div>
            </div>
        </div>

        
    )
 
}
