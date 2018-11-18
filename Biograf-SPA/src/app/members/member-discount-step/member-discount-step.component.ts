import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../_services/reservation.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-member-discount-step',
  templateUrl: './member-discount-step.component.html',
  styleUrls: ['./member-discount-step.component.css']
})
export class MemberDiscountStepComponent implements OnInit {
  discountInformation: any;

  constructor(
    private reservationService: ReservationService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.getDiscountStep();
  }

  getDiscountStep() {
    this.reservationService.getDiscountStep(this.authService.decodedToken.nameid).subscribe(data => {
      this.discountInformation = data.body;
    });
  }

}
