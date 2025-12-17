// profile.component.ts
import { Component, OnInit, inject, signal, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ProfileService } from '../../services/profile.service';
import { AuthService } from '../../../../core/services/auth.service';
import { TokenService } from '../../../../core/services/token.service';
import { UpdateUtilisateurDto, UtilisateurDto } from '../../../../core/models/UtilisateurDto';
import { RoleEnum, SexeEnum } from '../../../../core/models/user.enums';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private profileService = inject(ProfileService);
  private authService = inject(AuthService);
  private router = inject(Router);
  private tokenService = inject(TokenService);
  private fb = inject(FormBuilder);
  private destroyRef = inject(DestroyRef);

  user = signal<UtilisateurDto | null>(null);
  loading = signal(true);
  isEditing = signal(false);
  error = signal<string | null>(null);
  success = signal<string | null>(null);

  // Formulaire réactif
  profileForm: FormGroup;

  sexeOptions = [
    { value: SexeEnum.HOMME, label: 'Homme' },
    { value: SexeEnum.FEMME, label: 'Femme' }
  ];

  constructor() {
    // Initialisation du formulaire
    this.profileForm = this.fb.group({
      prenom: ['', [Validators.required, Validators.minLength(2)]],
      nom: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      telephone: ['', [
        Validators.pattern(/^(\+221)?7[05678][0-9]{7}$/)
      ]],
      sexe: [SexeEnum.HOMME]
    });

    // Désactiver le formulaire initialement
    this.profileForm.disable();
  }

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    this.loading.set(true);
    this.profileService.getProfile()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (user) => {
          this.user.set(user);
          this.patchFormValues(user);
          this.loading.set(false);
        },
        error: (err) => {
          this.error.set('Erreur lors du chargement du profil');
          this.loading.set(false);
          console.error(err);
        }
      });
  }

  private patchFormValues(user: UtilisateurDto) {
    this.profileForm.patchValue({
      prenom: user.prenom || '',
      nom: user.nom || '',
      email: user.email || '',
      telephone: user.telephone || '',
      sexe: user.sexe || SexeEnum.HOMME
    });
  }

  toggleEdit() {
    this.isEditing.set(!this.isEditing());

    if (this.isEditing()) {
      this.profileForm.enable();
      // Garder l'email désactivé si vous ne voulez pas qu'il soit modifiable
      this.profileForm.get('email')?.disable();
    } else {
      this.profileForm.disable();
      // Réinitialiser les valeurs si on annule
      if (this.user()) {
        this.patchFormValues(this.user()!);
      }
    }
  }

  saveProfile() {
    if (this.profileForm.invalid || !this.user()) {
      this.markFormGroupTouched(this.profileForm);
      return;
    }

    const payload: UpdateUtilisateurDto = {
      prenom: this.profileForm.get('prenom')?.value,
      nom: this.profileForm.get('nom')?.value,
      email: this.profileForm.get('email')?.value,
      telephone: this.profileForm.get('telephone')?.value,
      sexe: this.profileForm.get('sexe')?.value
    };

    // Supprimer les champs undefined
    Object.keys(payload).forEach(key => {
      if (payload[key as keyof UpdateUtilisateurDto] === undefined) {
        delete payload[key as keyof UpdateUtilisateurDto];
      }
    });

    this.profileService.updateProfile(this.user()!.id, payload)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (updatedUser) => {
          this.user.set(updatedUser);
          this.isEditing.set(false);
          this.profileForm.disable();
          this.success.set('Profil mis à jour avec succès');
          setTimeout(() => this.success.set(null), 3000);
        },
        error: (err) => {
          this.error.set(err.error?.message || 'Erreur lors de la mise à jour du profil');
          console.error(err);
        }
      });
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }

  logout() {
    this.profileService.logout()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.authService.logout();
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error('Erreur lors de la déconnexion:', err);
          this.authService.logout();
          this.router.navigate(['/login']);
        }
      });
  }

  get nomComplet(): string {
    const user = this.user();
    if (!user) return '';
    return `${user.prenom} ${user.nom}`;
  }

  getInitials(): string {
    const user = this.user();
    if (!user) return '';
    return `${user.prenom?.[0] || ''}${user.nom?.[0] || ''}`.toUpperCase();
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString('fr-FR', {
      day: 'numeric',
      month: 'long',
      year: 'numeric'
    });
  }

  getRoleLabel(role: RoleEnum): string {
    const roleLabels = {
      [RoleEnum.ADMIN]: 'Administrateur',
      [RoleEnum.PRESTATAIRE]: 'Prestataire',
      [RoleEnum.CLIENT]: 'Client'
    };
    return roleLabels[role] || role;
  }

  getSexeLabel(sexe?: SexeEnum): string {
    switch (sexe) {
      case SexeEnum.HOMME:
        return 'Homme';
      case SexeEnum.FEMME:
        return 'Femme';
      default:
        return 'Non spécifié';
    }
  }


  // Getters pour les contrôles du formulaire
  get prenom() { return this.profileForm.get('prenom'); }
  get nom() { return this.profileForm.get('nom'); }
  get email() { return this.profileForm.get('email'); }
  get telephone() { return this.profileForm.get('telephone'); }
  get sexe() { return this.profileForm.get('sexe'); }
}
