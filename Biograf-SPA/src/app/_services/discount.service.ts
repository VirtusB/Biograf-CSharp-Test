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

updateDiscountByAdmin(id: number, discount: Discount) {
  return this.http.put(this.baseUrl + 'discounts/' + id, discount);
}

}

