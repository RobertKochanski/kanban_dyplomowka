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

  constructor(private http: HttpClient) {
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
          this.setCurrentUser(user.data);
        }
      })
    )
  }

  setCurrentUser(user: UserData){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  currentUser(){
    let currentUser: UserData;

    if(localStorage.getItem('user') == null){
      currentUser = {
        id: null,
        username: null,
        email: null,
        token: null 
      }
    }
    else{
      currentUser = JSON.parse(localStorage.getItem('user'));
    }

    return currentUser;
  }
}
