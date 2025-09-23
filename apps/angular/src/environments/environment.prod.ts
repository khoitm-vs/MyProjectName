import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'MyProjectName',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:7600/',
    redirectUri: baseUrl,
    clientId: 'MyProjectName_Angular',
    clientSecret: '1q2w3e*',
    responseType: 'code',
    scope: 'offline_access MyProjectNameIdentityService MyProjectNameAdministration MyProjectNameSaaS',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:7500',
      rootNamespace: 'MyProjectName',
    },
  },
} as Environment;
