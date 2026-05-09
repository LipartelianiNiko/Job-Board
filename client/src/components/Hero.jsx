  import { useAuth } from "../context/AuthContext";

  export default function Hero({onCreateJobClick, onLoginClick, totalCount}){ 
    const { user } = useAuth();
    return(

        <>
            <div className="hero">
                <div className="hero-inner">
                <div>
                    <div className="hero-label">🇬🇪 Georgia's Job Market</div>
                    <h1>Find your next<br/><em>Job</em><br/>in Georgia.</h1>
                    <p className="hero-sub">The platform built for  talent across Tbilisi and beyond.</p>
                    <div className="hero-cta">
                    <button className="btn btn-primary" onClick={()=>{}}>Browse Jobs</button>

                    {!user && (
                        <button className="btn btn-ghost" onClick={onLoginClick}>Post a Job →</button>
                    )}

                    {user && user.role === 'Employer' && (
                        <button className="btn btn-ghost" onClick={onCreateJobClick}>Post a Job →</button>
                    )}
                    </div>
                </div>
                <div className="hero-stats-panel">
                    <div className="stat"><div className="stat-num">{totalCount}</div><div className="stat-label">Open Roles</div></div>
                </div>
                </div>
            </div>
            
        </>
    );
  }