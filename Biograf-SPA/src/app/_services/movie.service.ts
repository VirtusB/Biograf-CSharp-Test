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

    getGenres(): any {
      return this.http.get<string[]>(this.baseUrl + 'movies/genres', { observe: 'response'});
    }

    getMovie(id: number) {
      return this.http.get<Movie>(this.baseUrl + 'movies/' + id, { observe: 'response'});
    }

    createMovie(movie: Movie) {
    return this.http.post(this.baseUrl + 'movies/', movie);
    }

    updateMovieByAdmin(id: number, movie: Movie) {
      return this.http.put(this.baseUrl + 'movies/' + id, movie);
    }

    deleteMovie(id: number) {
      return this.http.delete(this.baseUrl + 'movies/' + id);
    }

    getAllMoviesWithoutPagination(): Observable<Movie[]> {
      return this.http.get<Movie[]>(this.baseUrl + 'movies/all', { observe: 'response'})
        .pipe(
          map(response => {
            return response.body;
          })
        );
    }


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
          params = params.append('stars', movieParams.stars);

          if (movieParams.pageSize) {
            params = params.set('pageSize', movieParams.pageSize);
          }
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
