import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { EditMovieModalComponent } from '../edit-movie-modal/edit-movie-modal.component';
import { AddMovieModalComponent } from '../add-movie-modal/add-movie-modal.component';
import { Movie } from '../../../_models/movie';
import { AuthService } from '../../../_services/auth.service';
import { MovieService } from '../../../_services/movie.service';
import { AlertifyService } from '../../../_services/alertify.service';

@Component({
  selector: 'app-admin-movies-list',
  templateUrl: './admin-movies-list.component.html',
  styleUrls: ['./admin-movies-list.component.css']
})
export class AdminMoviesListComponent implements OnInit {
  @Input() movies: Movie[];
  editMovieModalRef: BsModalRef;
  addMovieModalRef: BsModalRef;


  constructor(
    private authService: AuthService,
    private movieService: MovieService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
  }

  editMovieModal(id: number) {
    const movie = this.movies.find((u) => {
      return u.id === id;
    });

    const initialState = {
      movie,
      title: 'Rediger film'
    };
    this.editMovieModalRef = this.modalService.show(EditMovieModalComponent, {initialState});
    this.editMovieModalRef.content.closeBtnName = 'Gem film';
  }

  deleteMovie(id: number) {
    this.alertify.confirm('Slet film', 'Er du sikker?', () => {
      this.movieService.deleteMovie(id).subscribe((res) => {
        const index = this.movies.findIndex((u) => u.id === id);

        this.movies.splice(index, 1);
        this.alertify.success('Filmen blev slettet');
      }, error => {
        this.alertify.error(error);
      });
    });
  }

  addNewMovieModal() {
    const initialState = {
      title: 'Opret film'
    };
    this.addMovieModalRef = this.modalService.show(AddMovieModalComponent, {initialState});
    this.addMovieModalRef.content.closeBtnName = 'Opret film';
    this.addMovieModalRef.content.onSave.subscribe(addedMovie => {
      this.movies.push(addedMovie);
    });
  }

}
