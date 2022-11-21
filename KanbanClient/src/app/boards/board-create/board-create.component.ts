import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BoardData } from 'src/app/_models/boardData';
import { BoardsService } from 'src/app/_services/boards.service';
import { BoardListComponent } from '../board-list/board-list.component';

@Component({
  selector: 'app-board-create',
  templateUrl: './board-create.component.html',
  styleUrls: ['./board-create.component.css']
})
export class BoardCreateComponent implements OnInit {
  @Output() cancelCreateBoard = new EventEmitter();
  model: any = {};
  validationErrors: string[] = [];

  constructor(private boardSevice: BoardsService, private toastr: ToastrService, private boardListComponent: BoardListComponent) { }

  ngOnInit(): void {
  }

  createBoard(){
    this.boardSevice.postBoard(this.model).subscribe(response => {
      this.cancel();
      this.boardListComponent.loadBoards();
    }, error => {
      this.validationErrors = error;
    })
  }

  cancel(){
    this.cancelCreateBoard.emit(false);
  }
}
