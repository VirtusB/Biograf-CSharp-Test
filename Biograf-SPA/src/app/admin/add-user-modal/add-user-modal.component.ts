import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AuthService } from '../../_services/auth.service';
import { Role } from '../../_models/role';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.css']
})
export class AddUserModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  user: User;
  userForm: FormGroup;
  public onSave: Subject<User>;


  constructor(
    public bsModalRef: BsModalRef,
    private userService: UserService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private fb: FormBuilder
    ) {}

  ngOnInit() {
    this.onSave = new Subject();
    this.createUserForm();
  }



  createUserForm() {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      name: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      country: ['', Validators.required],
      city: ['', Validators.required],
    });
  }



  createUser() {
    this.user = Object.assign({}, this.userForm.value);

    this.userService.createUser(this.user).subscribe((addedUser: User) => {
      this.alertify.success('Brugeren blev oprettet');
      this.onSave.next(addedUser);
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.userForm.reset();
    });


    this.bsModalRef.hide();
  }

}
