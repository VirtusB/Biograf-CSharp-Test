import { Component, OnInit } from '@angular/core';
import { Show } from '../../_models/show';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ShowService } from '../../_services/show.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AuthService } from '../../_services/auth.service';
import { Movie } from '../../_models/movie';
import { MovieService } from '../../_services/movie.service';

@Component({
  selector: 'app-add-show-modal',
  templateUrl: './add-show-modal.component.html',
  styleUrls: ['./add-show-modal.component.css']
})
export class AddShowModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  show: Show;
  showForm: FormGroup;
  public onSave: Subject<Show>;
  movies: Movie[];
  hallNumbers = [1, 2, 3, 4];

  constructor(
    public bsModalRef: BsModalRef,
    private showService: ShowService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private fb: FormBuilder,
    private movieService: MovieService
    ) {}



  ngOnInit() {
    this.onSave = new Subject();
    this.createShowForm();
    this.loadAllMovies();
  }

  createShowForm() {
    this.showForm = this.fb.group({
      ticketPrice: ['', Validators.required],
      movie: ['', Validators.required],
      hallNumber: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });
  }

  loadAllMovies() {
    this.movieService.getAllMoviesWithoutPagination().subscribe((data) => {
      this.movies = data;
    });
  }



  createShow() {
    this.show = Object.assign({}, this.showForm.value);


    this.showService.createShow(this.show).subscribe((addedShow: Show) => {
      this.alertify.success('Forestillingen blev oprettet');
      this.onSave.next(addedShow);
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.showForm.reset();
    });


    this.bsModalRef.hide();
  }

}
