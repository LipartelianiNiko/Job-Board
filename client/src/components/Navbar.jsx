import { useAuth} from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import LoginModal from "./LoginModal";
import RegisterModal from "./RegisterModal";

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

const [showLogin, setShowLogin] = useState(false);
const [showRegister, setShowRegister] = useState(false);


  function handleLogout() {
    logout();
    navigate('/');
  }

  return (
    <>
    <nav>
      <div className="logo" onClick={() => navigate('/')}>
        <div className="logo-mark">K</div>
        Kariera
      </div>

      <div className="nav-actions">
        {user ? (
          <>
            <div className="user-menu">
              <div className="user-avatar">{user.fullName[0]}</div>
              <span className="user-name">{user.fullName}</span>
            </div>
            <button className="btn btn-ghost" onClick={() => navigate('/dashboard')}>Dashboard</button>
            <button className="btn btn-ghost" onClick={handleLogout}>Sign Out</button>
          </>
        ) : (
          <>
            <button className="btn btn-ghost" onClick={() => { console.log('login clicked'); setShowLogin(true); }}>Sign In</button>
            <button className="btn btn-primary" onClick={()=>setShowRegister(true)}>Register</button>

          </>
        )}
      </div>
    </nav>
    
    {showLogin && (
      <LoginModal 
        onClose={() => setShowLogin(false)} 
        onSwitchToRegister={() => { 
          setShowLogin(false); 
          setShowRegister(true); 
        }} 
      />
    )}     

    {showRegister && <RegisterModal onClose={() => setShowRegister(false)} />}

    </>
  );
}