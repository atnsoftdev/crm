import { UserManagerSettings } from "oidc-client";

export const OidcConfig: UserManagerSettings = {
  client_id: "crm-spa",
  redirect_uri: "http://localhost:3000/authentication/callback",
  authority: "https://idp.lab-xyz.tk/auth/realms/master",
  response_type: "id_token token",
  post_logout_redirect_uri: "http://localhost:3000/",
  scope: "openid",  
  silent_redirect_uri: "http://localhost:3000/authentication/silent_callback",
  automaticSilentRenew: false,
  loadUserInfo: true  
};
