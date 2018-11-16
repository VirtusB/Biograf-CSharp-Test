import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CarouselConfig } from 'ngx-bootstrap/carousel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [
    { provide: CarouselConfig, useValue: { interval: 3000, showIndicators: false } }
  ]
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {

  }

  registerToggle() {
    this.registerMode = true;
  }


  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
