import { MatTableModule } from '@angular/material/table';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WrongWalletDetailComponent } from './wrong-wallet-detail/wrong-wallet-detail.component';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { WithdrawListComponent } from './withdraw-list/withdraw-list.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { MatButtonModule } from '@angular/material/button';
import { FixPaidDialogComponent } from './fix-paid-dialog/fix-paid-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    WrongWalletDetailComponent,
    WithdrawListComponent,
    TransactionListComponent,
    FixPaidDialogComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatListModule,
    MatTableModule,
    MatButtonModule,
    MatDialogModule
  ],
  exports: [
    WrongWalletDetailComponent,
    WithdrawListComponent,
    TransactionListComponent
  ]
})
export class ComponentsModule { }
