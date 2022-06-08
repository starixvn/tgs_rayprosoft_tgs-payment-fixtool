using Tgs.CashService.Client;
using Tgs.Payment.Clients;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class FixWithdrawPaidCommandService : ICommandService<FixWithdrawPaidCommand>
{
	private readonly IPaymentServiceClient m_PaymentServiceClient;
	private readonly ICashServiceClient m_CashServiceClient;

	public FixWithdrawPaidCommandService(IPaymentServiceClient paymentServiceClient, ICashServiceClient cashServiceClient)
	{
		m_PaymentServiceClient = paymentServiceClient ?? throw new ArgumentNullException(nameof(paymentServiceClient));
		m_CashServiceClient = cashServiceClient ?? throw new ArgumentNullException(nameof(cashServiceClient));
	}

	public async ValueTask ExecuteAsync(FixWithdrawPaidCommand command, CancellationToken cancellationToken = default)
	{
		var form = await m_PaymentServiceClient
			.GetWithdrawFormAsync(command.WithdrawFormId, cancellationToken)
			.ConfigureAwait(false);

		if (form is null || form.Status != PaymentFlowState.Paid)
			return;

		_ = await m_CashServiceClient.CommitWithdrawAsync(
			command.WalletId,
			form.Amount,
			form.ActualAmount ?? form.Amount,
			"mark up the account",
			form.Id.ToString(),
			null,
			cancellationToken).ConfigureAwait(false);
	}
}
