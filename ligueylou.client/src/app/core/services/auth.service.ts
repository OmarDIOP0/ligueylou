import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TokenService } from './token.service';
import { ApiEndpointsService } from '../config/api-endpoints.service';
import { catchError, tap, throwError } from 'rxjs';
import { RegisterRequestDto } from '../models/RegisterRequestDto';
import { LoginRequestDto } from '../models/LoginRequestDto';
import { AuthResponse } from '../models/AuthResponse';

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
    return this.http.post < AuthResponse>(this.AUTH_ENDPOINTS.login, payload)
      .pipe(
        tap(res => this.tokenService.setAuth(res)),
        catchError(err => throwError(() => err))
      );
  }
  handleAuthSuccess(res: AuthResponse) {
    this.tokenService.setToken(res.token, res.refreshToken);
  }

  logout() {
    this.tokenService.clearToken();
  }

}
