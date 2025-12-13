import { Injectable, signal, computed } from '@angular/core';

export interface ThemeConfig {
  fontTheme: string;
  miniSidebar: boolean;
  sidebarDrawer: boolean;
  customizerDrawer: boolean;
  inputBg: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CustomizerService {
  // State signals
  private config = signal<ThemeConfig>({
    fontTheme: 'font-roboto',
    miniSidebar: false,
    sidebarDrawer: false,
    customizerDrawer: false,
    inputBg: false
  });

  // Computed signals
  fontTheme = computed(() => this.config().fontTheme);
  miniSidebar = computed(() => this.config().miniSidebar);
  sidebarDrawer = computed(() => this.config().sidebarDrawer);
  customizerDrawer = computed(() => this.config().customizerDrawer);
  inputBg = computed(() => this.config().inputBg);

  // Theme options
  readonly fontFamily = ['Roboto', 'Poppins', 'Inter'];
  readonly themes = ['light', 'dark', 'cupcake', 'bumblebee', 'emerald', 'corporate', 'synthwave', 'retro', 'cyberpunk', 'valentine', 'halloween', 'garden', 'forest', 'aqua', 'lofi', 'pastel', 'fantasy', 'wireframe', 'black', 'luxury', 'dracula', 'cmyk', 'autumn', 'business', 'acid', 'lemonade', 'night', 'coffee', 'winter'];

  toggleMiniSidebar(): void {
    this.config.update(current => ({
      ...current,
      miniSidebar: !current.miniSidebar
    }));
  }

  toggleSidebarDrawer(): void {
    this.config.update(current => ({
      ...current,
      sidebarDrawer: !current.sidebarDrawer
    }));
  }

  toggleCustomizerDrawer(): void {
    this.config.update(current => ({
      ...current,
      customizerDrawer: !current.customizerDrawer
    }));
  }

  setFontTheme(font: string): void {
    this.config.update(current => ({
      ...current,
      fontTheme: `font-${font.toLowerCase()}`
    }));
  }

  setInputBg(enabled: boolean): void {
    this.config.update(current => ({
      ...current,
      inputBg: enabled
    }));
  }

  resetOptions(): void {
    this.config.set({
      fontTheme: 'font-roboto',
      miniSidebar: false,
      sidebarDrawer: false,
      customizerDrawer: false,
      inputBg: false
    });
  }

  setTheme(theme: string): void {
    document.documentElement.setAttribute('data-theme', theme);
    localStorage.setItem('theme', theme);
  }

  loadTheme(): void {
    const savedTheme = localStorage.getItem('theme') || 'light';
    this.setTheme(savedTheme);
  }
}
