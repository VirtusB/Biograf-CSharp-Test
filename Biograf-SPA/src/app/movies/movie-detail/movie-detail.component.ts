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
    });
  }

}
