import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { EditUserModalComponent } from '../edit-user-modal/edit-user-modal.component';
import { AddUserModalComponent } from '../add-user-modal/add-user-modal.component';
import { ViewUserOrdersModalComponent } from '../view-user-orders-modal/view-user-orders-modal.component';
import { User } from '../../../_models/user';
import { AuthService } from '../../../_services/auth.service';
import { UserService } from '../../../_services/user.service';
import { AlertifyService } from '../../../_services/alertify.service';

@Component({
  selector: 'app-admin-users-list',
  templateUrl: './admin-users-list.component.html',
  styleUrls: ['./admin-users-list.component.css']
})
export class AdminUsersListComponent implements OnInit {
  @Input() users: User[];
  editUserModalRef: BsModalRef;
  addUserModalRef: BsModalRef;
  viewUserOrdersModalRef: BsModalRef;


  constructor(
    private authService: AuthService,
    private userService: UserService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
  }

  editUserModal(id: number) {
    const user = this.users.find((u) => {
      return u.id === id;
    });

    const initialState = {
      user,
      title: 'Rediger bruger'
    };
    this.editUserModalRef = this.modalService.show(EditUserModalComponent, {initialState});
    this.editUserModalRef.content.closeBtnName = 'Gem bruger';
  }

  deleteUser(id: number) {
    this.alertify.confirm('Slet bruger', 'Er du sikker?', () => {
      this.userService.deleteUser(id).subscribe((res) => {
        const index = this.users.findIndex((u) => u.id === id);

        this.users.splice(index, 1);
        this.alertify.success('Bruger slettet');
      }, error => {
        this.alertify.error(error);
      });
    });
  }

  addNewUserModal() {
    const initialState = {
      title: 'Opret bruger'
    };
    this.addUserModalRef = this.modalService.show(AddUserModalComponent, {initialState});
    this.addUserModalRef.content.closeBtnName = 'Opret bruger';
    this.addUserModalRef.content.onSave.subscribe(addedUser => {
      this.users.push(addedUser);
    });
  }

  viewUserOrdersModal(id: number) {
    const userId = id;
    const initialState = {
      userId,
      title: 'Se ordrer for bruger'
    };

    const otherState = Object.assign({initialState}, { class: 'gray modal-lg' });
    this.viewUserOrdersModalRef = this.modalService.show(ViewUserOrdersModalComponent, otherState);
    this.viewUserOrdersModalRef.content.closeBtnName = 'Luk';
  }

}
