import { Component, OnInit, Input } from '@angular/core';
import { Reservation } from '../../_models/reservation';
import { AlertifyService } from '../../_services/alertify.service';
import { ReservationService } from '../../_services/reservation.service';
import { AuthService } from '../../_services/auth.service';
import { Movie } from '../../_models/movie';

@Component({
  selector: 'app-member-orders',
  templateUrl: './member-orders.component.html',
  styleUrls: ['./member-orders.component.css']
})
export class MemberOrdersComponent implements OnInit {
  reservations: Reservation[];
  countOfRervations: number;
  paidReservationsCount: number;
  @Input() movie: Movie;
  dateHover = false;

  constructor(private authService: AuthService, private alertify: AlertifyService, private reservationService: ReservationService) { }

  ngOnInit() {
    this.getReservations();
  }

  getPaidReservationsCount() {
    this.reservationService.
    getPaidReservationsCount(this.authService.decodedToken.nameid)
    .subscribe((res: number) => {
      this.paidReservationsCount = res;
    }, error => {
      this.alertify.error(error);
    });
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
