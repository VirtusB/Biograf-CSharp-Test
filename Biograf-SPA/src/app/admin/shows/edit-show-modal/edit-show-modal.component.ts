import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap';
import { Show } from '../../../_models/show';
import { Movie } from '../../../_models/movie';
import { ShowService } from '../../../_services/show.service';
import { AlertifyService } from '../../../_services/alertify.service';
import { AuthService } from '../../../_services/auth.service';
import { MovieService } from '../../../_services/movie.service';
import isEqual from 'lodash/isEqual';

@Component({
  selector: 'app-edit-show-modal',
  templateUrl: './edit-show-modal.component.html',
  styleUrls: ['./edit-show-modal.component.css']
})
export class EditShowModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  @ViewChild('editForm') editForm: NgForm;
  show: Show;
  halls = [1, 2, 3, 4];
  movies: Movie[];
  selectedMovie: Movie;
  copyOfShow: Show;

  constructor(
    public bsModalRef: BsModalRef,
    private showService: ShowService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private movieService: MovieService
    ) {}

  ngOnInit() {
    this.loadAllMovies();
    this.copyOfShow = Object.assign({}, this.show);
  }

  loadAllMovies() {
    this.movieService.getAllMoviesWithoutPagination().subscribe((data) => {
      this.movies = data;
      this.selectedMovie = this.movies.find((r) => r.id === this.show.movie.id);
    });
  }

  hasChanges() {
    return !isEqual(this.show, this.copyOfShow);
  }

  saveChangesToShow() {
    this.show.movie = this.selectedMovie;



    this.showService.updateShowByAdmin(this.show.id, this.show).subscribe(next => {
      this.alertify.success('Forestilling opdateret');
      return;
    }, error => {
      this.alertify.error(error);
    });


    this.bsModalRef.hide();
  }

}
