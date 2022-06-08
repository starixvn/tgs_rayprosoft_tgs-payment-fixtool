import { WalletTransaction } from './../services/wallet-transaction';
import { WrongWalletDetailComponent } from './../components/wrong-wallet-detail/wrong-wallet-detail.component';
import { WrongWallet } from './../services/wrong-wallet';
import { WithdrawForm } from './../services/withdraw-form';
import { FixToolService } from './../services/fix-tool.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-payment-detail',
  templateUrl: './payment-detail.component.html',
  styleUrls: ['./payment-detail.component.scss']
})
export class PaymentDetailComponent implements OnInit {
  public withdrawForms: WithdrawForm[] = [];
  public withdrawSelectedRowIds = new Set<string>();
  public transactions: WalletTransaction[] = [];
  public transactionSelectedRowIds = new Set<string>();
  public wrongWallet: WrongWallet | null = null;
  public preCalculateAmount = 0;

  private walletId: string;

  constructor(route: ActivatedRoute, private fixTool: FixToolService) {
    this.walletId = route.snapshot.paramMap.get("walletId")!;
  }

  async ngOnInit(): Promise<void> {
    await this.refresh();
  }

  public async refresh(): Promise<void> {
    this.wrongWallet = await lastValueFrom(this.fixTool.getWrongWallet(this.walletId));

    if (this.wrongWallet != null) {
      this.withdrawForms = await lastValueFrom(this.fixTool.listPlayerWithdrawForms(
        this.wrongWallet.brandId,
        this.wrongWallet.playerCode));
      this.transactions = await lastValueFrom(this.fixTool.listTransactions(this.wrongWallet.id));
    }
  }
}
