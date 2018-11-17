import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation } from '../_models/reservation';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getReservations(id: number): Observable<Reservation[]> {

    let reservations: Reservation[] = [];

    return this.http.get<Reservation[]>(this.baseUrl + 'reservations/' + id, { observe: 'response'})
      .pipe(
        map(response => {
          reservations = response.body;
          return reservations;
        })
      );
  }

  getPaidReservationsCount(id: number) {
    let count: number;

    return this.http.get<number>(this.baseUrl + 'reservations/paidcount/' + id, { observe: 'response'})
      .pipe(
        map(response => {
          count = response.body;
          return count;
        })
      );
  }

}
