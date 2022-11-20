import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private toastr: ToastrService){}

  canActivate(): Observable<boolean> {
    // return this.accountService.currentUser$.pipe(
    //   map(user => {
    //     debugger
    //     if(user) return true;
    //     this.toastr.error('You shall not pass!');
    //   })
    // );

    return this.accountService.currentUser$.pipe(
      map(x => {
        if(x) return true;
        else {
          this.toastr.error('You shall not pass!');
          return false;
        }
      })
    )
  }
}
