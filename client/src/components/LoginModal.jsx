import { useAuth } from "../context/AuthContext";
import { useState } from "react";

export default function LoginModal({ onClose ,onSwitchToRegister }) {
  const { login } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showSeeker, setShowSeeker]=useState(true)



  async function handleSubmit() {
    await login({ email, password });
    onClose();
  }

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal" onClick={e => e.stopPropagation()}>
        <button className="modal-close" onClick={onClose}>✕</button>
        <div className="modal-title">Welcome back</div>
        <div className="modal-sub">Sign in to continue</div>
        <div className="auth-tabs">
            
          <button className={`auth-tab ${showSeeker === true ? 'active' : ''}`} onClick={() => {setShowSeeker(true)}}>Job Seeker</button>
          <button className={`auth-tab ${showSeeker === false ? 'active' : ''}`} onClick={() => {setShowSeeker(false)}}>Employer</button>
        </div>
        <div className="form-group">
          <label className="form-label">Email</label>
          <input className="form-input" type="email" value={email} onChange={e => setEmail(e.target.value)} placeholder="you@example.com" />
        </div>
        <div className="form-group">
          <label className="form-label">Password</label>
          <input className="form-input" type="password" value={password} onChange={e => setPassword(e.target.value)} placeholder="••••••••" />
        </div>
        <div className="modal-footer">
          <button className="btn btn-primary" style={{ flex: 1 }} onClick={handleSubmit}>Sign In</button>
        </div>
        <div className="divider">or</div>
        <button 
          style={{ color: 'var(--accent)', fontWeight: '500', background: 'none', border: 'none', cursor: 'pointer', padding: 0, fontSize: '13px' }} 
          onClick={onSwitchToRegister}
        >Register here</button>
      </div>
    </div>
  );
}