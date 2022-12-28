import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserData } from 'src/app/_models/userData';
import { JobsService } from 'src/app/_services/jobs.service';

@Component({
  selector: 'app-create-job-dialog',
  templateUrl: './create-job-dialog.component.html',
  styleUrls: ['./create-job-dialog.component.css']
})
export class CreateJobDialogComponent {
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
    private dialogRef: MatDialogRef<CreateJobDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: {users: UserData[]}) 
    { }

  ngOnInit(): void {
    this.members = this.data
  }

  createJob(){
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
