import { Injectable, signal } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { AuthResponse } from '../models/AuthResponse';
import { UtilisateurDto } from '../models/UtilisateurDto';

interface JwtPayload {
  role: number;
  exp: number;
}

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  private readonly TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly EXPIRED_TOKEN_KEY = 'expired_token';
  private USER_KEY = 'user_info';

  private _token = signal<string | null>(localStorage.getItem(this.TOKEN_KEY));
  private _refreshToken = signal<string | null>(localStorage.getItem(this.REFRESH_TOKEN_KEY));
  private _expiredToken = signal<string | null>(localStorage.getItem(this.EXPIRED_TOKEN_KEY));


  token = this._token.asReadonly();
  refreshToken = this._refreshToken.asReadonly();
  expiredToken = this._expiredToken.asReadonly();
  

  getToken(): string | null {
    return this._token();
  }

  getRefreshToken(): string | null {
    return this._refreshToken();
  }
  getUser(): UtilisateurDto | null {
    const u = localStorage.getItem(this.USER_KEY);
    return u ? JSON.parse(u) : null;
  }
  setAuth(res: AuthResponse) {
    console.log('Storing auth data:', res);
    this._token.set(res.token);
    this._refreshToken.set(res.refreshToken);
    localStorage.setItem(this.TOKEN_KEY, res.token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, res.refreshToken);
    localStorage.setItem(this.USER_KEY, JSON.stringify(res.utilisateur));
    console.log('Token stored:', localStorage.getItem(this.TOKEN_KEY)?.substring(0, 20) + '...');
    console.log('User stored:', localStorage.getItem(this.USER_KEY));
  }

  setToken(token: string, refresh: string) {
    this._token.set(token);

    localStorage.setItem(this.TOKEN_KEY, token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, refresh);
  }

  clearToken() {
    this._token.set(null);

    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
  }
  getPayload(): JwtPayload | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      return jwtDecode<JwtPayload>(token);
    } catch {
      return null;
    }
  }
  isExpired(): boolean {
    const payload = this.getPayload();
    if (!payload) return true;
    return Date.now() > payload.exp * 1000;
  }
  getRole(): number | null {
    return this.getPayload()?.role ?? null;
  }
}
