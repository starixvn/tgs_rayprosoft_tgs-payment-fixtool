using System.Runtime.CompilerServices;
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

	public async IAsyncEnumerable<WithdrawFormInfo> QueryAsync(
		PlayerWithdrawFormsQuery query,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
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

		var result = await m_PaymentServiceClient
			.QueryWithdrawFormsAsync(queryConditions, paging, cancellationToken)
			.ConfigureAwait(false);

		foreach (var item in result.Items)
			yield return new WithdrawFormInfo(
				item.Id,
				item.OrderNo!,
				item.BrandId,
				item.UserId!,
				item.UserType,
				item.Status,
				item.Amount,
				item.CreatedOn.LocalDateTime,
				item.UpdatedOn.LocalDateTime);
	}
}
