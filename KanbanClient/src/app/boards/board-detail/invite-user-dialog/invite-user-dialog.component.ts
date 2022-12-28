import { Component, Inject, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { InvitationsService } from 'src/app/_services/invitations.service';
import { BoardDetailComponent } from '../board-detail.component';

@Component({
  selector: 'app-invite-user-dialog',
  templateUrl: './invite-user-dialog.component.html',
  styleUrls: ['./invite-user-dialog.component.css']
})
export class InviteUserDialogComponent implements OnInit {
  model: any = {};

  constructor(private invitationService: InvitationsService, private dialog: MatDialog,
    private dialogRef: MatDialogRef<InviteUserDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit(): void {
  }

  sendInvitation(){
    this.dialogRef.close(this.model);
  }

  close() {
    this.dialogRef.close();
  }
}
