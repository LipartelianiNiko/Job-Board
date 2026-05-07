import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
//import JobDetailPage from './pages/JobDetailPage';
import Dashboard from './pages/Dashboard';
import Navbar from './components/Navbar';
import JobDetails from './pages/JobDetailsPage';


import './App.css'

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/jobs/:id" element={<JobDetails/>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;