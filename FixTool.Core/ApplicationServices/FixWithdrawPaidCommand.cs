namespace Tgs.Payment.FixTools.ApplicationServices;
public record FixWithdrawPaidCommand(Guid WithdrawFormId, Guid WalletId);
