import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';
import { BoardsService } from 'src/app/_services/boards.service';

@Component({
  selector: 'app-members-dialog',
  templateUrl: './members-dialog.component.html',
  styleUrls: ['./members-dialog.component.css']
})
export class MembersDialogComponent {
  members: any;
  board: any;

  constructor(
    public accountService: AccountService,
    private boardService: BoardsService,
    private toastr: ToastrService,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<MembersDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) 
    { }

  ngOnInit(): void {
    this.board = this.data;
    this.members = this.board.members;
  }

  removeMember(userId: any){
    if(confirm("Are you sure you want to remove member?")){
      this.boardService.removeMember(this.board.id, userId).subscribe(response => {
        this.members = this.members.filter(x => x.id !== userId);
        this.toastr.error("Member Removed");
      }, error => {
        this.toastr.error(error);
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
}
