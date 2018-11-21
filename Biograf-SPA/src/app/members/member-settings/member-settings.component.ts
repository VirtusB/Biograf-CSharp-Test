import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-settings',
  templateUrl: './member-settings.component.html',
  styleUrls: ['./member-settings.component.css']
})
export class MemberSettingsComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  deleteMyUser() {
    this.alertify.confirm('Slet bruger', 'Er du helt sikker? Det kan ikke fortrydes', () => {
      this.userService.deleteMyUser(this.authService.decodedToken.nameid).subscribe((res) => {
        this.alertify.success('Din bruger er slettet');
        this.logout();
      }, error => {
        this.alertify.error(error);
      });
    });
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.router.navigate(['/home']);
  }

}
