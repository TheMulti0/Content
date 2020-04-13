import React from "react";
import { Route } from 'react-router-dom';
import Component from "./Component";
import { IRouteMappingProps } from "../models/IRouteMappingProps";

export default function ComponentRoute(props: IRouteMappingProps) {
  const { mapping } = props;
  return (
    <Route
      exact
      path={ mapping.path }
      render={ renderProps => <Component mapping={mapping} {...renderProps} /> } />
  );
}

