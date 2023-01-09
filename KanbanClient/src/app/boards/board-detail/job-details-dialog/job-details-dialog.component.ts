import { Component, Inject, OnDestroy } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { interval, Subscription } from 'rxjs';
import { CommentData } from 'src/app/_models/commentData';
import { AccountService } from 'src/app/_services/account.service';
import { CommentsService } from 'src/app/_services/comments.service';
import { EditJobDialogComponent } from '../edit-job-dialog/edit-job-dialog.component';

@Component({
  selector: 'app-job-details-dialog',
  templateUrl: './job-details-dialog.component.html',
  styleUrls: ['./job-details-dialog.component.css']
})
export class JobDetailsDialogComponent implements OnDestroy {
  job: any;
  board: any;
  comments: CommentData[];
  model: any = {};
  reload: Subscription;

  constructor(
    public accountService: AccountService,
    private commentsService: CommentsService,
    private toastr: ToastrService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<JobDetailsDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnDestroy(): void {
    this.reload.unsubscribe();
  }

  ngOnInit(): void {
    this.job = this.data[0];
    this.board = this.data[1];
    this.loadComments(this.job.id);
    this.Reload();
  }

  Reload(){
    this.reload = interval(5000).subscribe(() => {
      this.loadComments(this.job.id);
    })
  }

  loadComments(id: any){
    this.commentsService.getComments(id).subscribe(comments => {
      this.comments = comments.data;
    })
  }

  createComment(){
    this.commentsService.postComment(this.model, this.job.id).subscribe(response => {
      this.loadComments(this.job.id);
      this.toastr.success("Comment Created");
    }, error => {
      this.toastr.error(error);
    });

    this.model = {};
  }

  deleteComment(commentId: any){
    if(confirm("Are you sure you want to delete this comment?")){
      this.commentsService.deleteColumn(commentId).subscribe(() => {
        this.loadComments(this.job.id);
        this.toastr.error("Comment Deleted")
      }, error => {
        this.toastr.error(error);
      })
    }
  }

  close() {
    this.dialogRef.close();
  }
}
