import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CommentResponse } from '../_models/commentResponse';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getComments(jobId: any): Observable<CommentResponse>{
    return this.http.get<CommentResponse>(this.baseUrl + 'Comments/' + jobId);
  }

  postComment(model: any, jobId: any){
    return this.http.post(this.baseUrl + 'Comments/' + jobId, model);
  }

  deleteColumn(id: any){
    return this.http.delete(this.baseUrl + 'Comments/' + id);
  }
}
