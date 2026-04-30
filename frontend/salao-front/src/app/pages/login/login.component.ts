import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  email = '';
  password = '';

  constructor(private router: Router) {}

  onLogin() {
    // Aqui você pode trocar por API depois
    if (this.email && this.password) {
      this.router.navigate(['/home']);
    } else {
      alert('Preencha os campos');
    }
  }
}