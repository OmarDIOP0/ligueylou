import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  private readonly TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';

  private _token = signal<string | null>(localStorage.getItem(this.TOKEN_KEY));
  private _refreshToken = signal<string | null>(localStorage.getItem(this.REFRESH_TOKEN_KEY));

  token = this._token.asReadonly();
  refreshToken = this._refreshToken.asReadonly();

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string, refresh: string) {
    this._token.set(token);
    this._refreshToken.set(refresh);

    localStorage.setItem(this.TOKEN_KEY, token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, refresh);
  }

  clearToken() {
    this._token.set(null);
    this._refreshToken.set(null);

    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
  }
}
