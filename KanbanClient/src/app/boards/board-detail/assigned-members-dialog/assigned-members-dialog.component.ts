import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AccountService } from 'src/app/_services/account.service';
import { JobsService } from 'src/app/_services/jobs.service';
import { EditJobDialogComponent } from '../edit-job-dialog/edit-job-dialog.component';

@Component({
  selector: 'app-assigned-members-dialog',
  templateUrl: './assigned-members-dialog.component.html',
  styleUrls: ['./assigned-members-dialog.component.css']
})
export class AssignedMembersDialogComponent {
  members: any

  constructor(
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<AssignedMembersDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnInit(): void {
    this.members = this.data.userEmails;
  }

  close() {
    this.dialogRef.close();
  }
}
