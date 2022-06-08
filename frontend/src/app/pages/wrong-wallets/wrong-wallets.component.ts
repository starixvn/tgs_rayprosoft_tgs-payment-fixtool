import { WrongWallet } from './../services/wrong-wallet';
import { FixToolService } from './../services/fix-tool.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { last, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-wrong-wallets',
  templateUrl: './wrong-wallets.component.html',
  styleUrls: ['./wrong-wallets.component.scss'],
  providers: [
    FixToolService
  ]
})
export class WrongWalletsComponent implements OnInit {
  public wrongWallets: WrongWallet[] = [];
  public displayColumns = [
    "username",
    "balance",
    "locked-balance",
    "in-progress",
    "commands"
  ];

  constructor(private fixTool: FixToolService, private router: Router) { }

  async ngOnInit(): Promise<void> {
    this.wrongWallets = await lastValueFrom(this.fixTool.listWrongWallets());
  }

  clickDetail(walletId: string): Promise<boolean> {
    return this.router.navigateByUrl(`/payment-detail/${walletId}`);
  }
}
