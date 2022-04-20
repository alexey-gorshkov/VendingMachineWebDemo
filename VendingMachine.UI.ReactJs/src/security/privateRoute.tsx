import React from "react";
import { Navigate } from "react-router-dom";
import securityUtils from "./securityUtils";

const PrivateRoute = (props: any) => {
  const isAuth = securityUtils.isAuthenticated();
  console.log("isAuth", isAuth);
  return isAuth ? props.children : <Navigate to="/login" />;
};

export default PrivateRoute;
