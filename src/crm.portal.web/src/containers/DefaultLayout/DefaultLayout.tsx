import React from "react";
import Route from "Route";

import { Header, Footer, Sidebar } from "components";

export default () => {
  return (
    <div className="container-scroller">
      <Header />
      <div className="container-fluid page-body-wrapper">
        <Sidebar />
        <div className="main-panel">
          <div className="content-wrapper" />
          <Route />
          <Footer />
        </div>
      </div>
    </div>
  );
};
