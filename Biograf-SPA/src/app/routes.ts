import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { MovieListResolver } from './_resolvers/movie-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MovieDetailComponent } from './movies/movie-detail/movie-detail.component';
import { MovieDetailResolver } from './_resolvers/movie-detail.resolver';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            {path: 'movies', component: MovieListComponent, resolve: {movies: MovieListResolver}},
            {path: 'movies/:id', component: MovieDetailComponent, resolve: {movie: MovieDetailResolver}}
        ]
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'member/edit', component: MemberEditComponent, resolve: {user: MemberEditResolver}}
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
