import { withdrawApplication } from "../api";

export default function ApplicationDetailsModal({onClose , app, onWithdraw }){


    async function handleWithdraw(appId) {
        if (!window.confirm('Withdraw this application?')) return;
        await withdrawApplication(app.id);
        onWithdraw(app.id);
    }

    return(
        <div className="modal-overlay" id="applyModal" onClick={onClose}>
            <div className="modal" onClick={e => e.stopPropagation()}>
                <button className="modal-close" onClick={onClose}>✕</button>
                <div className="modal-title">Application Details</div>
                <div className="modal-sub" id="applyJobName">{app.title}</div>
                <div className="form-group"><label className="form-label">Cover Letter <span style={{color:'var(--ink4)',fontWeight:'400'}}></span></label>
                     <p> {app.coverLetter}</p>                     
                </div>

                <div className="modal-title">{app.jobTitle}</div>
                <div className="modal-sub">{app.companyName}</div>

                

                <div className="meta-row">
                <span className="meta-label">Status</span>
                <span className="meta-value">{app.status}</span>
                </div>

                <div className="meta-row">
                <span className="meta-label">Applied</span>
                <span className="meta-value">{new Date(app.createdAt).toLocaleDateString()}</span>
                </div>

                <div className="modal-footer">
                    <button className="btn btn-ghost" style={{flex:1}} onClick={onClose}>Cancel</button>
                    <button className="btn btn-ghost" style={{flex:1,     backgroundColor: "#b91c1c", color: "white"}} onClick={()=>{handleWithdraw(app)}}>Widthdraw</button>

                </div>
            </div>
        </div>

        
    )
 
}
