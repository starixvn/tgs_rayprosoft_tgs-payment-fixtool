import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WrongWalletDetailComponent } from './wrong-wallet-detail/wrong-wallet-detail.component';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list';
import { WithdrawListComponent } from './withdraw-list/withdraw-list.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { FixWithdrawDialogComponent } from './fix-withdraw-dialog/fix-withdraw-dialog.component';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';


@NgModule({
  declarations: [
    WrongWalletDetailComponent,
    WithdrawListComponent,
    TransactionListComponent,
    FixWithdrawDialogComponent
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
