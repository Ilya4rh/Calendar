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
          <Route path="/">
              <Route index element={<Promo />} />
              <Route
                  path="/MainPage"
                  element={
                  <AuthProvider>
                      <MainPage/>
                  </AuthProvider>
              }
              />
          </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default Router;
