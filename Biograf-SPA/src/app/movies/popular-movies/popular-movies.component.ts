import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../_services/movie.service';
import { Movie } from '../../_models/movie';

@Component({
  selector: 'app-popular-movies',
  templateUrl: './popular-movies.component.html',
  styleUrls: ['./popular-movies.component.css']
})
export class PopularMoviesComponent implements OnInit {
  movies: Movie[];

  constructor(private movieService: MovieService) { }

  ngOnInit() {
    this.getFiveMostPopular();
  }

  getFiveMostPopular() {
    this.movieService.getFiveMostPopular().subscribe(data => {
      this.movies = data.body;
    });
  }

}
