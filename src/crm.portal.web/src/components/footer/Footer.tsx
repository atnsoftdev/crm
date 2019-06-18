import React from "react";
import { Container } from "reactstrap";

const footer = () => {
  return (
    <footer className="footer">
      <Container fluid className="clearfix">
        <span className="text-muted d-block text-center text-sm-left d-sm-inline-block">
          Copyright © 2019{" "} CRM. All rights reserved.
        </span>
      </Container>
    </footer>
  );
};

export default footer;
