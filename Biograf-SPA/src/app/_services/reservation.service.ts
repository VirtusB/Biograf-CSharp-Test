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

  

  setBookingState(userId: number, id: number, reservation: Reservation) {
    return this.http.post<Reservation>(this.baseUrl + 'reservations/' + userId + '/' + id, reservation);
  }

  getReservation(id: number): Observable<Reservation> {
    let reservation: Reservation;

    return this.http.get<Reservation>(this.baseUrl + 'reservations/' + id, { observe: 'response'})
      .pipe(
        map(response => {
          reservation = response.body;
          return reservation;
        })
      );
  }

  getReservations(id: number): Observable<Reservation[]> {

    let reservations: Reservation[] = [];

    return this.http.get<Reservation[]>(this.baseUrl + 'reservations/all/' + id, { observe: 'response'})
      .pipe(
        map(response => {
          reservations = response.body;
          return reservations;
        })
      );
  }



  createReservations(id: number, reservations: Reservation[]) {
    return this.http.post(this.baseUrl + 'reservations/' + id, reservations);
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
