export default function SearchSection({ 
  search, setSearch, 
  category, setCategory, 
  city, setCity, 
  employmentType, setEmploymentType 
}){
    return(
        <div className="search-section">
            <div className="search-inner">
            <div className="search-bar">
                <input 
                    className="search-input" type="text" placeholder="Search roles, companies, or skills..."
                    value={search} onChange={(e) => setSearch(e.target.value)}
                />
                
                <button className="btn btn-primary">Search</button>
            </div>
            <div className="filters">
                <span className="filter-label">Filter:</span>
                <button 
                    className={`filter-chip ${!category && !city && !employmentType ? 'active' : ''}`}
                    onClick={() => { setCategory(''); setCity(''); setEmploymentType(''); }}>
                All</button>

                <button 
                    className={`filter-chip ${category === 'Technology' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'Technology' ? '' : 'Technology')}>
                Technology</button>

                <button 
                    className={`filter-chip ${category === 'Finance' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'Finance' ? '' : 'Finance')}>
                Finance</button>

                <button 
                    className={`filter-chip ${category === 'Management' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'Management' ? '' : 'Management')}>
                Management</button>

                <button 
                    className={`filter-chip ${category === 'HR' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'HR' ? '' : 'HR')}>
                HR</button>

                <button 
                    className={`filter-chip ${category === 'Sales' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'Sales' ? '' : 'Sales')}>
                Sales</button>

                <button 
                    className={`filter-chip ${category === 'Legal' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'Legal' ? '' : 'Legal')}>
                Legal</button>

                <button 
                    className={`filter-chip ${category === 'Design' ? 'active' : ''}`}
                    onClick={() => setCategory(category === 'Design' ? '' : 'Design')}>
                Design</button>
                
                <button 
                    className={`filter-chip ${employmentType === 'Full-time' ? 'active' : ''}`}
                    onClick={() => setEmploymentType(employmentType === 'Full-time' ? '' : 'Full-time')}>
                Full-time</button>                
                
                <button 
                    className={`filter-chip ${employmentType === 'Part-time' ? 'active' : ''}`}
                    onClick={() => setEmploymentType(employmentType === 'Part-time' ? '' : 'Part-time')}>
                Part-time</button>

                <button 
                    className={`filter-chip ${employmentType === 'Internship' ? 'active' : ''}`}
                    onClick={() => setEmploymentType(employmentType === 'Internship' ? '' : 'Internship')}>
                Internship</button>

                <button 
                    className={`filter-chip ${employmentType === 'Contract' ? 'active' : ''}`}
                    onClick={() => setEmploymentType(employmentType === 'Contract' ? '' : 'Contract')}>
                Contract</button>

                <button 
                    className={`filter-chip ${city === 'Tbilisi' ? 'active' : ''}`}
                    onClick={() => setCity(city === 'Tbilisi' ? '' : 'Tbilisi')}>
                Tbilisi</button>

                <button 
                    className={`filter-chip ${city === 'Kutaisi' ? 'active' : ''}`}
                    onClick={() => setCity(city === 'Kutaisi' ? '' : 'Kutaisi')}>
                Kutaisi</button>

                <button 
                    className={`filter-chip ${city === 'Batumi' ? 'active' : ''}`}
                    onClick={() => setCity(city === 'Batumi' ? '' : 'Batumi')}>
                Batumi</button>

                <button 
                    className={`filter-chip ${city === 'Remote' ? 'active' : ''}`}
                    onClick={() => setCity(city === 'Remote' ? '' : 'Remote')}>
                Remote</button>
            </div>
            </div>
        </div>
    );

}