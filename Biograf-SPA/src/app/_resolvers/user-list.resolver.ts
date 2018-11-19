import { Injectable } from '@angular/core';
import { Movie } from '../_models/movie';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { MovieService } from '../_services/movie.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';

@Injectable()
export class UserListResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 5;

    constructor(private userService: UserService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.getUsers(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem ved load af data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
