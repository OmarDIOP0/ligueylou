import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomizerService } from '../customizer.service';

@Component({
  selector: 'app-vertical-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vertical-header.component.html',
  styleUrls: ['./vertical-header.component.css']
})
export class VerticalHeaderComponent {
  showSearch = signal(false);

  constructor(public customizer: CustomizerService) { }

  toggleSearch(): void {
    this.showSearch.update(value => !value);
  }

  toggleSidebar(): void {
    this.customizer.toggleMiniSidebar();
  }

  toggleMobileSidebar(): void {
    this.customizer.toggleSidebarDrawer();
  }
}
