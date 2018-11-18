import { Component, OnInit, Input, ViewChild, ElementRef, ViewChildren, QueryList } from '@angular/core';
import { Show } from '../../_models/show';
import { ActivatedRoute, Router } from '@angular/router';
import { ShowService } from '../../_services/show.service';
import { Reservation } from '../../_models/reservation';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';
import range from 'lodash/range';
import { ReservationService } from '../../_services/reservation.service';



@Component({
  selector: 'app-reservation-checkout',
  templateUrl: './reservation-checkout.component.html',
  styleUrls: ['./reservation-checkout.component.css']
})
export class ReservationCheckoutComponent implements OnInit  {
  show: Show;
  finalOrderPrice = 0;
  reservationForm: FormGroup;
  seatRange = range(1, 51);
  selectedSeats = [];
  phoneNumber: number;




  constructor(
    private route: ActivatedRoute,
    private showService: ShowService,
    private authService: AuthService,
    private alertify: AlertifyService,
    private reservationService: ReservationService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  createReservationForm() {
    this.reservationForm = this.fb.group({
      bookingState: ['2'],
      creditCardNumber: ['', [Validators.required, Validators.minLength(16), Validators.maxLength(16)]],
      creditCardExpiry: ['', [Validators.required, Validators.pattern('(0[1-9]|1[0-9]|2[0-9]|3[01]).(0[1-9]|1[012]).[0-9]{4}')]],
      creditCardCvv: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]]
    });
  }


  updateFinalOrderPrice() {
    this.finalOrderPrice = this.selectedSeats.length * this.show.ticketPrice;
  }

  seatInSelectedSeats(seatNumber) {
    return this.selectedSeats.includes(seatNumber);
  }

  toggleSeat(seatNumber) {
    if (this.seatInSelectedSeats(seatNumber)) {
      const index = this.selectedSeats.indexOf(seatNumber);
      this.selectedSeats.splice(index, 1);
    } else {
      this.selectedSeats.push(seatNumber);
    }
    this.updateFinalOrderPrice();
  }


  ngOnInit() {
    this.createReservationForm();
    this.phoneNumber = this.authService.currentUser.phoneNumber;

    this.reservationForm.get('bookingState').valueChanges.subscribe(value => {
      if (value === '1') {
        this.reservationForm.controls['creditCardNumber'].disable();
        this.reservationForm.controls['creditCardExpiry'].disable();
        this.reservationForm.controls['creditCardCvv'].disable();
      } else {
        this.reservationForm.controls['creditCardNumber'].enable();
        this.reservationForm.controls['creditCardExpiry'].enable();
        this.reservationForm.controls['creditCardCvv'].enable();
      }
    });

    this.route.params.subscribe(params => {
      this.showService.getShow(params['id']).subscribe(data => {
        this.show = data.body;
      });
    });

  }

  calculateRow(fullSeat) {
    switch (true) {
      case (fullSeat < 11):
        return 1;
      case (fullSeat <= 20):
        return 2;
      case (fullSeat <= 30):
        return 3;
      case (fullSeat <= 40):
        return 4;
      case (fullSeat > 40):
        return 5;
    }
  }

  calculateSeatNumber(fullSeat: number) {
    let str: string = fullSeat.toString();
    let splitted: string[] = str.split('');

    if (str.length === 2) {
      if (splitted[1] === '0') {
        return 10;
      } else {
        return +splitted[1];
      }
    }

    if (str.length === 1) {
      if (str === '0') {
        return 10;
      } else {
        return +str;
      }
    }
  }

  makeReservation() {

    if (this.reservationForm.valid) {
      this.selectedSeats.forEach(seat => {
        let res = {
          user: {
            id: this.authService.currentUser.id
          },
          show: {
            id: this.show.id,
            movie: {
              id: this.show.movie.id
            }
          },
          bookingState: +this.reservationForm.get('bookingState').value,
          row: this.calculateRow(seat),
          seat: this.calculateSeatNumber(seat)
        } as Reservation;

        let reservation = {} as Reservation;
        reservation = Object.assign({}, res);


        this.reservationService.createReservation(this.authService.currentUser.id, reservation).subscribe(() => {
          this.alertify.success('Billetterne er nu bestilt');
        }, error => {
          this.alertify.error(error);
        }, () => {
          this.router.navigate(['/member/edit']);
        });


        // reservation.user = this.authService.currentUser;
        // reservation.show = this.show;
        // reservation.bookingState = this.reservationForm.get('bookingState').value;
        // reservation.row = this.calculateRow(seat);
        // reservation.seat = this.calculateSeatNumber(seat);
        
      });

    }
  }

}
