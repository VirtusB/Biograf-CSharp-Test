import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Movie } from '../../_models/movie';
import { MovieService } from '../../_services/movie.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-manage-movies-tab',
  templateUrl: './manage-movies-tab.component.html',
  styleUrls: ['./manage-movies-tab.component.css']
})
export class ManageMoviesTabComponent implements OnInit {
  movieForm: FormGroup;
  movie: Movie;
  stars = [1, 2, 3, 4, 5];

  constructor(
    private fb: FormBuilder,
    private movieService: MovieService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.createMovieForm();
  }

  createMovieForm() {
    this.movieForm = this.fb.group({
      name: ['', Validators.required],
      genre: ['', Validators.required],
      minutes: ['', Validators.required],
      year: ['', Validators.required],
      director: ['', Validators.required],
      poster: ['', Validators.required],
      description: ['', Validators.required],
      stars: [Validators.required]
    });
  }

  createMovie() {
    this.movie = Object.assign({}, this.movieForm.value);
    this.movieService.createMovie(this.movie).subscribe(() => {
      this.alertify.success('Filmen blev oprettet');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.movieForm.reset();
    });
  }

}
