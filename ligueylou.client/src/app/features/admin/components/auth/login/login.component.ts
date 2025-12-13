import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../../../core/services/auth.service';
import { RoleEnum } from '../../../../../core/models/user.enums';

@Component({
  selector: 'app-login',
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  activeTab: 'email' | 'phone' = 'email';
  showPassword = false;
  currentYear: number;
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  loading = signal(false);
  error = signal<string | null>(null);
  constructor() {
    this.currentYear = new Date().getFullYear();
  }
  emailForm = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    remember: [false]
  })

  phoneForm = this.fb.nonNullable.group({
    telephone: ['', [
      Validators.required,
      Validators.pattern(/^(\+221)?7[05678][0-9]{7}$/) // Sénégal
    ]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  formData = {
    email: '',
    password: '',
    telephone: '',
    remember: false
  };
  ngOnInit(): void {
    // Initialisation si nécessaire
  }
  setActiveTab(tab: 'email' | 'phone'): void {
    this.error.set(null);
    this.activeTab = tab;
  }
  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
  submitEmail() {
    if (this.emailForm.invalid) {
      this.emailForm.markAllAsTouched();
      return;
    }
    this.authenticate(this.emailForm.getRawValue());
  }
  submitPhone() {
    if (this.phoneForm.invalid) {
      this.phoneForm.markAllAsTouched();
      return;
    }

    this.authenticate(this.phoneForm.getRawValue());
  }
  private authenticate(payload: any) {
    this.loading.set(true);
    this.error.set(null);

    this.authService.login(payload).subscribe({
      next: (res) => {
        this.loading.set(false);
        //this.redirectByRole(res.utilisateur.role);
        this.router.navigate(['/admin/dashboard']);
      },
      error: (err) => {
        this.loading.set(false);
        this.error.set(err.error?.message || 'Identifiants incorrects');
      }
    });
  }
  private redirectByRole(role: RoleEnum) {
    const routes = {
      [RoleEnum.ADMIN]: '/admin/dashboard',
      [RoleEnum.PRESTATAIRE]: '/prestataire/dashboard',
      [RoleEnum.CLIENT]: '/client/dashboard',
    };

    this.router.navigate([routes[role] ?? '/login']);
  }

}
