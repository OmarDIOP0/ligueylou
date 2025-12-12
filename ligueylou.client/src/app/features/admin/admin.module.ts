import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { AdminComponent } from './components/admin/admin.component';



@NgModule({
  declarations: [
    AdminDashboardComponent,
    AdminComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
