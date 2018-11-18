import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Show } from '../_models/show';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable()
export class ShowService {

    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }



    getShow(id: number) {
      return this.http.get<Show>(this.baseUrl + 'shows/' + id, { observe: 'response'});
    }


    getShows(page?, itemsPerPage?, showParams?): Observable<PaginatedResult<Show[]>> {
        const paginatedResult: PaginatedResult<Show[]> = new PaginatedResult<Show[]>();

        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
          params = params.append('pageNumber', page);
          params = params.append('pageSize', itemsPerPage);
        }

        if (showParams != null) {
          params = params.append('maxTicketPrice', showParams.maxTicketPrice);
          params = params.append('stars', showParams.stars);
        }


        return this.http.get<Show[]>(this.baseUrl + 'shows', { observe: 'response', params})
          .pipe(
            map(response => {
              paginatedResult.result = response.body;
              if (response.headers.get('Pagination') != null) {
                paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
              }
              return paginatedResult;
            })
          );
      }

}
