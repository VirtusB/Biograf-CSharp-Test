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
  countOfRervations = 0;
  countOfPaidReservations = 0;
  countOfCanceledReservations = 0;
  countOfReservedReservations = 0;
  @Input() movie: Movie;

  constructor(private authService: AuthService, private alertify: AlertifyService, private reservationService: ReservationService) { }

  ngOnInit() {
    this.getReservations();
  }

  countReservations() {
    this.countOfRervations = this.reservations.length;
      this.reservations.forEach(reservation => {
        if (reservation.bookingState === 2) {
          this.countOfPaidReservations++;
        } else if (reservation.bookingState === 0) {
          this.countOfCanceledReservations++;
        } else if (reservation.bookingState === 1) {
          this.countOfReservedReservations++;
        }
      });
  }

  getReservations() {
    this.reservationService.
    getReservations(this.authService.decodedToken.nameid)
    .subscribe((res: Reservation[]) => {
      this.reservations = res;
      this.countReservations();
    }, error => {
      this.alertify.error(error);
    });
  }

}
