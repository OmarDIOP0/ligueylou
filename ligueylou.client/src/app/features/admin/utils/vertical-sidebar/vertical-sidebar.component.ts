import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CustomizerService } from '../customizer.service';

interface SidebarItem {
  title?: string;
  icon?: string;
  route?: string;
  children?: SidebarItem[];
  divider?: boolean;
  header?: boolean;
}

@Component({
  selector: 'app-vertical-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './vertical-sidebar.component.html',
  styleUrls: ['./vertical-sidebar.component.css']
})
export class VerticalSidebarComponent implements OnInit {
  sidebarItems: SidebarItem[] = [
    { title: 'Dashboard', icon: 'dashboard', route: '/dashboard' },
    { title: 'Analytics', icon: 'analytics', route: '/analytics' },
    { title: 'E-commerce', icon: 'shopping_cart', route: '/ecommerce' },
    { divider: true },
    { title: 'Components', header: true },
    {
      title: 'UI Elements',
      icon: 'widgets',
      children: [
        { title: 'Buttons', route: '/ui/buttons' },
        { title: 'Cards', route: '/ui/cards' },
        { title: 'Forms', route: '/ui/forms' }
      ]
    },
    {
      title: 'Pages',
      icon: 'description',
      children: [
        { title: 'Login', route: '/auth/login' },
        { title: 'Register', route: '/auth/register' },
        { title: 'Profile', route: '/pages/profile' }
      ]
    },
    { divider: true },
    { title: 'Utilities', header: true },
    { title: 'Icons', icon: 'emoji_objects', route: '/icons' },
    { title: 'Typography', icon: 'text_fields', route: '/typography' },
    { title: 'Colors', icon: 'palette', route: '/colors' }
  ];

  constructor(public customizer: CustomizerService) { }

  ngOnInit(): void { }

  isActiveRoute(route: string): boolean {
    // Implémentez la logique de vérification de route active
    return false;
  }
}
