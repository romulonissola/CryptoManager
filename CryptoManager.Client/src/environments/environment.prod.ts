export const environment = {
  api: {
    baseUrl: "#{cryptomanager_server_base_url}#",
    version: "1.0.0"
  },
  login: {
    path: 'login',
    defaultRedirectTo: 'dash',
    facebook:{
      appId: '#{facebook_app_id}#'
    }
  },
  production: true
};
