/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DiscountService } from './discount.service';

import { HttpClientModule } from '@angular/common/http';

describe('Service: Discount', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DiscountService],
      imports: [HttpClientModule ]
    });
  });

  it('should ...', inject([DiscountService], (service: DiscountService) => {
    expect(service).toBeTruthy();
  }));
});
