import { Component, OnInit } from '@angular/core';
import { BoardData } from 'src/app/_models/boardData';
import { BoardsService } from 'src/app/_services/boards.service';

@Component({
  selector: 'app-board-list',
  templateUrl: './board-list.component.html',
  styleUrls: ['./board-list.component.css']
})
export class BoardListComponent implements OnInit {
  boards: BoardData[];

  constructor(private boardService: BoardsService) { }

  ngOnInit(): void {
    this.loadBoards();
  }

  loadBoards(){
    this.boardService.getBoards().subscribe(boards => {
      this.boards = boards.data
    })
  }

}
