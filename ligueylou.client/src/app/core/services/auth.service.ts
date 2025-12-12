import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TokenService } from './token.service';
import { ApiEndpointsService } from '../config/api-endpoints.service';
import { catchError, throwError } from 'rxjs';
import { RegisterRequestDto } from '../models/RegisterRequestDto';
import { LoginRequestDto } from '../models/LoginRequestDto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private tokenService = inject(TokenService);
  private endPoints = inject(ApiEndpointsService);
  private AUTH_ENDPOINTS = this.endPoints.auth;

  register(payload: RegisterRequestDto) {
    return this.http.post(this.AUTH_ENDPOINTS.register, payload)
      .pipe(
        catchError(err => {
          console.error('Erreur API register', err);
          return throwError(() => new Error('Impossible de créer un compte avec ces informations.'));
        }
        ));
  }

  login(payload: LoginRequestDto) {
    return this.http.post(this.AUTH_ENDPOINTS.login, payload)
      .pipe(
        catchError(err => {
          console.error('Erreur API login', err);
          return throwError(() => new Error('Impossible de se connecter avec ces informations.'));
        }
        ));
  }


}
