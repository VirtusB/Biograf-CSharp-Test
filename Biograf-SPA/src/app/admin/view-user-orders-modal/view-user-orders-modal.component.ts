import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { Reservation } from '../../_models/reservation';
import { ReservationService } from '../../_services/reservation.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-view-user-orders-modal',
  templateUrl: './view-user-orders-modal.component.html',
  styleUrls: ['./view-user-orders-modal.component.css']
})
export class ViewUserOrdersModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  reservations: Reservation[];
  countOfRervations = 0;
  countOfPaidReservations = 0;
  countOfCanceledReservations = 0;
  countOfReservedReservations = 0;
  userId: number;


  constructor(
    public bsModalRef: BsModalRef,
    private reservationService: ReservationService,
    private alertify: AlertifyService
  ) { }

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
    getReservations(this.userId)
    .subscribe((res: Reservation[]) => {
      this.reservations = res;
      this.countReservations();
    }, error => {
      this.alertify.error(error);
    });
  }

}
