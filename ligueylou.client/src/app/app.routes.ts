import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { RoleEnum } from './core/models/user.enums';
import { AdminDashboardComponent } from './features/admin/components/admin-dashboard/admin-dashboard.component';
import { AdminComponent } from './features/admin/components/admin/admin.component';
import { PrestataireComponent } from './features/prestataire/components/prestataire/prestataire.component';
import { PrestataireDashboardComponent } from './features/prestataire/components/prestataire-dashboard/prestataire-dashboard.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'admin',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.ADMIN] },
    component: AdminComponent,
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
    ]
  },
  {
    path: 'prestataire',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.PRESTATAIRE] },
    component: PrestataireComponent,
    children: [
      { path: 'dashboard', component: PrestataireDashboardComponent },
    ]
  },

  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
