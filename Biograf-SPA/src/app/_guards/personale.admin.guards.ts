import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Injectable({
  providedIn: 'root'
})
export class PersonaleAdminGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(): boolean {
    if (this.authService.loggedIn()
    &&
    (this.authService.currentUser.role.name === 'Personale' || this.authService.currentUser.role.name === 'Admin')) {
      return true;
    }

    this.alertify.error('Du har ikke adgang til denne side');
    this.router.navigate(['/home']);
    return false;
  }
}
