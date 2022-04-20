import React from "react";
import { Navigate } from "react-router-dom";
import securityUtils from "./securityUtils";

const RestrictedRoute = (props: any) => {
  const isAuth = securityUtils.isAuthenticated();
  console.log("isAuth", isAuth);
  return !isAuth ? props.children : <Navigate to="/" />;
};

export default RestrictedRoute;
