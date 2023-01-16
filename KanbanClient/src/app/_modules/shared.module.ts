import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    MatDialogModule,
    MatButtonModule,
    NgxSpinnerModule.forRoot({
      type: 'line-spin-fade'
    }),
    BsDatepickerModule.forRoot(),
  ],
  exports: [
    ToastrModule,
    BsDropdownModule,
    MatDialogModule,
    MatButtonModule,
    MatSelectModule,
    DragDropModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    BsDatepickerModule,
  ]
})
export class SharedModule { }
