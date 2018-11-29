import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap';
import { Movie } from '../../../_models/movie';
import { AlertifyService } from '../../../_services/alertify.service';
import { AuthService } from '../../../_services/auth.service';
import { MovieService } from '../../../_services/movie.service';
import isEqual from 'lodash/isEqual';

@Component({
  selector: 'app-edit-movie-modal',
  templateUrl: './edit-movie-modal.component.html',
  styleUrls: ['./edit-movie-modal.component.css']
})
export class EditMovieModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  @ViewChild('editForm') editForm: NgForm;
  movie: Movie;
  stars = [1, 2, 3, 4, 5];
  genres: string[];
  copyOfMovie: Movie;

  constructor(
    public bsModalRef: BsModalRef,
    private alertify: AlertifyService,
    private authService: AuthService,
    private movieService: MovieService
    ) {}

  ngOnInit() {
    this.loadGenres();
    this.copyOfMovie = Object.assign({}, this.movie);
  }

  loadGenres() {
    this.movieService.getGenres().subscribe((res: any) => {
      this.genres = res.body;
    }, error => {
      this.alertify.error(error);
    });
  }

  hasChanges() {
    return !isEqual(this.movie, this.copyOfMovie);
  }


  saveChangesToMovie() {
    this.movieService.updateMovieByAdmin(this.movie.id, this.movie).subscribe(next => {
      this.alertify.success('Film opdateret');
      return;
    }, error => {
      this.alertify.error(error);
    });


    this.bsModalRef.hide();
  }

}
