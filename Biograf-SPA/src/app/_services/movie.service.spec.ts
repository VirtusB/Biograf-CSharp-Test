/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MovieService } from './movie.service';

import { HttpClientModule } from '@angular/common/http';

describe('Service: Movie', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MovieService],
      imports: [HttpClientModule ]
    });
  });

  it('should ...', inject([MovieService], (service: MovieService) => {
    expect(service).toBeTruthy();
  }));
});
