  
  export default function Hero(){ 
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
                    <button className="btn btn-ghost" onClick={()=>{}}>Post a Job →</button>
                    </div>
                </div>
                <div className="hero-stats-panel">
                    <div className="stat"><div className="stat-num">0</div><div className="stat-label">Open Roles</div></div>
                    <div className="stat"><div className="stat-num">0</div><div className="stat-label">Companies</div></div>
                    <div className="stat"><div className="stat-num">0</div><div className="stat-label">New Today</div></div>
                    <div className="stat"><div className="stat-num">0</div><div className="stat-label">Response Rate</div></div>
                </div>
                </div>
            </div>
        </>
    );
  }