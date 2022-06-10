import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WrongWallet } from './wrong-wallet';
import { HttpClient } from '@angular/common/http';
import { WithdrawForm } from './withdraw-form';
import { WalletTransaction } from './wallet-transaction';

@Injectable({
  providedIn: 'root'
})
export class FixToolService {

  constructor(private http: HttpClient) { }

  listWrongWallets(): Observable<WrongWallet[]> {
    return this.http.get<WrongWallet[]>("/api/wallet");
  }

  getWrongWallet(walletId: string): Observable<WrongWallet | null> {
    return this.http.get<WrongWallet | null>(`/api/wallet/${walletId}`);
  }

  listPlayerWithdrawForms(brandId: string, playerCode: string): Observable<WithdrawForm[]> {
    return this.http.get<WithdrawForm[]>(`/api/withdraw/forms/${brandId}/${playerCode}`);
  }

  listTransactions(walletId: string): Observable<WalletTransaction[]> {
    return this.http.get<WalletTransaction[]>(`/api/transaction/${walletId}`);
  }

  fixWithdrawPaid(withdrawFormId: string, walletId: string): Observable<void> {
    return this.http.post<void>(`/api/withdraw/paid/${withdrawFormId}/${walletId}`, null);
  }

  fixWithdrawReject(withdrawFormId: string, walletId: string): Observable<void> {
    return this.http.post<void>(`/api/withdraw/reject/${withdrawFormId}/${walletId}`, null);
  }
}
