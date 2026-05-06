import { createContext, useContext, useState, useEffect } from 'react';
import { getMe, login as apiLogin } from './api';

// 1. create the context object
const AuthContext = createContext(null);

// 2. provider component - wraps the whole app, holds the state
export function AuthProvider({ children }) {
  const [user, setUser] = useState(null); //usestate is used to dynamically assign values to variables, one declaration is enough to do so continously
  const [loading, setLoading] = useState(true);

  // on app load: if token exists, restore session
  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      getMe()//API endpoint call
        .then(res => setUser(res.data))//like try catch, is succesful 'then' this, if not 'catch'
        .catch(() => localStorage.removeItem('token'))// if not 'catch'
        .finally(() => setLoading(false));//either way 'finally' this, runs 
    } else {
      setLoading(false);
    }
  }, []);//in braces we specify on which  data's updates to listen and on each update code block runs. 
  // '[]' means it runs once only when page is loaded.

  //set token on login and restore session, no need on registration because there was no session.
  async function login(data) {
    const res = await apiLogin(data);//login from api.js imported as apiLogin, same function , different name.
    localStorage.setItem('token', res.data.token);
    const me = await getMe();
    setUser(me.data);
    return me.data;
  }

  //remove token on logout
  function logout() {
    localStorage.removeItem('token');
    setUser(null);
  }

  return (
    <AuthContext.Provider value={{ user, login, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
}

// 3. custom hook - how any component accesses the context
export function useAuth() {
  return useContext(AuthContext);
}