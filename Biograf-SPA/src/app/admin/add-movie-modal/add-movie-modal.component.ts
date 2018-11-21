import { Component, OnInit } from '@angular/core';
import { Movie } from '../../_models/movie';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { AlertifyService } from '../../_services/alertify.service';
import { AuthService } from '../../_services/auth.service';
import { MovieService } from '../../_services/movie.service';

@Component({
  selector: 'app-add-movie-modal',
  templateUrl: './add-movie-modal.component.html',
  styleUrls: ['./add-movie-modal.component.css']
})
export class AddMovieModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  movie: Movie;
  movieForm: FormGroup;
  public onSave: Subject<Movie>;
  stars = [1, 2, 3, 4, 5];
  genres: string[];

  constructor(
    public bsModalRef: BsModalRef,
    private alertify: AlertifyService,
    private authService: AuthService,
    private fb: FormBuilder,
    private movieService: MovieService
    ) {}



  ngOnInit() {
    this.onSave = new Subject();
    this.createMovieForm();
    this.loadGenres();
  }

  loadGenres() {
    this.movieService.getGenres().subscribe((res: any) => {
      this.genres = res.body;
    }, error => {
      this.alertify.error(error);
    });
  }

  createMovieForm() {
    this.movieForm = this.fb.group({
      name: ['', Validators.required],
      stars: ['', Validators.required],
      genre: ['', Validators.required],
      poster: ['', Validators.required],
      director: ['', Validators.required],
      year: ['', Validators.required],
      minutes: ['', Validators.required],
      description: ['', Validators.required]
    });
  }





  createMovie() {
    this.movie = Object.assign({}, this.movieForm.value);


    this.movieService.createMovie(this.movie).subscribe((addedMovie: Movie) => {
      this.alertify.success('Filmen blev oprettet');
      this.onSave.next(addedMovie);
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.movieForm.reset();
    });


    this.bsModalRef.hide();
  }

}
