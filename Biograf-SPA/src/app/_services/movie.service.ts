import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../_models/movie';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable()
export class MovieService {

    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }


    getMovies(page?, itemsPerPage?, movieParams?): Observable<PaginatedResult<Movie[]>> {
        const paginatedResult: PaginatedResult<Movie[]> = new PaginatedResult<Movie[]>();

        let params = new HttpParams();

        if (page != null && itemsPerPage != null) {
          params = params.append('pageNumber', page);
          params = params.append('pageSize', itemsPerPage);
        }

        if (movieParams != null) {
          params = params.append('minYear', movieParams.minYear);
          params = params.append('maxYear', movieParams.maxYear);
          params = params.append('genre', movieParams.genre);

        }


        return this.http.get<Movie[]>(this.baseUrl + 'movies', { observe: 'response', params})
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
