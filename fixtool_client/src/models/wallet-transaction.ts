export interface WalletTransaction {
  id: string;
  type: string;
  amount: number;
  lockAmount: number;
  balance: number;
  lockedBalance: number;
  externalTransactionId?: string;
  recordedOn: Date;
}
