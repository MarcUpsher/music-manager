import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss'],
  providers: []
})
export class ConfirmDialogComponent implements OnInit {
  constructor(
    public dialog: MatDialog
  ) {
  }

  ngOnInit() {
  }
}
