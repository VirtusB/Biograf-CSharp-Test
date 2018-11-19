import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/user';
import { BsModalRef } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent implements OnInit {
  title: string;
  closeBtnName: string;
  user: User;
  @ViewChild('editForm') editForm: NgForm;
 
  constructor(public bsModalRef: BsModalRef) {}
 
  ngOnInit() {
    
  }

  saveChangesToUser() {
    console.log(this.user);
    this.bsModalRef.hide();
  }

}
