import React from "react";
import { Route } from "react-router-dom";

import { OidcCallback, Dashboard, NotAuthenticated, OidcSilentCallback } from "./components";
import WithOidcSecure from "./hoc/WithOidcSecure";

export default () => {
  return (
    <>
      <Route exact path="/" component={WithOidcSecure(Dashboard)} />
      <Route path="/leads" component={WithOidcSecure(Dashboard)} />
      <Route path="/authentication/callback" component={OidcCallback} />
      <Route path="/authentication/silent_callback" component={OidcSilentCallback} />
      <Route path="/authentication/401" component={NotAuthenticated} />
    </>
  );
};
