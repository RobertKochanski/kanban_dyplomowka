import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { UserData } from '../_models/userData';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection?: HubConnection;

  constructor(private toastr: ToastrService) { }

  createHubConnection(user: UserData){
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch(error => console.log(error));

    this.hubConnection.on('UserIsOnline', email => {
      this.toastr.info(email + ' has connected');
    })

    this.hubConnection.on('UserIsOffline', email => {
      this.toastr.info(email + ' has disconnected');
    })
  }

  stopHubConnection(){
    this.hubConnection.stop().catch(error => console.log(error));
  }
}
