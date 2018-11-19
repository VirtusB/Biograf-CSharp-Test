import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../_models/user';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { EditUserModalComponent } from '../edit-user-modal/edit-user-modal.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  @Input() users: User[];
  bsModalRef: BsModalRef;
  

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
  }

  editUserModal(id: number) {
    let user = this.users.find((u) => {
      return u.id === id;
    });

    const initialState = {
      user,
      title: 'Rediger bruger'
    };
    this.bsModalRef = this.modalService.show(EditUserModalComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Gem bruger';
  }
  }




