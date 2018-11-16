import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { MovieListResolver } from './_resolvers/movie-list.resolver';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'movies', component: MovieListComponent, resolve: {movies: MovieListResolver}}
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
