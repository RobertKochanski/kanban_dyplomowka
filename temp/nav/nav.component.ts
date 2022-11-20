import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { UserResponse } from '../_models/userResponse';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.model).subscribe(response => {
      this.toastr.info("Logged in")
      this.router.navigateByUrl('/boards');
    }, error => {
      this.toastr.error(error)
    });
  }

  logout(){
    this.toastr.info("Logged out")
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
