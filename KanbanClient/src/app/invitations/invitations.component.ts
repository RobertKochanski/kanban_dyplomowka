import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { InvitationData } from '../_models/invitationData';
import { InvitationsService } from '../_services/invitations.service';

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.css']
})
export class InvitationsComponent implements OnInit {
  invitations: InvitationData[] = [];

  constructor(private invitationService: InvitationsService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadInvitations();
  }

  loadInvitations(){
    this.invitationService.getInvitations().subscribe(invitations => {
      this.invitations = invitations.data
    })
  }

  acceptInvitation(id: any){
    // accept invite
    this.invitationService.acceptInvitation(id, null).subscribe(() => {
      this.invitations = this.invitations.filter(x => x.id !== id);
        this.toastr.success("Invitation Accepted")
    })

    //and delete invite
    this.invitationService.deleteInvitation(id).subscribe(() => {
      this.invitations = this.invitations.filter(x => x.id !== id);
    })
  }

  rejectInvitation(id: any){
    if(confirm("Are you sure you want to delete this invide?")){
      this.invitationService.deleteInvitation(id).subscribe(() => {
        this.invitations = this.invitations.filter(x => x.id !== id);
        this.toastr.error("Invitation Deleted")
      })
    }
  }


}
