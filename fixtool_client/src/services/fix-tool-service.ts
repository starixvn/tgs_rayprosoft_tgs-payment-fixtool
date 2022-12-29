import { AxiosInstance } from 'axios';
import { BrandMap } from '../models/brand-map.js';
import { WalletTransaction } from '../models/wallet-transaction.js';
import { WithdrawForm } from '../models/withdraw-form.js';
import { WrongWallet } from '../models/wrong-wallet.js';

export default class FixToolService {

    constructor(private http: AxiosInstance) {

    }

    listWrongWallets(): Promise<WrongWallet[]> {
        return this.http
            .get<WrongWallet[]>("/api/wallet")
            .then(res => res.data);
    }

    getWrongWallet(walletId: string): Promise<WrongWallet | null> {
        return this.http
            .get<WrongWallet | null>(`/api/wallet/${walletId}`)
            .then(res => res.data);
    }

    listPlayerWithdrawForms(brandId: string, playerCode: string): Promise<WithdrawForm[]> {
        return this.http
            .get<WithdrawForm[]>(`/api/withdraw/forms/${brandId}/${playerCode}`)
            .then(res => res.data);
    }

    listTransactions(walletId: string): Promise<WalletTransaction[]> {
        return this.http
            .get<WalletTransaction[]>(`/api/transaction/${walletId}`)
            .then(res => res.data);
    }

    fixWithdrawPaid(withdrawFormId: string, walletId: string): Promise<void> {
        return this.http
            .post<void>(`/api/withdraw/paid/${withdrawFormId}/${walletId}`, null)
            .then(res => res.data);
    }

    fixWithdrawReject(withdrawFormId: string, walletId: string): Promise<void> {
        return this.http
            .post<void>(`/api/withdraw/reject/${withdrawFormId}/${walletId}`, null)
            .then(res => res.data);
    }

    listBrandMaps(): Promise<BrandMap> {
        return this.http
            .get<BrandMap>('/api/brand')
            .then(res => res.data);
    }
}