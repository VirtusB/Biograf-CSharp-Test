import { Component, OnInit } from '@angular/core';
import { Movie } from '../../_models/movie';
import { MovieService } from '../../_services/movie.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../../_models/Pagination';
import { User } from '../../_models/user';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {
  movies: Movie[];
  movieParams: any = {};
  pagination: Pagination;

  constructor(private movieService: MovieService, private alertify: AlertifyService, private route: ActivatedRoute) { }


  ngOnInit() {
    this.route.data.subscribe(data => {
      this.movies = data['movies'].result;
      this.pagination = data['movies'].pagination;
    });

    this.movieParams.minYear = 1920;
    this.movieParams.maxYear = 2018;
    this.movieParams.genre = 'Comedy';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMovies();
  }

  resetFilters() {
    this.movieParams.minYear = 1920;
    this.movieParams.maxYear = 2018;
    this.movieParams.genre = 'Comedy';
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
