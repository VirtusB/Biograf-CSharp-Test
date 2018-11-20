import { Component, OnInit, Input } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MovieService } from 'src/app/_services/movie.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-create-movie-tab',
  templateUrl: './create-movie-tab.component.html',
  styleUrls: ['./create-movie-tab.component.css']
})
export class CreateMovieTabComponent implements OnInit {
  movieForm: FormGroup;
  movie: Movie;

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
      stars: ['', Validators.required]
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
