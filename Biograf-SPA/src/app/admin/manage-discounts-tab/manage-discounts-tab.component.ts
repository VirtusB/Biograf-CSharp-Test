import { Component, OnInit } from '@angular/core';
import { Discount } from '../../_models/discount';
import { DiscountService } from '../../_services/discount.service';
import { Observable } from 'rxjs';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { EditDiscountModalComponent } from '../edit-discount-modal/edit-discount-modal.component';

@Component({
  selector: 'app-manage-discounts-tab',
  templateUrl: './manage-discounts-tab.component.html',
  styleUrls: ['./manage-discounts-tab.component.css']
})
export class ManageDiscountsTabComponent implements OnInit {
  discounts: Discount[];
  bsModalRef: BsModalRef;

  constructor(
    private discountService: DiscountService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
    this.getDiscounts();
  }

  getDiscounts() {
    this.discountService.
    getDiscounts()
    .subscribe((res: Discount[]) => {
      this.discounts = res;
    }, error => {
      this.alertify.error(error);
    });
  }

  editDiscountModal(id: number) {
    let discount = this.discounts.find((u) => {
      return u.id === id;
    });

    const initialState = {
      discount,
      title: 'Rediger rabat'
    };
    this.bsModalRef = this.modalService.show(EditDiscountModalComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Gem rabat';
  }

}
