import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Movie } from '../_models/movie';
import { MovieService } from '../_services/movie.service';

@Injectable()
export class MemberMovieFavoritesResolver implements Resolve<Movie[]> {
    pageNumber = 1;
    pageSize = 5;
    likesParam = 'Likees';

    constructor(private movieSerivce: MovieService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Movie[]> {
        return this.movieSerivce.getMovies(this.pageNumber, this.pageSize, null, this.likesParam).pipe(
            catchError(error => {
                this.alertify.error('Problem ved load af data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
