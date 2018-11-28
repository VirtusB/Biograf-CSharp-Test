import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HubConnection, HubConnectionBuilder  } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubService {
  staffHubUrl = environment.staffHubUrl;


constructor() { }

getStaffHub() {
  return new HubConnectionBuilder().withUrl(this.staffHubUrl).build();
}

}
