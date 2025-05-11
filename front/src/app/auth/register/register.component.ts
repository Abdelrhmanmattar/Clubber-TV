import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone:true,
  imports: [RouterLink,RouterLinkActive,FormsModule],  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  user = {
    email: '',
    userName: '',
    phoneNumber: '',
    password: ''
  };
  constructor(private auth:AuthService , private router:Router) {}
  register() {
    this.auth.register(this.user).subscribe((res:any) => {
      this.router.navigate(['/login']);
    });
  }

}
