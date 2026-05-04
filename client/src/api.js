//file for declarng api endpoints and functions that call them

import axios from 'axios';

//declare where to connect
const  API=axios.create({
    baseURL:'http://localhost:5220/api',
})

//get token and save in local storage
API.interceptors.request.use((config)=>{
    const token=localStorage.getItem('token');
    if(token){
        config.headers.Authorization=`Bearer ${token}`;
    }
    return config;
})

//AUTH endpoints, functions to call those endpoints
export const registerSeeker=(data)=>API.post('auth/register/seeker', data);
export const registerEmployer=(data)=>API.post('auth/register/employer', data);
export const login=(data)=>API.post('auth/login', data);
export const getMe=()=>API.get('auth/me');

//JOBS functions for calling jobs' endpoints
export const getJobs=(params)=>API.get('/jobs', {params});//params for including filtering
export const getJobsById=(id)=>API.get(`/jobs/${id}`);
export const createJob=(data)=>API.post('/jobs', data);
export const updateJob=(id, data)=>API.patch(`/jobs/${id}`, data);
export const updateJobStatus=(id, data)=>API.patch(`/jobs/${id}/status`, data);
export const deleteJob = (id)=>API.delete(`/jobs/${id}`);
export const getEmployerJobs = (params)=> API.get('/employer/jobs', {params});//params for filtering
export const getEmployerJobApplications=(jobId, params)=> API.get(`employer/jobs/${jobId}/applications`,{params});


//APLICATIONS 

export const applyToJob = (jobId, data )=>API.post(`/jobs/${jobId}/apply`, data);
export const getMyApplications = (params)=>API.get(`seeker/applications`, {params});
export const withdrawApplication = (id)=>API.delete(`/applications/${id}`);
export const updateApplicationStatus =(id, data)=>API.patch(`/applications/${id}/status`, data);

//SAVED JOBS
export const saveJob = (jobId)=>API.post(`/jobs/${jobId}/save`);
export const unsaveJob =  (jobId)=>API.delete(`/seeker/savedJobs/${jobId}`);
export const getSavedJobs=(params)=>API.get('/seeker/savedJobs', { params });

