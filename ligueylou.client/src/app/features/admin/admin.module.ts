import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { AdminComponent } from './components/admin/admin.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { CustomizerPanelComponent } from './utils/customizer-panel/customizer-panel.component';
import { VerticalHeaderComponent } from './utils/vertical-header/vertical-header.component';
import { VerticalSidebarComponent } from './utils/vertical-sidebar/vertical-sidebar.component';
import { DemandesComponent } from './components/demandes/demandes.component';
import { ClientsComponent } from './components/clients/clients.component';
import { PrestatairesComponent } from './components/prestataires/prestataires.component';
import { PaiementsComponent } from './components/paiements/paiements.component';
import { ServicesComponent } from './components/services/services.component';
import { ProfileComponent } from './components/profile/profile.component';



@NgModule({
  declarations: [
    AdminDashboardComponent,
    AdminComponent,
    LoginComponent,
    RegisterComponent,
    CustomizerPanelComponent,
    VerticalHeaderComponent,
    VerticalSidebarComponent,
    DemandesComponent,
    ClientsComponent,
    PrestatairesComponent,
    PaiementsComponent,
    ServicesComponent,
    ProfileComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
