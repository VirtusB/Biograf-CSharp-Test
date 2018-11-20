import { Component, OnInit } from '@angular/core';
import { ShowService } from '../../_services/show.service';
import { MovieService } from '../../_services/movie.service';
import { Movie } from '../../_models/movie';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertifyService } from '../../_services/alertify.service';
import { Show } from '../../_models/show';

@Component({
  selector: 'app-create-show-tab',
  templateUrl: './create-show-tab.component.html',
  styleUrls: ['./create-show-tab.component.css']
})
export class CreateShowTabComponent implements OnInit {
  movies: Movie[];
  showForm: FormGroup;
  show: Show;
  halls = [1, 2, 3, 4];


  constructor(
    private showService: ShowService,
    private fb: FormBuilder,
    private movieService: MovieService,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.loadAllMovies();
    this.createShowForm();
    this.showForm.updateValueAndValidity();
  }

  createShowForm() {
    this.showForm = this.fb.group({
      movie: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      ticketPrice: ['', Validators.required],
      hallNumber: ['', Validators.required]
    });
  }

  loadAllMovies() {
    this.movieService.getAllMoviesWithoutPagination().subscribe((data) => {
      this.movies = data;
    });
  }

  createShow() {

    this.show = Object.assign({}, this.showForm.value);

    this.showService.createShow(this.show).subscribe(() => {
      this.alertify.success('Forestillingen blev oprettet');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.showForm.reset();
    });
  }

}
