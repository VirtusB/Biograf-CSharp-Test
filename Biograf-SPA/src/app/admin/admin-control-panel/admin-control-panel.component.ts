import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-admin-control-panel',
  templateUrl: './admin-control-panel.component.html',
  styleUrls: ['./admin-control-panel.component.css']
})
export class AdminControlPanelComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit() {
  }

}
