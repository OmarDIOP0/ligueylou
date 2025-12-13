import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, FormsModule } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { RoleEnum } from '../../../../core/models/user.enums';

@Component({
  selector: 'app-login',
  imports: [CommonModule, RouterModule,FormsModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
//je dois tester avec form et signals
export class LoginComponent {
  activeTab: 'email' | 'phone' = 'email';
  showPassword = false;
  currentYear: number;
  private router = inject(Router);

  formData = {
    email: '',
    password: '',
    telephone: '',
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
    this.error.set(null);
    this.loading.set(true);

    this.authService.login({
      email: this.formData.email,
      password: this.formData.password
    }).subscribe({
      next: (res) => {
        this.loading.set(false);
        switch (res.utilisateur.role) {
          case RoleEnum.ADMIN:
            this.router.navigate(['/admin/dashboard']);
            break;
          case RoleEnum.PRESTATAIRE:
            this.router.navigate(['/prestataire/dashboard']); 
            break;
          case RoleEnum.CLIENT:
            this.router.navigate(['/client/dashboard']); 
            break;
          default:
            this.router.navigate(['/login']); 
        }
      },
      error: (err) => {
        this.loading.set(false);
        this.error.set(err.error?.message || "Identifiants incorrects.");
      }
    });
  }



  handlePhoneLogin(): void {
    this.error.set(null);
    this.loading.set(true);
    this.authService.login({
      telephone: this.formData.telephone,
      password: this.formData.password
    }).subscribe({
      next: (res) => {
        this.loading.set(false);
        switch (res.utilisateur.role) {
          case RoleEnum.ADMIN:
            this.router.navigate(['/admin/dashboard']);
            break;
          case RoleEnum.PRESTATAIRE:
            this.router.navigate(['/prestataire/dashboard']);
            break;
          case RoleEnum.CLIENT:
            this.router.navigate(['/client/dashboard']);
            break;
          default:
            this.router.navigate(['/login']);
        }
      }, error: (err) => {
        this.loading.set(false);
        this.error.set(err.error?.message || "Identification incorrecte")
      }
    });
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
