import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { interval, Subscription } from 'rxjs';
import { BoardData } from 'src/app/_models/boardData';
import { AccountService } from 'src/app/_services/account.service';
import { BoardsService } from 'src/app/_services/boards.service';
import { MembersDialogComponent } from './members-dialog/members-dialog.component';

@Component({
  selector: 'app-board-list',
  templateUrl: './board-list.component.html',
  styleUrls: ['./board-list.component.css']
})
export class BoardListComponent implements OnInit {
  boards: BoardData[];
  createMode = false;
  reloadBoards: Subscription;

  constructor(
    private dialog: MatDialog,
    private boardService: BoardsService,
    public accountService: AccountService, 
    private toastr: ToastrService) { }

  ngOnDestroy(): void {
    this.reloadBoards.unsubscribe();
  }

  ngOnInit(): void {
    this.loadBoards();
    this.Reload();
  }

  Reload(){
    this.reloadBoards = interval(5000).subscribe(() => {
      this.loadBoards();
    })
  }

  loadBoards(){
    this.boardService.getBoards().subscribe(boards => {
      this.boards = boards.data
    })
  }

  deleteBoard(id: any){
    if(confirm("Are you sure you want to delete this board?")){
      this.boardService.deleteBoard(id).subscribe(() => {
        this.boards = this.boards.filter(x => x.id !== id);
        this.toastr.error("Board Deleted")
      })
    }
  }

  createBoardToggle(){
    this.createMode = true;
  }

  cancelCreateBoard(event: boolean){
    this.createMode = event;
  }

  reload(){
    this.createMode = false;
    this.loadBoards();
    this.toastr.info("Page Reloaded");
  }

  openAssignedMembersDialog(board: any){
    this.dialog.open(MembersDialogComponent, {
      height: '400px',
      width: '600px',
      data: board,
    });
  }
}
