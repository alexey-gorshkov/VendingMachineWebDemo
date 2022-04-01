import React from "react";
import { Navigate, Route } from "react-router-dom";

const RestrictedRoute = (props: any) => {
  const token = localStorage.getItem('auth');
  console.log("token",token); 
  return !token ? props.children : <Navigate to="/" />;
};

export default RestrictedRoute;
