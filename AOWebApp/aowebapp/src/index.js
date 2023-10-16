import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import CardListSearch from './components/CardListSearch'
import Home from './routes/Home';
import Contact from './routes/Contact'
import CardDetail from './components/CardDetail'
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <BrowserRouter>
            
            <Routes>
                <Route path="/" element={<App />}>
                    <Route path="/" element={<Home />} />
                    <Route path="/Home" element={<Home />} />
                    <Route path="/Contact" element={<Contact />} />
                    <Route path="/Products" element={<CardListSearch />} />
                    <Route path="" element={<Home />} />
                    <Route path="Products/:itemId" element={<CardDetail />} />
                    <Route path="*" element={<Home />} />
                </Route>
                
            </Routes>

        </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
