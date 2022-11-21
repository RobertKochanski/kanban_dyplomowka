import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BoardListResponse } from '../_models/boardListResponse';
import { BoardResponse } from '../_models/boardResponse';


@Injectable({
  providedIn: 'root'
})
export class BoardsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBoards(): Observable<BoardListResponse>{
    return this.http.get<BoardListResponse>(this.baseUrl + 'Boards/UserAll');
  }

  getBoard(id: any){
    return this.http.get<BoardResponse>(this.baseUrl + 'Boards/' + id);
  }

  postBoard(model: any){
    return this.http.post(this.baseUrl + 'Boards', model);
  }

  deleteBoard(id: any){
    return this.http.delete(this.baseUrl + 'Boards/' + id);
  }
}
