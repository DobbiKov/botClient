import logo from './logo.svg';
import './App.css';
import { useRoutes } from './routes';
import { useAuth } from './hooks/auth.hook';

import { AuthContext } from './context/AuthContext';

import { NavComponent } from './components/NavComponent/index';

function App() {
  const routes = useRoutes();
  const {token, login, logout} = useAuth();
  const isAuth = !!token;
  return (
    <AuthContext.Provider value={{
      login, logout, token, isAuth
    }} >
      <div className="App">
        <NavComponent/>
        {routes}
      </div>
    </AuthContext.Provider>
  );
}

export default App;
