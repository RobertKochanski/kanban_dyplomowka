import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  validationErrors: string[] = [];
  registered: boolean = false;

  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }
  ngOnDestroy(): void {
    this.registered = false;
  }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe(response => {
      this.registered = true;
      // this.cancel();
      // this.router.navigateByUrl("/boards");
    }, error => {
      this.validationErrors = error;
    })
  }

  confirmEmail(){
    this.accountService.sendConfirm(this.model.username).subscribe(response => {
      // this.cancel();
      // this.router.navigateByUrl("/boards");
    }, error => {
      this.validationErrors = error;
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
