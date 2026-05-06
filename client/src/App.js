import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
//import JobDetailPage from './pages/JobDetailPage';
import Dashboard from './pages/Dashboard';
import Navbar from './components/Navbar';
import Hero from './components/Hero';

import './App.css'

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Hero/>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;