import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Show } from '../../../_models/show';
import { Pagination, PaginatedResult } from '../../../_models/pagination';
import { ShowService } from '../../../_services/show.service';
import { AlertifyService } from '../../../_services/alertify.service';

@Component({
  selector: 'app-manage-shows-tab',
  templateUrl: './manage-shows-tab.component.html',
  styleUrls: ['./manage-shows-tab.component.css']
})
export class ManageShowsTabComponent implements OnInit {
  shows: Show[];
  showParams: any = {};
  pagination: Pagination;
  pageSizes = [5, 10, 15, 20];
  pageSize: number;
  prices = [100, 200, 300, 400, 500];
  stars = [1, 2, 3, 4, 5];


  constructor(private showService: ShowService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.shows = data['shows'].result;
      this.pagination = data['shows'].pagination;
    });


    this.showParams.pageSize = 5;
    this.showParams.maxTicketPrice = 500;
    this.showParams.stars = 0;


    this.loadShows();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadShows();
  }

  resetFilters() {
    this.showParams.pageSize = 5;
    this.showParams.maxTicketPrice = 500;
    this.showParams.stars = 0;
    this.loadShows();
  }



  loadShows() {
    this.showService.
    getShows(this.pagination.currentPage, this.pagination.itemsPerPage, this.showParams)
    .subscribe((res: PaginatedResult<Show[]>) => {
      this.shows = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
