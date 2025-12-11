import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

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
  loading = false;
  currentYear: number;

  formData = {
    email: '',
    password: '',
    phone: '',
    remember: false
  };

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
    if (this.loading) return;

    this.loading = true;
    console.log('Email login attempt:', this.formData.email);

    // Simuler une requête API
    setTimeout(() => {
      this.loading = false;
      // Ici, vous ajouteriez la logique de connexion réelle
      // this.authService.login(this.formData.email, this.formData.password);
    }, 1500);
  }

  handlePhoneLogin(): void {
    if (this.loading) return;

    this.loading = true;
    console.log('Phone login attempt:', this.formData.phone);

    // Simuler l'envoi de code
    setTimeout(() => {
      this.loading = false;
      // Ici, vous ajouteriez la logique d'envoi de code par SMS
    }, 1500);
  }

  loginWithFacebook(): void {
    this.loading = true;
    console.log('Facebook login attempt');

    setTimeout(() => {
      this.loading = false;
      // Intégration Facebook OAuth
    }, 1500);
  }

  loginWithTwitter(): void {
    this.loading = true;
    console.log('Twitter login attempt');

    setTimeout(() => {
      this.loading = false;
      // Intégration Twitter OAuth
    }, 1500);
  }


}
