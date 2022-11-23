import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router){}

  canActivate(): boolean {
    let dupa = this.accountService.currentUser();

    if(dupa.token != null){
      return true;
    }

    this.toastr.error("You shall not pass");
    this.router.navigateByUrl('/');
    return false;
  }
}
