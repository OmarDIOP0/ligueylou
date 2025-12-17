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
    getByEmail: (email: string) => `${this.base}/Utilisateurs/email/${email}`,
    getByTelephone: (telephone: string) => `${this.base}/Utilisateurs/telephone/${telephone}`,
    getAll: `${this.base}/Utilisateurs`,
  };

  client = {
    create: `${this.base}/Clients`,
    getAll: `${this.base}/Clients`,
    get: (id: string) => `${this.base}/Clients/${id}`,
    getByEmail: (email: string) => `${this.base}/Clients/email/${email}`,
    getByTelephone: (telephone: string) => `${this.base}/Clients/telephone/${telephone}`,
    update: (id: string) => `${this.base}/Clients/${id}`,
    delete: (id: string) => `${this.base}/Clients/${id}`,
    updateAdresse: (id: string) => `${this.base}/Clients/${id}/adresse`,
    createFavori: (clientId: string) => `${this.base}/Clients/${clientId}/favoris`,
    getFavoris: (clientId: string) => `${this.base}/Clients/${clientId}/favoris`,
    deleteFavori: (clientId: string, favoriId: string) => `${this.base}/Clients/${clientId}/favoris/${favoriId}`,
    getReservations: (clientId: string) => `${this.base}/Clients/${clientId}/reservations`,
    getEvaluations: (clientId: string) => `${this.base}/Clients/${clientId}/evaluations`,
    getFilter: (actif: boolean, email: string, telephone: string) => `${this.base}/Clients/filter?actif=${actif}&email=${email}&telephone=${telephone}`,
  };

  prestataires = {
    create: `${this.base}/Prestataires`,
    getAll: `${this.base}/Prestataires`,
    get: (id: string) => `${this.base}/Prestataires/${id}`,
    getByName: (name: string) => `${this.base}/Prestataires/search/${name}`,
    update: (id: string) => `${this.base}/Prestataires/${id}`,
    delete: (id: string) => `${this.base}/Prestataires/${id}`,
    updateAdresse: (id: string) => `${this.base}/Prestataires/${id}/adresse`,
    createSpecialite: (id: string) => `${this.base}/Prestataires/${id}/specialites`,
    createReservation: (id: string) => `${this.base}/Prestataires/${id}/reservations`,
    updateActivate: (id: string) => `${this.base}/Prestataires/${id}/activate`,
    isActif: (id: string) => `${this.base}/Prestataires/${id}/is-actif`,
    getScore: (id: string) => `${this.base}/Prestataires/${id}/score`,
    updateScore: (id: string) => `${this.base}/Prestataires/${id}/score`,
    getTop: (n: number) => `${this.base}/Prestataires/top/${n}`,
    getPerPage: (page: Int16Array, size: Int16Array) => `${this.base}/Prestataires/paged?page=${page}&size=${size}`,
    getFilter: (actif: boolean, ville: string, specialite: string) => `${this.base}/Prestataires/filter?actif=${actif}&ville=${ville}&specialite=${specialite}`,
  };
}
