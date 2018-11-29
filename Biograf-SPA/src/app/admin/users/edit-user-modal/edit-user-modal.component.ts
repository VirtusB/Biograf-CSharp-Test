import { Component, OnInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { Role } from 'src/app/_models/role';
import { User } from '../../../_models/user';
import isEqual from 'lodash/isEqual';

@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  user: User;
  roles: Role[];
  @ViewChild('editForm') editForm: NgForm;
  selectedRole: Role;
  copyOfUser: User;

  constructor(
    public bsModalRef: BsModalRef,
    private userService: UserService,
    private alertify: AlertifyService,
    private authService: AuthService
    ) {}

  ngOnInit() {
    this.getRoles();
    this.copyOfUser = Object.assign({}, this.user);
  }

  hasChanges() {
    return !isEqual(this.user, this.copyOfUser);
  }

  getRoles() {
    this.userService.
    getRoles()
    .subscribe((res: Role[]) => {
      this.roles = res;
      this.selectedRole = this.roles.find((r) => r.id === this.user.role.id);
    }, error => {
      this.alertify.error(error);
    });
  }

  saveChangesToUser() {
    this.user.role = this.selectedRole;



    this.userService.updateUserByAdmin(this.user.id, this.user).subscribe(next => {
      this.alertify.success('Bruger opdateret');
      this.editForm.reset(this.user);
    }, error => {
      this.alertify.error(error);
    });


    this.bsModalRef.hide();
  }

}
