import { Component, OnInit, Input } from '@angular/core';
import { ReservationService } from '../../_services/reservation.service';
import { AuthService } from '../../_services/auth.service';
import { DiscountService } from '../../_services/discount.service';
import { User } from '../../_models/user';

@Component({
  selector: 'app-member-discount-step',
  templateUrl: './member-discount-step.component.html',
  styleUrls: ['./member-discount-step.component.css']
})
export class MemberDiscountStepComponent implements OnInit {
  discountInformation: any;
  @Input() user: User;

  constructor(
    private discountService: DiscountService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.getDiscountStep();
  }

  getDiscountStep() {
    this.discountService.getDiscountStep(this.authService.decodedToken.nameid).subscribe(data => {
      this.discountInformation = data.body;
    });
  }

}
