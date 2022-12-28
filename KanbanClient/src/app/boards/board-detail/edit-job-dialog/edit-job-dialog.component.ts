import { Component, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JobData } from 'src/app/_models/jobData';
import { UserData } from 'src/app/_models/userData';
import { JobsService } from 'src/app/_services/jobs.service';
import { CreateJobDialogComponent } from '../create-job-dialog/create-job-dialog.component';

@Component({
  selector: 'app-edit-job-dialog',
  templateUrl: './edit-job-dialog.component.html',
  styleUrls: ['./edit-job-dialog.component.css']
})
export class EditJobDialogComponent {
  job: any
  members: any

  form = this.fb.group({
    name: [''],
    description: [''],
    userEmails: [[]]
  })

  constructor(
    private fb: FormBuilder, 
    private jobService: JobsService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<EditJobDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnInit(): void {
    this.job = this.data[0];
    this.members = this.data[1];
  }

  editJob(){
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
