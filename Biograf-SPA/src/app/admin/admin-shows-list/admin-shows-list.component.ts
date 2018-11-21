import { Component, OnInit, Input } from '@angular/core';
import { Show } from '../../_models/show';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { AuthService } from '../../_services/auth.service';
import { ShowService } from '../../_services/show.service';
import { AlertifyService } from '../../_services/alertify.service';
import { EditShowModalComponent } from '../edit-show-modal/edit-show-modal.component';
import { AddShowModalComponent } from '../add-show-modal/add-show-modal.component';

@Component({
  selector: 'app-admin-shows-list',
  templateUrl: './admin-shows-list.component.html',
  styleUrls: ['./admin-shows-list.component.css']
})
export class AdminShowsListComponent implements OnInit {
  @Input() shows: Show[];
  editShowModalRef: BsModalRef;
  addShowModalRef: BsModalRef;


  constructor(
    private authService: AuthService,
    private showService: ShowService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
  }

  editShowModal(id: number) {
    const show = this.shows.find((u) => {
      return u.id === id;
    });

    const initialState = {
      show,
      title: 'Rediger forestilling'
    };
    this.editShowModalRef = this.modalService.show(EditShowModalComponent, {initialState});
    this.editShowModalRef.content.closeBtnName = 'Gem forestilling';
  }

  deleteShow(id: number) {
    this.alertify.confirm('Slet forestilling', 'Er du sikker?', () => {
      this.showService.deleteShow(id).subscribe((res) => {
        const index = this.shows.findIndex((u) => u.id === id);

        this.shows.splice(index, 1);
        this.alertify.success('Forestilling slettet');
      }, error => {
        this.alertify.error(error);
      });
    });
  }

  addNewShowModal() {
    const initialState = {
      title: 'Opret Forestilling'
    };
    this.addShowModalRef = this.modalService.show(AddShowModalComponent, {initialState});
    this.addShowModalRef.content.closeBtnName = 'Opret forestilling';
    this.addShowModalRef.content.onSave.subscribe(addedShow => {
      this.shows.push(addedShow);
    });
  }

}
