import React from "react";
import { BrowserRouter } from "react-router-dom";

import "./App.css";

import { DefaultLayout } from "./containers";
import { AppCtxProvider } from "./contexts";

export default () => {
  return (
    <>
      <BrowserRouter>
        <AppCtxProvider>
          <DefaultLayout />
        </AppCtxProvider>
      </BrowserRouter>
    </>
  );
};
