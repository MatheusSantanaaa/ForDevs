import { Component, OnInit } from '@angular/core';
import { MenuItem } from './Menu/menu.model';
import { MENU } from './Menu/menu';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-menu',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderMenuComponent implements OnInit {
  public headerMenus: MenuItem[];

  constructor(private authService: AuthService, private router: Router) {
    this.headerMenus = MENU.filter(menu => menu.isTopbarMenu);
  }

  ngOnInit(): void {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
