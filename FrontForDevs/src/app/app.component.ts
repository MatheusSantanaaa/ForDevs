import { Component } from '@angular/core';
import { AuthService } from './auth/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isUserLoggedIn: boolean;

  title = 'FrontForDevs';
  
  constructor(private authService: AuthService) {
    this.isUserLoggedIn = this.authService.isLoggedIn();
  }
}
