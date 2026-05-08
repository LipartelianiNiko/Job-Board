import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
//import JobDetailPage from './pages/JobDetailPage';
import Dashboard from './pages/Dashboard';
import Navbar from './components/Navbar';
import JobDetails from './pages/JobDetailsPage';
import { useState } from 'react';
import LoginModal from './components/LoginModal';
import RegisterModal from './components/RegisterModal';
import CreateJobModal from './components/CreateJobModal';

import './App.css'

function App() {

  //modals
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false);
  const [showCreateJob, setShowCreateJob] = useState(false);


  return (
    <BrowserRouter>
      <Navbar 
        onLoginClick={() => setShowLogin(true)} 
        onRegisterClick={() => setShowRegister(true)} 
        onCreateJobClick={() => setShowCreateJob(true)}
      />
      <Routes>
        <Route path="/" element={
          <HomePage 
            onLoginClick={() => setShowLogin(true)} 
            onCreateJobClick={() => setShowCreateJob(true)}
          />} 
        />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/jobs/:id" element={<JobDetails/>} />
      </Routes>

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
      {showCreateJob && <CreateJobModal onClose={() => setShowCreateJob(false)} />}

    </BrowserRouter>
  );
}

export default App;