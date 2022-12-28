import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class JobsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  postJob(model: any, columnId: any){
    return this.http.post(this.baseUrl + 'Jobs/' + columnId, model);
  }

  putJob(model: any, id: any){
    return this.http.put(this.baseUrl + 'Jobs/' + id, model);
  }

  deleteJob(id: any){
    return this.http.delete(this.baseUrl + 'Jobs/' + id);
  }
}
