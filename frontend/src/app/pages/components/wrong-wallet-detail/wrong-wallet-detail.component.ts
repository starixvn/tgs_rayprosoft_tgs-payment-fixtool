import { WrongWallet } from './../../services/wrong-wallet';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-wrong-wallet-detail',
  templateUrl: './wrong-wallet-detail.component.html',
  styleUrls: ['./wrong-wallet-detail.component.scss']
})
export class WrongWalletDetailComponent implements OnInit {
  @Input()
  public wrongWallet: WrongWallet | null = null;
  @Input()
  public calculateAmount:number = 0;

  constructor() { }

  ngOnInit(): void {
  }
}
