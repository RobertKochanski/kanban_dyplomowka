import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ColumnsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  postColumn(model: any, boardId: any){
    return this.http.post(this.baseUrl + 'Columns/' + boardId, model);
  }

  deleteColumn(id: any){
    return this.http.delete(this.baseUrl + 'Columns/' + id);
  }
}
