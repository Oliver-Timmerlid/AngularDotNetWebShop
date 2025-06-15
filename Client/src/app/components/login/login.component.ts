import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { UserLogin } from '../../models/user-login';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  signinForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.signinForm = this.fb.group({
      email: [''],
      password: [''],
    });
  }

  onSubmit(): void {
    const loginData: UserLogin = this.signinForm.value;
    this.userService.login(loginData).subscribe({
      next: (response) => {
        if (response.token) {
          localStorage.setItem('authToken', response.token);
        }
        this.router.navigate(['/products']);
      },
      error: (err) => {
        console.error('Login failed', err);
      },
    });
  }
}
