import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Discount } from '../../_models/discount';
import { BsModalRef } from 'ngx-bootstrap';
import { DiscountService } from '../../_services/discount.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-edit-discount-modal',
  templateUrl: './edit-discount-modal.component.html',
  styleUrls: ['./edit-discount-modal.component.css']
})
export class EditDiscountModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  @ViewChild('editForm') editForm: NgForm;
  discount: Discount;

  constructor(
    public bsModalRef: BsModalRef,
    private discountService: DiscountService,
    private alertify: AlertifyService,
    private authService: AuthService
    ) {}

  ngOnInit() {
  }

  saveChangesToDiscount() {
    this.discountService.updateDiscountByAdmin(this.discount.id, this.discount).subscribe(next => {
      this.alertify.success('Rabat opdateret');
      this.editForm.reset(this.discount);
    }, error => {
      this.alertify.error(error);
    });


    this.bsModalRef.hide();
  }

}
