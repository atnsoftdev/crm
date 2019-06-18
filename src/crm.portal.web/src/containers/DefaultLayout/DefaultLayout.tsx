import React from "react";

import { Header, Footer, Sidebar } from "components";
import Route from "Route";

const layout: React.FC = () => {
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

export default layout;
