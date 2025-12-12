import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrestataireComponent } from './components/prestataire/prestataire.component';
import { PrestataireDashboardComponent } from './components/prestataire-dashboard/prestataire-dashboard.component';



@NgModule({
  declarations: [
    PrestataireComponent,
    PrestataireDashboardComponent
  ],
  imports: [
    CommonModule
  ]
})
export class PrestataireModule { }
