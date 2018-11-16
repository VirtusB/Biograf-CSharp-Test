import { Component, OnInit } from '@angular/core';
import { Reservation } from '../../_models/reservation';
import { AlertifyService } from '../../_services/alertify.service';
import { ReservationService } from '../../_services/reservation.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-member-orders',
  templateUrl: './member-orders.component.html',
  styleUrls: ['./member-orders.component.css']
})
export class MemberOrdersComponent implements OnInit {
  reservations: Reservation[];
  countOfRervations: number;

  constructor(private authService: AuthService, private alertify: AlertifyService, private reservationService: ReservationService) { }

  ngOnInit() {
    this.getReservations();
  }

  getReservations() {
    this.reservationService.
    getReservations(this.authService.decodedToken.nameid)
    .subscribe((res: Reservation[]) => {
      this.reservations = res;
      this.countOfRervations = res.length;
    }, error => {
      this.alertify.error(error);
    });
  }

}
