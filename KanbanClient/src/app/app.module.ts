import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { BoardListComponent } from './boards/board-list/board-list.component';
import { BoardDetailComponent } from './boards/board-detail/board-detail.component';
import { SharedModule } from './_modules/shared.module';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { BoardCreateComponent } from './boards/board-create/board-create.component';
import { InvitationsComponent } from './invitations/invitations.component';
import { InviteUserDialogComponent } from './boards/board-detail/invite-user-dialog/invite-user-dialog.component';
import { CreateColumnDialogComponent } from './boards/board-detail/create-column-dialog/create-column-dialog.component';
import { CreateJobDialogComponent } from './boards/board-detail/create-job-dialog/create-job-dialog.component';
import { MatSelectModule } from '@angular/material/select';
import { EditJobDialogComponent } from './boards/board-detail/edit-job-dialog/edit-job-dialog.component';
import { JobDetailsDialogComponent } from './boards/board-detail/job-details-dialog/job-details-dialog.component';
import { AssignedMembersDialogComponent } from './boards/board-detail/assigned-members-dialog/assigned-members-dialog.component';
import { MembersDialogComponent } from './boards/board-list/members-dialog/members-dialog.component';
import { EditColumnDialogComponent } from './boards/board-detail/edit-column-dialog/edit-column-dialog.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    BoardListComponent,
    BoardDetailComponent,
    NotFoundComponent,
    ServerErrorComponent,
    BoardCreateComponent,
    InvitationsComponent,
    InviteUserDialogComponent,
    CreateColumnDialogComponent,
    CreateJobDialogComponent,
    EditJobDialogComponent,
    JobDetailsDialogComponent,
    AssignedMembersDialogComponent,
    MembersDialogComponent,
    EditColumnDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
