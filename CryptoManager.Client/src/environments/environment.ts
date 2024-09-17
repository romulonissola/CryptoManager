// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  api: {
    baseUrl: "https://192.168.1.82:62329/api",
    roboTraderBaseUrl: "https://192.168.1.82:7170/api",
    version: "1.0.0",
  },
  login: {
    path: "login",
    defaultRedirectTo: "dash",
    facebook: {
      appId: "1293953147938399",
    },
  },
  production: false,
};
