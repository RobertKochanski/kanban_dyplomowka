import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { JobsService } from 'src/app/_services/jobs.service';
import { DatePipe } from '@angular/common'
import { JobData } from 'src/app/_models/jobData';

@Component({
  selector: 'app-edit-job-dialog',
  templateUrl: './edit-job-dialog.component.html',
  styleUrls: ['./edit-job-dialog.component.css']
})
export class EditJobDialogComponent {
  job: JobData;
  members: any;
  minDate: Date;

  date = new Date();

  form = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    userEmails: [[]],
    deadline: [],
    priority: ['High']
  })

  constructor(
    public datepipe: DatePipe,
    private fb: FormBuilder, 
    private jobService: JobsService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<EditJobDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnInit(): void {
    this.job = this.data[0];
    this.members = this.data[1];
    this.minDate = new Date;

    let latest_date = this.datepipe.transform(this.data[0].deadline, 'dd MMMM YYYY');
    this.form.controls['deadline'].setValue(latest_date);
    this.form.controls['priority'].setValue(this.job.priority);
  }

  editJob(){
    var date = new Date(this.form.controls['deadline'].value)
    var dateFixed = new Date(date.setMinutes(date.getMinutes() - date.getTimezoneOffset()));
    this.form.controls['deadline'].setValue(dateFixed);
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
