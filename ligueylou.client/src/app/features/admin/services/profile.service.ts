import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TokenService } from '../../../core/services/token.service';
import { ApiEndpointsService } from '../../../core/config/api-endpoints.service';
import { Observable } from 'rxjs';
import { UpdateUtilisateurDto, UtilisateurDto } from '../../../core/models/UtilisateurDto';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private http = inject(HttpClient);
  private tokenService = inject(TokenService);
  private endPoints = inject(ApiEndpointsService);

  getProfile(): Observable<UtilisateurDto> {
    return this.http.get<UtilisateurDto>(`${this.endPoints.utilisateur}/profile`);
  }
  updateProfile(id: string, payload: UpdateUtilisateurDto): Observable<UtilisateurDto> {
    return this.http.put<UtilisateurDto>(`${this.endPoints.utilisateur}/${id}`, payload);
  }

  deleteAccount(id: string): Observable<void> {
    return this.http.delete<void>(`${this.endPoints.utilisateur}/${id}`);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.endPoints.utilisateur}/logout`, {});
  }
}
