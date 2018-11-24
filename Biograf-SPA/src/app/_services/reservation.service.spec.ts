/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReservationService } from './reservation.service';
import { HttpClientModule } from '@angular/common/http';

describe('Service: Reservation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReservationService],
      imports: [HttpClientModule ]
    });
  });

  it('should ...', inject([ReservationService], (service: ReservationService) => {
    expect(service).toBeTruthy();
  }));
});
