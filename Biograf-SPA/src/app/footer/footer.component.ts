import { Component, OnInit } from '@angular/core';
import { Movie } from '../_models/movie';
import { MovieService } from '../_services/movie.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  movies: Movie[];

  constructor(private movieService: MovieService) { }

  ngOnInit() {
    this.getFooterSliderMovies();
  }

  getFooterSliderMovies() {
    this.movieService.getFooterSliderMovies().subscribe(data => {
      this.movies = data.body;
    });
  }

}
