import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { catchError, tap, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const tokenService = inject(TokenService);
  return next(req).pipe(
    catchError(err => {

      if (err.status === 401) {
        tokenService.clearToken();
        router.navigate(['/login']);
      }

      if (err.status === 403) {
        router.navigate(['/unauthorized']);
      }

      return throwError(() => err);
    })
  );
};

