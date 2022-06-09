namespace Tgs.Payment.FixTools.ApplicationServices;
public record FixWithdrawRejectCommand(Guid WithdrawFormId, Guid WalletId);
