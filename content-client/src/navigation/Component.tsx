import './Component.css';
import React from "react";
import { IRouteMappingProps } from "../models/IRouteMappingProps";

export default function Component(props: IRouteMappingProps) {
  const { mapping } = props;
  return (
    <div className="container">

      <mapping.component
        mappings={mapping.mappings}
        {...props} />

    </div>
  );
}
