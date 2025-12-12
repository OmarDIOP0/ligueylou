import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService);
  const token = tokenService.token();

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
  return next(req);
};

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const tokenService = inject(TokenService);
  return next(req).pipe(
    // catchError est remplacé par rxjs pipe
    // Angular 19 : on utilise transform operators
    tap({
      error: (err: any) => {
        // Log interne
        console.error('API Error:', err);

        // Gestion des erreurs critiques
        if (err.status === 401) {
          // Token expiré ou non autorisé → purge token et redirige vers login
          tokenService.clearToken();
          router.navigate(['/login']);
        }

        if (err.status === 403) {
          alert('Accès refusé.');
        }

        // Autres erreurs : format user-friendly
        if (err.status >= 500) {
          alert('Erreur serveur, veuillez réessayer plus tard.');
        }
      }
    })
  );


}
