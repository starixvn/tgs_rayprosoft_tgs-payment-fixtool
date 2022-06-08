import { WithdrawForm } from './../../services/withdraw-form';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-fix-paid-dialog',
  templateUrl: './fix-paid-dialog.component.html',
  styleUrls: ['./fix-paid-dialog.component.scss']
})
export class FixPaidDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<FixPaidDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WithdrawForm,
  ) { }

  ngOnInit(): void {
  }

  public clickNo() {
    this.dialogRef.close();
  }
}
