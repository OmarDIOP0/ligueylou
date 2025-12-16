import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiEndpointsService {
  private base = "/api";

  auth = {
    register: `${this.base}/Utilisateurs/register`,
    login: `${this.base}/Utilisateurs/login`,
    refresh: `${this.base}/Utilisateurs/refresh-token`,
    profile: `${this.base}/Utilisateurs/profile`,
    logout: `${this.base}/Utilisateurs/logout`,
  };
  utilisateur = {
    create: `${this.base}/Utilisateurs`,
    update: (id: string) => `${this.base}/Utilisateurs/${id}`,
    delete: (id: string) => `${this.base}/Utilisateurs/${id}`,
    get: (id: string) => `${this.base}/Utilisateurs/${id}`
  };

}
