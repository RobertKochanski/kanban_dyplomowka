import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BoardData } from '../_models/boardData';
import { BoardResponse } from '../_models/boardResponse';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class BoardsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // getBoards(){
  //   return this.http.get<BoardResponse[]>(this.baseUrl + 'Boards/UserAll', httpOptions);
  // }

  // getBoard(id: any){
  //   return this.http.get<BoardResponse[]>(this.baseUrl + 'Boards/' + id, httpOptions);
  // }
}
