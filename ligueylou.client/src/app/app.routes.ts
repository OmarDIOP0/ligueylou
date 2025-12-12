import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { RoleEnum } from './core/models/user.enums';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'admin',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.ADMIN] }
  },
  {
    path: 'prestataire',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.PRESTATAIRE, RoleEnum.ADMIN] }
  },

  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
