import { Config } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'LeesStore',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44360',
    redirectUri: baseUrl,
    clientId: 'LeesStore_App',
    responseType: 'code',
    scope: 'offline_access LeesStore',
  },
  apis: {
    default: {
      url: 'https://localhost:44360',
      rootNamespace: 'LeesStore',
    },
  },
} as Config.Environment;
