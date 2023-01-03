import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ColumnsService } from 'src/app/_services/columns.service';

@Component({
  selector: 'app-edit-column-dialog',
  templateUrl: './edit-column-dialog.component.html',
  styleUrls: ['./edit-column-dialog.component.css']
})
export class EditColumnDialogComponent {
  column: any;

  constructor(private columnService: ColumnsService, private dialog: MatDialog,
    private dialogRef: MatDialogRef<EditColumnDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.column = this.data;
  }

  editColumn(){
    this.dialogRef.close(this.column.name);
  }

  close() {
    this.dialogRef.close();
  }
}
