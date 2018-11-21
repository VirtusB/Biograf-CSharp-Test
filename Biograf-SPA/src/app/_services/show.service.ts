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

    updateShowByAdmin(id: number, show: Show) {
      return this.http.put(this.baseUrl + 'shows/' + id, show);
    }

    deleteShow(id: number) {
      return this.http.delete(this.baseUrl + 'shows/' + id);
    }

    createShow(show: Show) {
      return this.http.post(this.baseUrl + 'shows/', show);
      }

      getAllShowsWithoutPagination(): Observable<Show[]> {
        return this.http.get<Show[]>(this.baseUrl + 'shows/all', { observe: 'response'})
          .pipe(
            map(response => {
              return response.body;
            })
          );
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
          params = params.append('hallNumber', showParams.hallNumber);

          if (showParams.pageSize) {
            params = params.set('pageSize', showParams.pageSize);
          }
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
