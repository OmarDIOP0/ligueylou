import { Routes } from '@angular/router';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { RoleEnum } from './core/models/user.enums';
import { AdminComponent } from './features/admin/components/admin/admin.component';
import { PrestataireComponent } from './features/prestataire/components/prestataire/prestataire.component';
import { PrestataireDashboardComponent } from './features/prestataire/components/prestataire-dashboard/prestataire-dashboard.component';
import { ClientComponent } from './features/client/components/client/client.component';
import { ClientDashboardComponent } from './features/client/components/client-dashboard/client-dashboard.component';
import { LoginComponent } from './features/admin/components/auth/login/login.component';
import { AdminDashboardComponent } from './features/admin/components/admin-dashboard/admin-dashboard.component';
import { DemandesComponent } from './features/admin/components/demandes/demandes.component';
import { PaiementsComponent } from './features/admin/components/paiements/paiements.component';
import { ProfileComponent } from './features/admin/components/profile/profile.component';

export const routes: Routes = [
  { path: 'admin/login', component: LoginComponent },
  //{ path: 'register', component: RegisterComponent },
  {
    path: 'admin',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.ADMIN] },
    component: AdminComponent,
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'demandes', component: DemandesComponent },
      { path: 'clients', component: ClientComponent },
      { path: 'prestataires', component: PrestataireComponent },
      { path: 'paiements', component: PaiementsComponent },
      //{ path: 'services', component: ServicesComponent }
    ]
  },
  {
    path: 'prestataire',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.PRESTATAIRE] },
    component: PrestataireComponent,
    children: [
      { path: 'dashboard', component: PrestataireDashboardComponent }
    ]
  },
  {
    path: 'client',
    canActivate: [authGuard],
    data: { roles: [RoleEnum.CLIENT] },
    component: ClientComponent,
    children: [
      { path: 'dashboard', component: ClientDashboardComponent },
    ]
  },

  { path: '', redirectTo: 'admin/login', pathMatch: 'full' }
];
