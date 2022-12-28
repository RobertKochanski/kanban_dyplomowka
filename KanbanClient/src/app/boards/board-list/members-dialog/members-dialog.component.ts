import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EditJobDialogComponent } from '../../board-detail/edit-job-dialog/edit-job-dialog.component';

@Component({
  selector: 'app-members-dialog',
  templateUrl: './members-dialog.component.html',
  styleUrls: ['./members-dialog.component.css']
})
export class MembersDialogComponent {
  members: any

  constructor(
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<EditJobDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnInit(): void {
    this.members = this.data;
  }

  close() {
    this.dialogRef.close();
  }
}
