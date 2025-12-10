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
  activeTab = 'email';
  email = '';
  password = '';
  phone = '';
  remember = false;
  showPassword = false;

  loginWithEmail() {
    console.log('Email login', this.email, this.password, this.remember);
  }

  loginWithPhone() {
    console.log('Phone login', this.phone);
  }


}
