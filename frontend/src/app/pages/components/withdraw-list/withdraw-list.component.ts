import { FixToolService } from './../../services/fix-tool.service';
import { FixPaidDialogComponent } from './../fix-paid-dialog/fix-paid-dialog.component';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { WithdrawForm } from '../../services/withdraw-form';
import { MatDialog } from '@angular/material/dialog';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-withdraw-list',
  templateUrl: './withdraw-list.component.html',
  styleUrls: ['./withdraw-list.component.scss']
})
export class WithdrawListComponent implements OnInit {
  @Input()
  public withdrawForms: WithdrawForm[] = [];
  @Input()
  public walletId: string = '';
  @Input()
  public preCalculateAmount: number = 0;
  @Output()
  public preCalculateAmountChange = new EventEmitter<number>();

  public withdrawDisplayColumns = [
    "order-no",
    "status",
    "amount",
    "created",
    "updated",
    "commands"
  ];

  @Output()
  public afterFix = new EventEmitter();

  @Input()
  public selectedRowIds = new Set<string>();

  constructor(private dialog: MatDialog, private fixTool: FixToolService) { }

  ngOnInit(): void {
  }

  public clickFixPaid(form: WithdrawForm): void {
    const dialogRef = this.dialog.open(FixPaidDialogComponent, {
      width: '500px',
      data: form,
    });

    dialogRef.afterClosed().subscribe(async result => {
      await lastValueFrom(this.fixTool.fixWithdrawPaid(result, this.walletId));
      this.afterFix.emit();
    });
  }

  public clickRow(row: WithdrawForm): void {
    if (this.selectedRowIds.has(row.id))
      this.selectedRowIds.delete(row.id);
    else
      this.selectedRowIds.add(row.id);

    this.refreshPreCalculateAmount();
  }

  private refreshPreCalculateAmount() {
    this.preCalculateAmount = this.withdrawForms
      .map(v => {
        if (this.selectedRowIds.has(v.id))
          return v.amount;
        else
          return 0;
      })
      .reduce((p, c) => p + c, 0);

    this.preCalculateAmountChange.emit(this.preCalculateAmount);
  }
}
