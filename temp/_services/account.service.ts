import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { UserData } from '../_models/userData';
import { UserResponse } from '../_models/userResponse';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<UserData>(1);
  currentUser$ = this.currentUserSource.asObservable();
  private dupalas = new ReplaySubject<boolean>(1);
  zalogowany$ = this.dupalas.asObservable();

  user = {
    id: null,
    username: null,
    email: null,
    token: null
  }

  constructor(private http: HttpClient) {
    this.czyZalogowany(this.user);
  }

  login(model: any){
    return this.http.post(this.baseUrl + 'Users/login', model).pipe(
      map((response: UserResponse) => {
        const user = response;
        if(user){
          this.setCurrentUser(user.data);
        }
        return user;
      })
    );
  }

  register(model: any){
    return this.http.post(this.baseUrl + 'Users/register', model).pipe(
      map((user: UserResponse) => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user.data);
          this.czyZalogowany(user.data);
        }
      })
    )
  }

  setCurrentUser(user: UserData){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    this.czyZalogowany(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.czyZalogowany(this.user);
  }

  czyZalogowany(xd: UserData){
    this.currentUserSource.next(xd);
  }
}
