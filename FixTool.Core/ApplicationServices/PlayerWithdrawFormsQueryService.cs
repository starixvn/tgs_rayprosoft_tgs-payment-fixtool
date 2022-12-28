using Tgs.Payment.Clients;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class PlayerWithdrawFormsQueryService : IQueryService<PlayerWithdrawFormsQuery, WithdrawFormInfo>
{
	private readonly IPaymentServiceClient m_PaymentServiceClient;

	public PlayerWithdrawFormsQueryService(IPaymentServiceClient paymentServiceClient)
	{
		m_PaymentServiceClient = paymentServiceClient ?? throw new ArgumentNullException(nameof(paymentServiceClient));
	}

	public IAsyncEnumerable<WithdrawFormInfo> QueryAsync(
		PlayerWithdrawFormsQuery query,
		CancellationToken cancellationToken = default)
	{
		var queryConditions = new WithdrawFormQueryConditions
		{
			BrandId = query.BrandId,
			UserId = query.PlayerCode,
			UserType = UserType.Player
		};
		var paging = new Paging
		{
			ShowAll = true,
			SortBy = "UpdatedOn",
			IsDesc = true
		};

		return m_PaymentServiceClient
			.QueryWithdrawFormsAsync(query.BrandId, query.PlayerCode, UserType.Player, cancellationToken)
			.ToArrayAsync(cancellationToken: cancellationToken)
			.AsTask()
			.ToAsyncEnumerable()
			.SelectMany(arr => arr.ToAsyncEnumerable()
				.Select(info => new WithdrawFormInfo(
					info.Id,
					info.OrderNo!,
					info.BrandId,
					info.UserId!,
					info.UserType,
					info.Status,
					info.Amount,
					info.CreatedOn.LocalDateTime,
					info.UpdatedOn.LocalDateTime)));
	}
}
