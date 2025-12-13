import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { AdminComponent } from './components/admin/admin.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { CustomizerPanelComponent } from './utils/customizer-panel/customizer-panel.component';
import { VerticalHeaderComponent } from './utils/vertical-header/vertical-header.component';
import { VerticalSidebarComponent } from './utils/vertical-sidebar/vertical-sidebar.component';



@NgModule({
  declarations: [
    AdminDashboardComponent,
    AdminComponent,
    LoginComponent,
    RegisterComponent,
    CustomizerPanelComponent,
    VerticalHeaderComponent,
    VerticalSidebarComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
