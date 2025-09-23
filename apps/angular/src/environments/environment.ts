import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MyProjectName',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:7600/',
    redirectUri: baseUrl,
    clientId: 'MyProjectName_Angular',
    responseType: 'code',
    scope: 'MyProjectNameIdentityService MyProjectNameAdministration MyProjectNameSaaS',
    requireHttps: false,
  },
  apis: {
    default: {
      url: 'https://localhost:7500',
      rootNamespace: 'MyProjectName',
    },
  },
} as Environment;
