import React from 'react';
import  './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import {MainPage} from "./MainPage/MainPage";
import AuthProvider from "./AuthProvider";
import {Promo} from "./Promo/Promo";

function Router() {
  return (
    <BrowserRouter>
      <Routes>
          <Route
              path = '/'
              element = {<Promo/>}
          />
          <Route
              path="/MainPage"
              element={<AuthProvider component={MainPage()}/>}
          />
      </Routes>
    </BrowserRouter>
  );
}

export default Router;
