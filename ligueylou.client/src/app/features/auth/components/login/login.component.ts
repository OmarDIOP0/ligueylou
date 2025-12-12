import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormsModule } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, RouterModule,FormsModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  activeTab: 'email' | 'phone' = 'email';
  showPassword = false;
  currentYear: number;

  formData = {
    email: '',
    password: '',
    phone: '',
    remember: false
  };
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  loading = signal(false);
  error = signal<string | null>(null);
  constructor() {
    this.currentYear = new Date().getFullYear();
  }

  ngOnInit(): void {
    // Initialisation si nécessaire
  }

  setActiveTab(tab: 'email' | 'phone'): void {
    this.activeTab = tab;
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  handleEmailLogin(): void {
    this.loading.set(true);
    console.log('Email login attempt:', this.formData.email);

    // Simuler une requête API
    setTimeout(() => {
      this.loading.set(false);
      // Ici, vous ajouteriez la logique de connexion réelle
      // this.authService.login(this.formData.email, this.formData.password);
    }, 1500);
  }

  handlePhoneLogin(): void {

    this.loading.set(true);
    console.log('Phone login attempt:', this.formData.phone);

    // Simuler l'envoi de code
    setTimeout(() => {
      this.loading.set(false);
      // Ici, vous ajouteriez la logique d'envoi de code par SMS
    }, 1500);
  }

  loginWithFacebook(): void {
    this.loading.set(true);
    console.log('Facebook login attempt');

    setTimeout(() => {
      this.loading.set(false);
      // Intégration Facebook OAuth
    }, 1500);
  }

  loginWithTwitter(): void {
    this.loading.set(true);
    console.log('Twitter login attempt');

    setTimeout(() => {
      this.loading.set(false);
      // Intégration Twitter OAuth
    }, 1500);
  }


}
