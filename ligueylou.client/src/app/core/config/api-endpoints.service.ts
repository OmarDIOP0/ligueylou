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
  };
  utilisateur = {
    create: `${this.base}/Utilisateurs`,
    update: (id: string) => `${this.base}/Utilisateurs/${id}`,
    delete: (id: string) => `${this.base}/Utilisateurs/${id}`,
    get: (id: string) => `${this.base}/Utilisateurs/${id}`,
    profile: `${this.base}/Utilisateurs/profile`,
    logout: `${this.base}/Utilisateurs/logout`,
  };
  client = {
    create: `${this.base}/Clients`,
    getAll: `${this.base}/Clients`,
    get: (id: string) => `${this.base}/Clients`,
    getByEmail: (email: string) => `${this.base}/Clients`,
    getByTelephone: (telephone: string) => `${this.base}/Clients`,
    update: (id: string) => `${this.base}/Clients`,
    delete: (id: string) => `${this.base}/Clients`,
    updateAdresse: (id: string) => `${this.base}/Clients/${id}/adresse`,
    createFavori: (clientId: string) => `${this.base}/Clients/${clientId}/favoris`,
    getFavoris: (clientId: string) => `${this.base}/Clients/${clientId}/favoris`,
    deleteFavori: (clientId: string, favoriId: string) => `${this.base}/Clients/${clientId}/favoris/${favoriId}`,
    getReservations: (clientId: string) => `${this.base}/Clients/${clientId}/reservations`,
    getEvaluations: (clientId: string) => `${this.base}/Clients/${clientId}/evaluations`,
    getFilter: (actif: boolean, email: string, telephone: string) => `${this.base}/Clients/filter`,
  }
}
