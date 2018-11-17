import { Injectable } from '@angular/core';
import { Movie } from '../_models/movie';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { MovieService } from '../_services/movie.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MovieDetailResolver implements Resolve<Movie> {


    constructor(private movieService: MovieService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Movie> {
        return this.movieService.getMovie(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem ved load af data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
