import { useAuth} from "../AuthContext";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  function handleLogout() {
    logout();
    navigate('/');
  }

  return (
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
            <button className="btn btn-ghost">Sign In</button>
            <button className="btn btn-primary">Register</button>
          </>
        )}
      </div>
    </nav>
  );
}