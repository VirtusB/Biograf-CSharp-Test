import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReservationService } from '../../_services/reservation.service';
import { Reservation } from '../../_models/reservation';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-reservation-validation',
  templateUrl: './reservation-validation.component.html',
  styleUrls: ['./reservation-validation.component.css']
})
export class ReservationValidationComponent implements OnInit {
  validateReservationForm: FormGroup;
  reservation: Reservation;
  reservationNotFound: boolean;

  states = [
    {value: 2, display: 'er betalt'},
    {value: 1, display: 'er reserveret'},
    {value: 0, display: 'er annulleret'},
    {value: 3, display: 'er indløst'}
  ];

  constructor(
    private fb: FormBuilder,
    private reservationService: ReservationService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.createvalidateReservationForm();
  }

  changeBookingState() {
    const state = this.validateReservationForm.get('reservationBookingState').value;
    this.reservation.bookingState = state;
    const reservationId = this.reservation.id;
    const userId = this.reservation.userId;

    this.reservationService.setBookingState(userId, reservationId, this.reservation).subscribe(next => {
      this.alertify.success('Status ændret');

      if (+state === 3) {
        this.validateReservationForm.controls['reservationBookingState'].disable();
      } else {
        this.validateReservationForm.controls['reservationBookingState'].enable();
      }

      this.validateReservationForm.updateValueAndValidity();
    }, error => {
      this.alertify.error(error);
    });
  }


  validateReservation() {
    const reservationId = this.validateReservationForm.get('reservationId').value;

    this.reservationService.getReservation(reservationId).subscribe(reservation => {
      if (reservation !== null) {

        if (reservation.bookingState === 3) {
          this.validateReservationForm.controls['reservationBookingState'].disable();
        } else {
          this.validateReservationForm.controls['reservationBookingState'].enable();
        }

        this.reservation = reservation;
        this.reservationNotFound = false;
      } else {
        this.reservation = null;
        this.reservationNotFound = true;
      }
    });
  }

  createvalidateReservationForm() {
    this.validateReservationForm = this.fb.group({
      reservationId: ['', Validators.required],
      reservationBookingState: []
    });
  }

}
