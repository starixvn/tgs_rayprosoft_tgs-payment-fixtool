import { WithdrawForm } from '../../services/withdraw-form';
import { Component, OnInit, Inject } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-fix-withdraw-dialog',
  templateUrl: './fix-withdraw-dialog.component.html',
  styleUrls: ['./fix-withdraw-dialog.component.scss']
})
export class FixWithdrawDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<FixWithdrawDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WithdrawForm,
  ) { }

  ngOnInit(): void {
  }

  public clickNo() {
    this.dialogRef.close();
  }
}
