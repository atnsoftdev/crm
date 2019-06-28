import { UserManagerSettings } from "oidc-client";

let portalUrl = window.location.origin;

export const OidcConfig: UserManagerSettings = {
  client_id: "crm-spa",
  redirect_uri: `${portalUrl}/authentication/callback`,
  authority: "/authority",
  response_type: "id_token token",
  post_logout_redirect_uri: `${portalUrl}/`,
  scope: "openid",
  silent_redirect_uri: `${portalUrl}/authentication/silent_callback`,
  automaticSilentRenew: false,
  loadUserInfo: true
};
