import { WalletTransaction } from './../../services/wallet-transaction';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit {
  @Input()
  public transactions: WalletTransaction[] = [];
  @Input()
  public selectedRowIds = new Set<string>();
  public displayColumns = [
    "recorded",
    "type",
    "amount",
    "lock-amount",
    "balance",
    "locked-balance",
    "empty"
  ];

  constructor() { }

  ngOnInit(): void {
  }

  public clickRow(row: WalletTransaction): void {
    if (this.selectedRowIds.has(row.id))
      this.selectedRowIds.delete(row.id);
    else
      this.selectedRowIds.add(row.id);
  }
}
