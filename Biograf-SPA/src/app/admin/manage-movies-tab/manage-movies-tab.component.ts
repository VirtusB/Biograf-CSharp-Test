import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Movie } from '../../_models/movie';
import { MovieService } from '../../_services/movie.service';
import { AlertifyService } from '../../_services/alertify.service';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manage-movies-tab',
  templateUrl: './manage-movies-tab.component.html',
  styleUrls: ['./manage-movies-tab.component.css']
})
export class ManageMoviesTabComponent implements OnInit {
  movies: Movie[];
  movieParams: any = {};
  pagination: Pagination;
  pageSizes = [5, 10, 15, 20];
  pageSize: number;
  stars = [1, 2, 3, 4, 5];
  genres: string[];


  constructor(private movieService: MovieService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.movies = data['movies'].result;
      this.pagination = data['movies'].pagination;
    });


    this.movieParams.pageSize = 5;
    this.movieParams.minYear = 1920;
    this.movieParams.maxYear = 2018;
    this.movieParams.genre = '';
    this.movieParams.stars = 0;

    this.loadGenres();
    this.loadMovies();
  }

  loadGenres() {
    this.movieService.getGenres().subscribe((res: any) => {
      this.genres = res.body;
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMovies();
  }

  resetFilters() {
    this.movieParams.pageSize = 5;
    this.movieParams.minYear = 1920;
    this.movieParams.maxYear = 2018;
    this.movieParams.genre = '';
    this.movieParams.stars = 0;
    this.loadMovies();
  }



  loadMovies() {
    this.movieService.
    getMovies(this.pagination.currentPage, this.pagination.itemsPerPage, this.movieParams)
    .subscribe((res: PaginatedResult<Movie[]>) => {
      this.movies = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
