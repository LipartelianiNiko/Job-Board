import { useAuth } from "../context/AuthContext";
import { useState } from "react"
import { registerSeeker, registerEmployer } from '../api';

export default function RegisterModal({onClose}){
    const { login } = useAuth();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('seeker');
    const [fullName, setFullName] = useState('');
    const [companyName, setCompanyName] = useState('');
    
    async function handleSubmit() {
    if (role === 'seeker') {
        await registerSeeker({ fullName, email, password });
    } else {
        await registerEmployer({ fullName, email, password, companyName });
    }
    await login({ email, password }); // auto login after register
    onClose();
    }
    
    return(
        <>
        <div className="modal-overlay" id="registerModal" onClick={onClose}>
            <div className="modal" onClick={e => e.stopPropagation()}>
                <button className="modal-close" onClick={onClose}>✕</button>
                <div className="modal-title">Create account</div>
                <div className="modal-sub">Join Georgia's tech job market</div>
                <div className="role-selector">
                <div className={`role-option ${role === 'seeker' ? 'active' : ''}`} id="roleSeeker" onClick={()=>{setRole('seeker')}}><div className="role-icon">👤</div><div className="role-name">Job Seeker</div></div>
                <div className={`role-option ${role === 'employer' ? 'active' : ''}`} id="roleEmployer" onClick={()=>{setRole('employer')}}><div className="role-icon">🏢</div><div className="role-name">Employer</div></div>
                </div>
                <div className="form-group"><label className="form-label">Full Name</label>
                    <input className="form-input" type="text" value={fullName} onChange={e=> setFullName(e.target.value)} placeholder="Your name"/>
                </div>
                   {role === 'employer' && (
                    <div className="form-group">
                        <label className="form-label">Company Name</label>
                        <input className="form-input" type="text" value={companyName} onChange={e => setCompanyName(e.target.value)} placeholder="Your company" />
                    </div>
                    )}

                <div className="form-group"><label className="form-label">Email</label>
                    <input className="form-input" type="email" value={email} onChange={e => setEmail(e.target.value)} placeholder="you@example.com" />
                </div>
                <div className="form-group"><label className="form-label">Password</label>
                    <input className="form-input" type="password" value={password} onChange={e => setPassword(e.target.value)} placeholder="Minimum 8 characters" />
                </div>
                <div className="modal-footer"><button className="btn btn-primary" style={{flex:1}} onClick={handleSubmit}>Create Account</button></div>
            </div>         
        </div>
        </>
    )
}