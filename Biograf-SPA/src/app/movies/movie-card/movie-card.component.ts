import { Component, OnInit, Input } from '@angular/core';
import { Movie } from '../../_models/movie';
import { MovieService } from '../../_services/movie.service';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from '../../_services/alertify.service';


@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {
  @Input() movie: Movie;

  constructor(
    private authService: AuthService,
    private movieService: MovieService,
    private alertify: AlertifyService) {

     }

  ngOnInit() {
  }

}
