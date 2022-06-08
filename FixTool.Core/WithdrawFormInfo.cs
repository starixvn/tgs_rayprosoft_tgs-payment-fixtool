using Tgs.Payment.Clients;

namespace Tgs.Payment.FixTools;
public record WithdrawFormInfo(
	Guid Id,
	string OrderNo,
	Guid BrandId,
	string UserId,
	UserType UserType,
	PaymentFlowState Status,
	decimal Amount,
	DateTime CreatedOn,
	DateTime UpdatedOn);
