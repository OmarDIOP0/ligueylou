import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { JwtHelperService } from '@auth0/angular-jwt'; 

const jwtHelper = new JwtHelperService();
export const authGuard: CanActivateFn = (route, state) => {
  const tokenService = inject(TokenService);
  const router = inject(Router);

  const token = tokenService.getToken();
  if (!token) {
    return router.createUrlTree(['/login']);
  }
  if (jwtHelper.isTokenExpired(token)) {
    tokenService.clearToken();
    return router.createUrlTree(['/login']);
  }
  const allowedRoles = route.data?.['roles'] as number[] | undefined;
  if (allowedRoles && allowedRoles.length > 0) {
    const user = tokenService.getUser(); 
    if (!user || !allowedRoles.includes(user.role)) {
      return router.createUrlTree(['/unauthorized']);
    }
  }

  return true;
};
