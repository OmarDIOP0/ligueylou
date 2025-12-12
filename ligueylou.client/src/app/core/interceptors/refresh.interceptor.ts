import { HttpClient, HttpInterceptorFn } from '@angular/common/http';
import { TokenService } from '../services/token.service';
import { inject } from '@angular/core';
import { catchError, switchMap, tap, throwError } from 'rxjs';

export const refreshInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService);
  const http = inject(HttpClient);

  return next(req).pipe(
    catchError(err => {
      if (err.status !== 401) return throwError(() => err);

      const refreshToken = tokenService.getRefreshToken();
      if (!refreshToken) {
        tokenService.clearToken();
        return throwError(() => err);
      }

      return http.post<any>('/api/Utilisateurs/refresh-token', {
        refreshToken
      }).pipe(
        tap(res => tokenService.updateTokens(res.token, res.refreshToken)),
        switchMap(res => {
          const retryReq = req.clone({
            setHeaders: {
              Authorization: `Bearer ${res.token}`
            }
          });
          return next(retryReq);
        })
      );
    })
  );
};
