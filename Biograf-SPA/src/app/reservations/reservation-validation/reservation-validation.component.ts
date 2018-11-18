import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReservationService } from '../../_services/reservation.service';
import { Reservation } from '../../_models/reservation';

@Component({
  selector: 'app-reservation-validation',
  templateUrl: './reservation-validation.component.html',
  styleUrls: ['./reservation-validation.component.css']
})
export class ReservationValidationComponent implements OnInit {
  validateReservationForm: FormGroup;
  stateMessage: string;

  constructor(
    private fb: FormBuilder,
    private reservationService: ReservationService
  ) { }

  ngOnInit() {
    this.createvalidateReservationForm();
  }

  validateReservation() {
    const reservationId = this.validateReservationForm.get('reservationId').value;

    this.reservationService.getReservation(reservationId).subscribe(reservation => {
      if (reservation === null) {
        this.stateMessage = 'eksisterer ikke';
      } else if (reservation.bookingState === 2) {
        this.stateMessage = 'er betalt';
      } else if (reservation.bookingState === 1) {
        this.stateMessage = 'er reserveret';
      } else if (reservation.bookingState === 0) {
        this.stateMessage = 'er annulleret';
      }
    });
  }

  createvalidateReservationForm() {
    this.validateReservationForm = this.fb.group({
      reservationId: ['', Validators.required]
    });
  }

}
