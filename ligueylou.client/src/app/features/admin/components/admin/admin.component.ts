import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { VerticalSidebarComponent } from '../../utils/vertical-sidebar/vertical-sidebar.component';
import { VerticalHeaderComponent } from '../../utils/vertical-header/vertical-header.component';
import { CustomizerPanelComponent } from '../../utils/customizer-panel/customizer-panel.component';
import { CustomizerService } from '../../utils/customizer.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    VerticalSidebarComponent,
    VerticalHeaderComponent,
    CustomizerPanelComponent,
    //FooterPanelComponent
  ],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {
  constructor(public customizer: CustomizerService) { }

  ngOnInit(): void {
    // Initialisation
  }

  ngOnDestroy(): void {
    // Cleanup
  }

  toggleCustomizer(): void {
    this.customizer.toggleCustomizerDrawer();
  }
}
