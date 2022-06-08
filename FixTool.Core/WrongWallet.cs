namespace Tgs.Payment.FixTools;
public record WrongWallet(
	Guid Id,
	Guid BrandId,
	string PlayerCode,
	string UserName,
	decimal Balance,
	decimal LockedBalance,
	decimal? InProgressAmount);
