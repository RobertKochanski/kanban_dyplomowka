import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BoardData } from 'src/app/_models/boardData';
import { BoardsService } from 'src/app/_services/boards.service';

@Component({
  selector: 'app-board-detail',
  templateUrl: './board-detail.component.html',
  styleUrls: ['./board-detail.component.css']
})
export class BoardDetailComponent implements OnInit {
  board: BoardData;

  constructor(private boardService: BoardsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.boardService.getBoard(this.route.snapshot.paramMap.get('id')).subscribe(board => {
      this.board = board.data;
    })
  }

}
