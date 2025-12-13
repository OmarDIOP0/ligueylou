import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { JwtHelperService } from '@auth0/angular-jwt'; 
import { RoleEnum } from '../models/user.enums';

const jwtHelper = new JwtHelperService();
export const authGuard: CanActivateFn = (route, state) => {
  const tokenService = inject(TokenService);
  const router = inject(Router);

  const token = tokenService.getToken();
  if (!token) {
    return router.createUrlTree(['/admin/login']);
  }
  if (jwtHelper.isTokenExpired(token)) {
    tokenService.clearToken();
    return router.createUrlTree(['/admin/login']);
  }
  const allowedRoles = route.data?.['roles'] as RoleEnum[] | undefined;

  if (allowedRoles) {
    const user = tokenService.getUser();
    if (!user || !allowedRoles.includes(user.role)) {
      return router.createUrlTree(['/unauthorized']);
    }
  }


  return true;
};
