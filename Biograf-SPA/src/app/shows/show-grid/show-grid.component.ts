import { Component, OnInit, Input } from '@angular/core';
import { Show } from '../../_models/show';
import { AuthService } from '../../_services/auth.service';
import { ShowService } from '../../_services/show.service';
import { AlertifyService } from '../../_services/alertify.service';


@Component({
  selector: 'app-show-grid',
  templateUrl: './show-grid.component.html',
  styleUrls: ['./show-grid.component.css']
})
export class ShowGridComponent implements OnInit {

  @Input() show: Show;

  constructor(
    private authService: AuthService,
    private showService: ShowService,
    private alertify: AlertifyService
  ) { }

  loggedIn() {
    return this.authService.loggedIn();
  }

  ngOnInit() {
  }

}
