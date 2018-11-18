import { Component, OnInit } from '@angular/core';
import { Show } from '../../_models/show';
import { ShowService } from '../../_services/show.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../../_models/Pagination';



@Component({
  selector: 'app-show-list',
  templateUrl: './show-list.component.html',
  styleUrls: ['./show-list.component.css']
})
export class ShowListComponent implements OnInit {
  shows: Show[];
  showParams: any = {};
  pagination: Pagination;
  prices = [100, 200, 300, 400, 500];
  stars = [1, 2, 3, 4, 5];


  constructor(private showService: ShowService, private alertify: AlertifyService, private route: ActivatedRoute) { }


  ngOnInit() {
    this.route.data.subscribe(data => {
      this.shows = data['shows'].result;
      this.pagination = data['shows'].pagination;
    });

    this.showParams.maxTicketPrice = 500;
    this.showParams.stars = 0;

  }



  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadShows();
  }

  resetFilters() {
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



