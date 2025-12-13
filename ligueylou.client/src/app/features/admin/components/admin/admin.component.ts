import { Component, OnInit, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

interface MenuItem {
  title: string;
  icon: string;
  route?: string;
  badge?: string;
  children?: MenuItem[];
  isOpen?: boolean;
}

interface MenuSections {
  dashboard: MenuItem[];
  pages: MenuItem[];
  utilities: MenuItem[];
}

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  isSidebarOpen = true;
  isMobileView = false;
  currentDate: string = '';

  // Pour le menu burger/sidebar horizontal
  isHorizontalSidebarOpen = false;

  menuSections: MenuSections = {
    dashboard: [
      {
        title: 'Default',
        icon: 'M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6',
        route: '/admin/dashboard',
        badge: 'New'
      },
      {
        title: 'Analytics',
        icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z',
        route: '/admin/analytics'
      },
      {
        title: 'E-commerce',
        icon: 'M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z',
        route: '/admin/ecommerce',
        children: [
          {
            title: 'Products',
            icon: 'M5 8h14M5 8a2 2 0 110-4h14a2 2 0 110 4M5 8v10a2 2 0 002 2h10a2 2 0 002-2V8m-9 4h4',
            route: '/admin/ecommerce/products'
          },
          {
            title: 'Orders',
            icon: 'M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z',
            route: '/admin/ecommerce/orders'
          },
          {
            title: 'Customers',
            icon: 'M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197m13.5 5.197a4.5 4.5 0 00-8.366-2.309M3 13.125A4.125 4.125 0 017.125 9h9.75A4.125 4.125 0 0121 13.125v1.75A4.125 4.125 0 0116.875 19h-9.75A4.125 4.125 0 013 14.875v-1.75z',
            route: '/admin/ecommerce/customers'
          }
        ]
      }
    ],
    pages: [
      {
        title: 'Authentication',
        icon: 'M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z',
        route: '/admin/auth',
        children: [
          {
            title: 'Login',
            icon: 'M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1',
            route: '/admin/auth/login'
          },
          {
            title: 'Register',
            icon: 'M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z',
            route: '/admin/auth/register'
          }
        ]
      },
      {
        title: 'User Profile',
        icon: 'M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z',
        route: '/admin/profile'
      }
    ],
    utilities: [
      {
        title: 'Typography',
        icon: 'M4 6h16M4 12h16M4 18h7',
        route: '/admin/typography'
      },
      {
        title: 'Icons',
        icon: 'M7 21a4 4 0 01-4-4V5a2 2 0 012-2h4a2 2 0 012 2v12a4 4 0 01-4 4zm0 0h12a2 2 0 002-2v-4a2 2 0 00-2-2h-2.343M11 7.343l1.657-1.657a2 2 0 012.828 0l2.829 2.829a2 2 0 010 2.828l-8.486 8.485M7 17h.01',
        route: '/admin/icons'
      }
    ]
  };

  ngOnInit(): void {
    this.updateCurrentDate();
    this.checkMobileView();
    window.addEventListener('resize', () => this.checkMobileView());

    // Initialisation de l'état des sous-menus
    this.initSubmenus();
  }

  toggleSidebar(): void {
    this.isSidebarOpen = !this.isSidebarOpen;

    // Fermer le sidebar horizontal quand on ferme le vertical
    if (!this.isSidebarOpen) {
      this.isHorizontalSidebarOpen = false;
    }
  }

  toggleHorizontalSidebar(): void {
    this.isHorizontalSidebarOpen = !this.isHorizontalSidebarOpen;
  }

  toggleSubmenu(item: MenuItem): void {
    if (item.children) {
      item.isOpen = !item.isOpen;
    }
  }

  private updateCurrentDate(): void {
    const now = new Date();
    const options: Intl.DateTimeFormatOptions = {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    };
    this.currentDate = now.toLocaleDateString('fr-FR', options);
  }

  private checkMobileView(): void {
    this.isMobileView = window.innerWidth < 1024;
    if (!this.isMobileView) {
      this.isSidebarOpen = true;
      // Fermer le sidebar horizontal sur desktop par défaut
      this.isHorizontalSidebarOpen = false;
    } else {
      // Sur mobile, fermer les deux sidebars par défaut
      this.isSidebarOpen = false;
      this.isHorizontalSidebarOpen = false;
    }
  }

  private initSubmenus(): void {
    Object.values(this.menuSections).forEach((section: MenuItem[]) => {
      section.forEach((item: MenuItem) => {
        if (item.children) {
          item.isOpen = false;
        }
      });
    });
  }


  // Fermer le sidebar horizontal quand on clique ailleurs
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.horizontal-sidebar') &&
      !target.closest('.burger-menu-button') &&
      this.isHorizontalSidebarOpen) {
      this.isHorizontalSidebarOpen = false;
    }
  }
}
