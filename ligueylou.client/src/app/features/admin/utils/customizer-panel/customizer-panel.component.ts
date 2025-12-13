import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomizerService } from '../customizer.service';

@Component({
  selector: 'app-customizer-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customizer-panel.component.html',
  styleUrls: ['./customizer-panel.component.css']
})
export class CustomizerPanelComponent {
  selectedFont: string = 'Roboto';
  selectedTheme: string = 'light';

  constructor(public customizer: CustomizerService) {
    this.selectedFont = this.customizer.fontTheme().replace('font-', '');
  }

  ngOnInit(): void {
    this.customizer.loadTheme();
  }

  onFontChange(font: string): void {
    this.selectedFont = font;
    this.customizer.setFontTheme(font);
  }

  onThemeChange(theme: string): void {
    this.selectedTheme = theme;
    this.customizer.setTheme(theme);
  }

  resetOptions(): void {
    this.customizer.resetOptions();
    this.selectedFont = 'Roboto';
    this.selectedTheme = 'light';
    this.customizer.setTheme('light');
  }
}
