import Hero from '../components/Hero';
import SearchSection from '../components/Search';
import { useEffect, useState } from 'react';
import { getJobs } from '../api';
import JobCard from '../components/JobCard';


const categoryMap = {
  'Technology': 0, 'Finance': 1, 'Marketing': 2, 'Design': 3,
  'Sales': 4, 'HR': 5, 'Operations': 6, 'Legal': 7, 'Management': 8, 'Other': 9
};

const employmentTypeMap = {
  'Full-time': 0, 'Part-time': 1, 'Contract': 2, 'Internship': 3
};

export default function HomePage({ onLoginClick, onCreateJobClick }) {
  const [jobs, setJobs] = useState([]);
  const [totalCount, setTotalCount] = useState(0);
  const [page, setPage] = useState(1);

  //filters
  const [search, setSearch] = useState('');
  const [category, setCategory] = useState('');
  const [city, setCity] = useState('');
  const [employmentType, setEmploymentType] = useState('');
  const [totalPages, setTotalPages]=useState(1)


  //useeffect for filtering and for load
  useEffect(() => {
  getJobs({ //params
      search, 
      category: category ? categoryMap[category] : undefined,
      employmentType: employmentType ? employmentTypeMap[employmentType] : undefined,
      city, 
      page
  }).then(res => {
    console.log('category param:', category ? categoryMap[category] : undefined);
    console.log('employmentType param:', employmentType ? employmentTypeMap[employmentType] : undefined);
    setJobs(res.data.jobs);
    setTotalCount(res.data.totalCount);
    setPage(res.data.page);
    setTotalPages(res.data.totalPages);
    console.log("count: "+totalCount);
    console.log("total pages: "+totalPages);

  })
}, [search, category, city, employmentType, page, totalCount, totalPages]);




  return(
    <>
    <div style={{ paddingTop: '30px' }}>
    <Hero onLoginClick={onLoginClick} onCreateJobClick={onCreateJobClick}/>
    </div>
    <SearchSection
      search={search}
      setSearch={setSearch}
      category={category}
      setCategory={setCategory}
      city={city}
      setCity={setCity}
      employmentType={employmentType}
      setEmploymentType={setEmploymentType}
    />

    <div className="main-content">
        <div className="content-header">
          <span className="content-title">Latest Openings</span>
          <span className="content-count">{totalCount} POSITIONS</span>
        </div>
      <div className="jobs-list" id="jobsList">
        <div>
      <p>{totalCount} jobs</p>
      {jobs.map(job => (
        <div style={{ paddingTop: '8px' }}>
          <JobCard key={job.id} job={job} />
        </div>
      ))}
    </div>
      </div>
      {/*pagination, based on page count retuned form backedn(page=totalcount/10) create buttons,
       eacch button on click updates page, sets it to num(assigned page numebr to button) and triggers useeffect
       then useeffect passes in parameters in getJobs(), page is one of them, and page is set to num*/}
        <div className="pagination">
          {Array.from({ length: totalPages }, (_, i) => i + 1).map(num=>(
            <button
                key={num}
                className={`page-btn ${page === num ? 'active' : ''}`}
                onClick={() => setPage(num)}>
                {num}
            </button>
          ))}
      </div>
    </div>
    </>
  );
}