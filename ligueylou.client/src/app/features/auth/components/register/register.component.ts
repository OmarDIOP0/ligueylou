import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  activeTab: 'client' | 'provider' = 'client';
  showPassword = false;
  showConfirmPassword = false;
  loading = false;
  currentYear: number;

  clientFormData = {
    nomComplet: '',
    email: '',
    phone: '',
    password: '',
    confirmPassword: '',
    acceptTerms: false
  };

  providerFormData = {
    nomComplet: '',
    email: '',
    phone: '',
    password: '',
    confirmPassword: '',
    acceptTerms: false
  };

  constructor() {
    this.currentYear = new Date().getFullYear();
  }

  ngOnInit(): void {
    // Initialisation si nécessaire
  }

  setActiveTab(tab: 'client' | 'provider'): void {
    this.activeTab = tab;
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPasswordVisibility(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  handleClientSubmit(): void {
    if (this.loading) return;

    // Vérifier que les mots de passe correspondent
    if (this.clientFormData.password !== this.clientFormData.confirmPassword) {
      alert('Les mots de passe ne correspondent pas');
      return;
    }

    // Vérifier les conditions
    if (!this.clientFormData.acceptTerms) {
      alert('Veuillez accepter les conditions d\'utilisation');
      return;
    }

    this.loading = true;
    console.log('Client registration attempt:', this.clientFormData);

    // Simuler une requête API
    setTimeout(() => {
      this.loading = false;
      // Ici, vous ajouteriez la logique d'inscription réelle
      // this.authService.registerClient(this.clientFormData);
    }, 1500);
  }

  handleProviderSubmit(): void {
    if (this.loading) return;

    // Vérifier que les mots de passe correspondent
    if (this.providerFormData.password !== this.providerFormData.confirmPassword) {
      alert('Les mots de passe ne correspondent pas');
      return;
    }

    // Vérifier les conditions
    if (!this.providerFormData.acceptTerms) {
      alert('Veuillez accepter les conditions d\'utilisation');
      return;
    }

    this.loading = true;
    console.log('Provider registration attempt:', this.providerFormData);

    // Simuler une requête API
    setTimeout(() => {
      this.loading = false;
      // Ici, vous ajouteriez la logique d'inscription réelle
      // this.authService.registerProvider(this.providerFormData);
    }, 1500);
  }
}
