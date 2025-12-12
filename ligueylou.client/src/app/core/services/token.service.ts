import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  private readonly TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly EXPIRED_TOKEN_KEY = 'expired_token';

  private _token = signal<string | null>(localStorage.getItem(this.TOKEN_KEY));
  private _refreshToken = signal<string | null>(localStorage.getItem(this.REFRESH_TOKEN_KEY));
  private _expiredToken = signal<string | null>(localStorage.getItem(this.EXPIRED_TOKEN_KEY));


  token = this._token.asReadonly();
  refreshToken = this._refreshToken.asReadonly();
  expiredToken = this._expiredToken.asReadonly();
  

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string, refresh: string, expired?:number | null) {
    this._token.set(token);
    this._refreshToken.set(refresh);
    //this._expiredToken.set(expired);

    localStorage.setItem(this.TOKEN_KEY, token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, refresh);
    //if (expired) sessionStorage.setItem('access_exp', expired.toString());
  }

  clearToken() {
    this._token.set(null);
    this._refreshToken.set(null);

    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
  }
}
