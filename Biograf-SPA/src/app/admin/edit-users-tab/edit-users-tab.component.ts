import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-users-tab',
  templateUrl: './edit-users-tab.component.html',
  styleUrls: ['./edit-users-tab.component.css']
})
export class EditUsersTabComponent implements OnInit {
  users: User[];
  userParams: any = {};
  pagination: Pagination;
  enabled: any;

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });

    
    this.userParams.enabled = true;

    this.loadUsers();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  resetFilters() {
    this.userParams.enabled = true;
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
