import { useAuth } from '../context/AuthContext';
import SeekerDashboard from '../components/SeekerDash';
import EmployerDashboard from '../components/EmployerDash';



export default function Dashboard() {

  const { user } = useAuth();

  console.log(user);

  if (!user) {
    return <div>Please log in</div>;
  }

  if (user.role === 'Seeker') {
    return <SeekerDashboard />;
  }

  if (user.role === 'Employer') {
    return <EmployerDashboard />;
  }

  return <div>Unknown role</div>;
}