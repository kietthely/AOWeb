import logo from './logo.svg';
import './App.css';
//import Card from './components/Card'
import CardListSearch from './components/CardListSearch'
import { Link, Outlet } from 'react-router-dom';
import CardV2 from './components/CardV2'
import CardV3 from './components/CardV3'
function App() {
    return (

        <div className="App container">
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container-fluid">
                    <Link className="navbar-brand" href="#">AOWebApp</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav">
                            <Link className="nav-link active" to="/Home">
                                Home
                            </Link>
                            <Link className="nav-link" to="/Contact">
                                Contact
                            </Link>
                            <Link className="nav-link" to="/Products">
                                Products
                            </Link>

                        </ul>
                    </div>
                </div>
            </nav>
            <Outlet/>
            {/*<div className="bg-light py-1 mb-2">*/}
            {/*    <h2 className="text-center">Exanple application</h2>*/}
            {/*</div>*/}
            {/*<div className="row justify-content-center">*/}
            {/*    <CardListSearch />*/}
            {/*</div>*/}
        </div>
    
  );
}

export default App;
