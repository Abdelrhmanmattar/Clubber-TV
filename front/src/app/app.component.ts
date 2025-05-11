import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from "./auth/login/login.component";
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone:true,
  imports: [RouterOutlet, HeaderComponent],
  template: `

<app-header></app-header>
<main>
  <router-outlet></router-outlet>
</main>

`,
  styles: [],
})
export class AppComponent implements OnInit {
  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit() {
    if (this.auth.isLoggedIn()) {
      this.router.navigate(['/matche']); // User already logged in
    } else {
      this.router.navigate(['/login']); // No token, go to login
    }
  }
}
