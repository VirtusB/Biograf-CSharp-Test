import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Discount } from '../_models/discount';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

getDiscounts(): Observable<Discount[]> {
  return this.http.get<Discount[]>(this.baseUrl + 'discounts');
}

createDiscount(discount: Discount) {
  return this.http.post(this.baseUrl + 'discounts/', discount);
  }

deleteDiscount(id: number) {
  return this.http.delete(this.baseUrl + 'discounts/' + id);
}

getDiscountStep(id: number) {
  return this.http.get<number>(this.baseUrl + 'reservations/discountstep/' + id, { observe: 'response'});
}

updateDiscountByAdmin(id: number, discount: Discount) {
  return this.http.put(this.baseUrl + 'discounts/' + id, discount);
}

}

