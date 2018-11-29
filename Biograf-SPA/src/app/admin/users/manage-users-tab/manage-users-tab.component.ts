import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../../_models/user';
import { Pagination, PaginatedResult } from '../../../_models/pagination';
import { UserService } from '../../../_services/user.service';
import { AlertifyService } from '../../../_services/alertify.service';


@Component({
  selector: 'app-manage-users-tab',
  templateUrl: './manage-users-tab.component.html',
  styleUrls: ['./manage-users-tab.component.css']
})
export class ManageUsersTabComponent implements OnInit {
  users: User[];
  userParams: any = {};
  pagination: Pagination;
  enabled: any;
  pageSizes = [5, 10, 15, 20];
  pageSize: number;


  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });


    this.userParams.enabled = true;
    this.userParams.pageSize = 5;


    this.loadUsers();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.enabled = true;
    this.userParams.pageSize = 5;
    this.loadUsers();
  }



  loadUsers() {
    this.userService.
    getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
    .subscribe((res: PaginatedResult<User[]>) => {
      this.users = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
