import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap';


import { Subject } from 'rxjs';
import { Discount } from '../../../_models/discount';
import { DiscountService } from '../../../_services/discount.service';
import { AlertifyService } from '../../../_services/alertify.service';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-discount-modal',
  templateUrl: './add-discount-modal.component.html',
  styleUrls: ['./add-discount-modal.component.css']
})
export class AddDiscountModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  discount: Discount;
  discountForm: FormGroup;
  public onSave: Subject<Discount>;

  constructor(
    public bsModalRef: BsModalRef,
    private discountService: DiscountService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private fb: FormBuilder
    ) {}



  ngOnInit() {
    this.onSave = new Subject();
    this.createDiscountForm();
  }

  createDiscountForm() {
    this.discountForm = this.fb.group({
      requiredBookings: ['', Validators.required],
      amount: ['', Validators.required]
    });
  }

  

  createDiscount() {
    this.discount = Object.assign({}, this.discountForm.value);

    this.discountService.createDiscount(this.discount).subscribe((addedDiscount: Discount) => {
      this.alertify.success('Rabat trinet blev oprettet');
      this.onSave.next(addedDiscount);
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.discountForm.reset();
    });


    this.bsModalRef.hide();
  }

}
