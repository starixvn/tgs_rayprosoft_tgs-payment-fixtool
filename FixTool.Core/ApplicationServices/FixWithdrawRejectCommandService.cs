using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tgs.CashService.Client;
using Tgs.Payment.Clients;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;
internal class FixWithdrawRejectCommandService : ICommandService<FixWithdrawRejectCommand>
{
	private readonly IPaymentServiceClient m_PaymentServiceClient;
	private readonly ICashServiceClient m_CashServiceClient;

	public FixWithdrawRejectCommandService(IPaymentServiceClient paymentServiceClient, ICashServiceClient cashServiceClient)
	{
		m_PaymentServiceClient = paymentServiceClient ?? throw new ArgumentNullException(nameof(paymentServiceClient));
		m_CashServiceClient = cashServiceClient ?? throw new ArgumentNullException(nameof(cashServiceClient));
	}
	public async ValueTask ExecuteAsync(FixWithdrawRejectCommand command, CancellationToken cancellationToken = default)
	{
		var form = await m_PaymentServiceClient
			.GetWithdrawFormAsync(command.WithdrawFormId, cancellationToken)
			.ConfigureAwait(false);

		if (form is null || form.Status != PaymentFlowState.Reject)
			return;

		_ = await m_CashServiceClient.AdjustSystemAsync(
			command.WalletId,
			0M,
			-form.Amount,
			"mark up the account",
			form.Id.ToString(),
			Guid.Empty,
			cancellationToken).ConfigureAwait(false);
	}
}
