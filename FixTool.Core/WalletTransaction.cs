namespace Tgs.Payment.FixTools;
public record WalletTransaction(
	Guid Id,
	TransactionType Type,
	decimal Amount,
	decimal LockAmount,
	decimal Balance,
	decimal LockedBalance,
	string? ExternalTransactionId,
	DateTime RecordedOn);
