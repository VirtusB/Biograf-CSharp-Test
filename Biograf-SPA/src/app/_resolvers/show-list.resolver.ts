import { Injectable } from '@angular/core';
import { Show } from '../_models/show';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { ShowService } from '../_services/show.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ShowListResolver implements Resolve<Show[]> {
    pageNumber = 1;
    pageSize = 6;

    constructor(private showService: ShowService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Show[]> {
        return this.showService.getShows(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem ved load af data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
