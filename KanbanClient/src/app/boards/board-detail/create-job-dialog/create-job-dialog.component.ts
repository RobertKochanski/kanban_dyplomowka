import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
  minDate: Date;

  form = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    userEmails: [[]],
    deadline: [''],
    priority: ['High']
  })

  constructor(
    private fb: FormBuilder, 
    private jobService: JobsService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<CreateJobDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: {users: UserData[]}) 
    { }

  ngOnInit(): void {
    this.members = this.data;
    this.minDate = new Date;
  }

  createJob(){
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
