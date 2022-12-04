import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BoardListResponse } from '../_models/boardListResponse';
import { InvitationReponse } from '../_models/invitationResponse';

@Injectable({
  providedIn: 'root'
})
export class InvitationsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  getInvitations(): Observable<InvitationReponse>{
    return this.http.get<InvitationReponse>(this.baseUrl + 'Invitations/UserInvitations');
  }

  postInvitation(model: any, boardId: any){
    return this.http.post(this.baseUrl + 'Invitations/' + boardId, model);
  }

  deleteInvitation(id: any){
    return this.http.delete(this.baseUrl + 'Invitations/' + id);
  }

  acceptInvitation(id: any, model: any){
    return this.http.put(this.baseUrl + 'Invitations/' + id, model);
  }
}
