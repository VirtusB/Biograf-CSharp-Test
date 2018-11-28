import { Component, OnInit } from '@angular/core';
import { HubService } from '../../_services/hub.service';
import { HubConnection } from '@aspnet/signalr';
import { Message } from '../../_models/message';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-staff-chat-room',
  templateUrl: './staff-chat-room.component.html',
  styleUrls: ['./staff-chat-room.component.css']
})
export class StaffChatRoomComponent implements OnInit {
  private hubConnection: HubConnection;
  messageToSend: Message = {name: this.authService.currentUser.username, body: ''};
  messages: Message[] = [];

  constructor(private hubService: HubService, public authService: AuthService) { }

  ngOnInit() {
    // this.messageToSend.name = this.authService.currentUser.username;
    // this.messageToSend.body = '';
    this.hubConnection = this.hubService.getStaffHub();

    this.hubConnection
      .start()
      .then(() => console.log('Forbindelse oprettet!'))
      .catch(err => console.log(`Kunne ikke forbinde: ${err}`));

      this.hubConnection.on('sendToAll', (message: Message) => {
        // const newMessage: Message = Object.assign({}, {name: message.name, body: message.body});
        this.messages.push(message);
      });
  }

  public sendMessage(): void {
    this.hubConnection
      .invoke('sendToAll', this.messageToSend)
      .catch(err => console.error(err));
    this.messageToSend.body = '';
  }

}
