import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BoardData } from 'src/app/_models/boardData';
import { BoardsService } from 'src/app/_services/boards.service';
import { InviteUserDialogComponent } from './invite-user-dialog/invite-user-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { InvitationsService } from 'src/app/_services/invitations.service';
import { ToastrService } from 'ngx-toastr';
import { ColumnData } from 'src/app/_models/columnData';
import { UserData } from 'src/app/_models/userData';
import { JobData } from 'src/app/_models/jobData';
import { ColumnsService } from 'src/app/_services/columns.service';
import { CreateColumnDialogComponent } from './create-column-dialog/create-column-dialog.component';
import { JobsService } from 'src/app/_services/jobs.service';
import { CreateJobDialogComponent } from './create-job-dialog/create-job-dialog.component';
import { EditJobDialogComponent } from './edit-job-dialog/edit-job-dialog.component';
import { JobDetailsDialogComponent } from './job-details-dialog/job-details-dialog.component';
import { AssignedMembersDialogComponent } from './assigned-members-dialog/assigned-members-dialog.component';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { EditColumnDialogComponent } from './edit-column-dialog/edit-column-dialog.component';
import { AccountService } from 'src/app/_services/account.service';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-board-detail',
  templateUrl: './board-detail.component.html',
  styleUrls: ['./board-detail.component.css']
})
export class BoardDetailComponent implements OnInit, OnDestroy {
  board: BoardData;
  columns: ColumnData[];
  members: UserData[];
  reload: Subscription;

  constructor(
    public accountService: AccountService,
    private boardService: BoardsService,
    private columnService: ColumnsService,
    private jobService: JobsService,
    private invitationService: InvitationsService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private toastr: ToastrService)
    { }

  ngOnDestroy(): void {
    this.reload.unsubscribe();
  }

  ngOnInit(): void {
    this.loadBoard();
    this.Reload();
  }

  Reload(){
    this.reload = interval(10000).subscribe(() => {
      this.loadBoard();
    })
  }

  loadBoard(){
    this.boardService.getBoard(this.route.snapshot.paramMap.get('id')).subscribe(board => {
      this.board = board.data;
      this.columns = this.board.columns;
      this.members = this.board.members;
    })
  }

  // Invitations
  openInvitationDialog(){
    const dialogConfig = new MatDialogConfig(); 
    
    const dialogRef = this.dialog.open(InviteUserDialogComponent, dialogConfig);
    
    dialogRef.afterClosed().subscribe(data => {
      if(data){
        this.invitationService.postInvitation(data, this.board.id).subscribe(response => {
          this.toastr.info("Sent an invitation");
        }, error => {
          this.toastr.error(error);
        });
      }

      }, error => {
        this.toastr.error(error);
      }
    );
  }

  // Columns
  openCreateColumnDialog(){
    const dialogRef = this.dialog.open(CreateColumnDialogComponent, {
      height: '200px',
      width: '300px'
    });
    
    dialogRef.afterClosed().subscribe(data => {
      if(data){
        this.columnService.postColumn(data, this.board.id).subscribe(response => {
          this.loadBoard();
          this.toastr.success("Column created");
        }, error => {
          this.toastr.error(error);
        });
      }

      }, error => {
        this.toastr.error(error);
      }
    );    
  }

  openEditColumnDialog(column: any){
    const dialogRef = this.dialog.open(EditColumnDialogComponent, {
      height: '200px',
      width: '300px',
      data: column,
    });
    
    dialogRef.afterClosed().subscribe(data => {
      if(data){
        this.columnService.putColumnName(data, column.id).subscribe(response => {
          this.loadBoard();
          this.toastr.success("Column name updated");
        }, error => {
          this.toastr.error(error);
        });
      } else {
        this.loadBoard();
      }

      }, error => {
        this.toastr.error(error);
      }
      );    
    }

  deleteColumn(id: any){
    if(confirm("Are you sure you want to delete this column? Each task will also be deleted")){
      this.columnService.deleteColumn(id).subscribe(() => {
        this.columns = this.columns.filter(x => x.id !== id);
        this.toastr.error("Column Deleted")
      })
    }
  }

  // Jobs
  openCreateJobDialog(columnId: any){
    const dialogRef = this.dialog.open(CreateJobDialogComponent, {
      height: '600px',
      width: '400px',
      data: this.members,
    });
    
    dialogRef.afterClosed().subscribe(data => {
      if(data){
        this.jobService.postJob(data, columnId).subscribe(response => {
          this.loadBoard();
          this.toastr.success("Task created");
        }, error => {
          this.toastr.error(error);
        });
      }

      }, error => {
        this.toastr.error(error);
      }
    );    
  }

  openEditJobDialog(job: any){    
    const dialogRef = this.dialog.open(EditJobDialogComponent, {
      height: '600px',
      width: '400px',
      data: [job, this.members],
    });
    
    dialogRef.afterClosed().subscribe(data => {
      if(data){
        this.jobService.putJob(data, job.id).subscribe(response => {
          this.loadBoard();
          this.toastr.success("Task updated");
        }, error => {
          this.toastr.error(error);
        });
      } else {
        this.loadBoard();
      }

      }, error => {
        this.toastr.error(error);
      }
    );    
  }

  openDetailsJobDialog(job: any){
    this.dialog.open(JobDetailsDialogComponent, {
      height: '750px',
      width: '600px',
      data: [job, this.board],
    });
  }

  deleteJob(id: any){
    if(confirm("Are you sure you want to delete this task?")){
      this.jobService.deleteJob(id).subscribe(() => {
        this.loadBoard();
        this.toastr.error("Task Deleted");
      }, error => {
        this.toastr.error(error);
      })
    }
  }

  // Members
  openAssignedMembersDialog(job: any){
    this.dialog.open(AssignedMembersDialogComponent, {
      height: '400px',
      width: '300px',
      data: job,
    });
  }


  // Drag and drop
  drop(event: CdkDragDrop<any>, currentId: any) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      console.log(event.previousContainer.data)
      console.log(event.container.data)
      console.log(currentId)
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
      this.columnService.putColumn(currentId, event.container.data).subscribe(() => {
        // this.loadBoard();
        this.toastr.info("Task Moved");
      }, error => {
        this.toastr.error(error);
      })
    }

    
  }
}
