import { Component, HostListener, inject } from '@angular/core';
import { Router } from '@angular/router';

interface MenuItem { title: string; icon: string; route: string; active: boolean; }
@Component({
  selector: 'app-admin-dashboard',
  standalone: false,
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {
  isSidebarOpen = true;
  isMobileView = false;
  private router = inject(Router);

  // Menu items avec icônes SVG inline
  menuItems: MenuItem[] = [
    { title: 'Dashboard', icon: 'M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6', active: true, route: '/admin/dashboard' },
    { title: 'Clients', icon: 'M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z', active: false, route: '/admin/clients' },
    { title: 'Services', icon: 'M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10', active: false, route: '/admin/services' },
    { title: 'Rendez-vous', icon: 'M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z', active: false, route: '/admin/appointments' },
    { title: 'Statistiques', icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z', active: false, route: '/admin/stats' },
    { title: 'Paramètres', icon: 'M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z', active: false, route: '/admin/settings' }
  ];
  stats = [
    { title: 'Chiffre d\'affaires', value: '15,240 €', change: '+12.5%', icon: '💰', color: 'bg-green-100 text-green-600' },
    { title: 'Clients actifs', value: '342', change: '+8.2%', icon: '👥', color: 'bg-blue-100 text-blue-600' },
    { title: 'Rendez-vous', value: '48', change: '+15.3%', icon: '📅', color: 'bg-purple-100 text-purple-600' },
    { title: 'Services', value: '12', change: '+2.1%', icon: '🛠️', color: 'bg-orange-100 text-orange-600' }
  ];
  recentAppointments = [
    { client: 'Marie Dupont', service: 'Consultation', date: 'Aujourd\'hui, 14:30', status: 'confirmé' },
    { client: 'Jean Martin', service: 'Installation', date: 'Demain, 10:00', status: 'en attente' },
    { client: 'Sophie Leroy', service: 'Maintenance', date: '15 Nov, 09:00', status: 'confirmé' },
    { client: 'Thomas Petit', service: 'Audit', date: '16 Nov, 11:30', status: 'annulé' }
  ];

  // Clients récents
  recentClients = [
    { name: 'Emma Bernard', email: 'emma@email.com', joinDate: '15 Nov 2024', status: 'actif' },
    { name: 'Lucas Moreau', email: 'lucas@email.com', joinDate: '14 Nov 2024', status: 'actif' },
    { name: 'Chloé Dubois', email: 'chloe@email.com', joinDate: '13 Nov 2024', status: 'inactif' },
    { name: 'Hugo Lambert', email: 'hugo@email.com', joinDate: '12 Nov 2024', status: 'actif' }
  ];


  constructor() { }

  ngOnInit(): void {
    this.checkMobileView();
    this.setActiveRoute();
  }

  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.checkMobileView();
  }

  toggleSidebar(): void {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  closeMobileSidebar(): void {
    if (this.isMobileView) {
      this.isSidebarOpen = false;
    }
  }

  private checkMobileView(): void {
    this.isMobileView = window.innerWidth < 1024;
    if (!this.isMobileView) {
      this.isSidebarOpen = true;
    }
  }

  selectMenuItem(item: MenuItem): void {
    this.menuItems.forEach(menuItem => menuItem.active = false);
    item.active = true;
    this.router.navigate([item.route]);
    this.closeMobileSidebar();
  }

  private setActiveRoute(): void {
    const currentRoute = this.router.url;
    this.menuItems.forEach(item => {
      item.active = currentRoute.includes(item.route);
    });
  }
  getStatusClass(status: string): string {
    switch (status) {
      case 'confirmé': return 'badge badge-success';
      case 'en attente': return 'badge badge-warning';
      case 'annulé': return 'badge badge-error';
      default: return 'badge';
    }
  }

}
