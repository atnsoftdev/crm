import React from "react";
import { Route } from "react-router-dom";

import { OidcCallback, Dashboard, NotAuthenticated } from "./components";
import WithOidcSecure from "./hoc/WithOidcSecure";

export default () => {
  return (
    <>
      <Route path="/abc" component={WithOidcSecure(Dashboard)} />
      <Route path="/authentication/callback" component={OidcCallback}></Route>
      <Route path="/authentication/401" component={NotAuthenticated}></Route>
    </>
  );
};
