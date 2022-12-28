import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EditJobDialogComponent } from '../edit-job-dialog/edit-job-dialog.component';

@Component({
  selector: 'app-job-details-dialog',
  templateUrl: './job-details-dialog.component.html',
  styleUrls: ['./job-details-dialog.component.css']
})
export class JobDetailsDialogComponent {
  job: any

  constructor(
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<EditJobDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnInit(): void {
    this.job = this.data;
  }

  close() {
    this.dialogRef.close();
  }
}
