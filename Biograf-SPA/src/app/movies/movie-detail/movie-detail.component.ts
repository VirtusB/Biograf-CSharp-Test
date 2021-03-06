import { Component, OnInit, Input } from '@angular/core';
import { Movie } from '../../_models/movie';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { MovieService } from '../../_services/movie.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {
  movie: Movie;
  isFavorite = false;
  countUsersWithFavorite: number;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private movieService: MovieService,
    private alertify: AlertifyService,
    private route: ActivatedRoute) {

     }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.movie = data['movie'].body;
      if (this.authService.loggedIn()) {
        this.isFavoriteStatus(this.movie.id);
      }
      this.getCountOfUsersWhoFavorited(this.movie.id);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  isFavoriteStatus( movieId: number) {
    this.movieService.getFavorite(this.authService.decodedToken.nameid, movieId).subscribe(data => {
      if (data.status === 200) {
        this.isFavorite = true;
      }
    });
  }

  getCountOfUsersWhoFavorited(movieId: number) {
    this.movieService.getCountOfUsersWhoFavorited(movieId).subscribe(data => {
      this.countUsersWithFavorite = data.body;
    });
  }

  addFavorite(id: number) {
    this.movieService.addFavorite(this.authService.decodedToken.nameid, id).subscribe(data => {
      this.alertify.success('Du har tilføjet ' + this.movie.name);
      this.isFavorite = true;
      this.getCountOfUsersWhoFavorited(this.movie.id);
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteFavorite(id: number) {
    this.movieService.removeFavorite(this.authService.decodedToken.nameid, id).subscribe(data => {
      this.alertify.error(this.movie.name + ' blev fjernet');
      this.isFavorite = false;
      this.getCountOfUsersWhoFavorited(this.movie.id);
    }, error => {
      this.alertify.error(error);
    });
  }

}
