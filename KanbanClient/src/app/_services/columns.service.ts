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

  putColumnName(name: any, columnId: any){
    return this.http.put(this.baseUrl + 'Columns/' + columnId, {name});
  }

  deleteColumn(id: any){
    return this.http.delete(this.baseUrl + 'Columns/' + id);
  }

  putColumn(currentColumnId: any, currentContainer: any){
    return this.http.put(this.baseUrl + 'Columns/', {obj: {currentColumnId, currentContainer}});
  }
}
