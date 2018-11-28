import { Component, OnInit } from '@angular/core';
import { Movie } from '../../_models/movie';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { AuthService } from '../../_services/auth.service';
import { MovieService } from '../../_services/movie.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-member-movie-favorites',
  templateUrl: './member-movie-favorites.component.html',
  styleUrls: ['./member-movie-favorites.component.css']
})
export class MemberMovieFavoritesComponent implements OnInit {
  movies: Movie[];
  pagination: Pagination;
  likesParam: string;

  constructor(private authService: AuthService,
    private movieService: MovieService,
    private route: ActivatedRoute,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.movies = data['movies'].result;
      this.pagination = data['movies'].pagination;
    });

    this.likesParam = 'Likees';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMovies();
  }

  loadMovies() {
    this.movieService.
    getMovies(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likesParam)
    .subscribe((res: PaginatedResult<Movie[]>) => {
      this.movies = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
