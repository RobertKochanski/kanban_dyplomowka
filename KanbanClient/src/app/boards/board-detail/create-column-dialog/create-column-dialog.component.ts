import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ColumnsService } from 'src/app/_services/columns.service';

@Component({
  selector: 'app-create-column-dialog',
  templateUrl: './create-column-dialog.component.html',
  styleUrls: ['./create-column-dialog.component.css']
})
export class CreateColumnDialogComponent {
  model: any = {};

  constructor(private columnService: ColumnsService, private dialog: MatDialog,
    private dialogRef: MatDialogRef<CreateColumnDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit(): void {
  }

  createColumn(){
    this.dialogRef.close(this.model);
  }

  close() {
    this.dialogRef.close();
  }
}
