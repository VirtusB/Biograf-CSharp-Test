/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ShowService } from './show.service';
import { HttpClientModule } from '@angular/common/http';

describe('Service: Show', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ShowService],
      imports: [HttpClientModule ]
    });
  });

  it('should ...', inject([ShowService], (service: ShowService) => {
    expect(service).toBeTruthy();
  }));
});
