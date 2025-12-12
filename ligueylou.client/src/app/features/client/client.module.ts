import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientComponent } from './components/client/client.component';
import { ClientDashboardComponent } from './components/client-dashboard/client-dashboard.component';



@NgModule({
  declarations: [
    ClientComponent,
    ClientDashboardComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ClientModule { }
