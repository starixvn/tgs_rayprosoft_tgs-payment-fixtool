using NSubstitute;
using Tgs.CashService.Client;
using Tgs.Payment.Clients;
using Tgs.Payment.FixTools.ApplicationServices;

namespace FixTool.Core.UnitTests;

[TestClass]
public class PaymentTests
{
	[TestMethod]
	public async Task Query_Withdraw_Forms_By_Player()
	{
		var fakePaymentServiceClient = Substitute.For<IPaymentServiceClient>();

		var sut = new PlayerWithdrawFormsQueryService(fakePaymentServiceClient);

		var query = new PlayerWithdrawFormsQuery(
			Guid.NewGuid(),
			"player1");

		_ = fakePaymentServiceClient
			.QueryWithdrawFormsAsync(
				Arg.Is<WithdrawFormQueryConditions>(conditions => conditions.BrandId == query.BrandId
					&& conditions.UserId == query.PlayerCode
					&& conditions.UserType == UserType.Player),
				Arg.Any<Paging>())
			.Returns(new WithdrawForms
			{
				Items = Array.Empty<WithdrawFormInfo>()
			});

		_ = await sut.QueryAsync(query).ToArrayAsync();
	}

	[TestMethod]
	public async Task Fix_Paid_WithdrawForm()
	{
		var fakePaymentServiceClient = Substitute.For<IPaymentServiceClient>();
		var fakeCashServiceClient = Substitute.For<ICashServiceClient>();

		var sut = new FixWithdrawPaidCommandService(
			fakePaymentServiceClient,
			fakeCashServiceClient);

		var command = new FixWithdrawPaidCommand(Guid.NewGuid(), Guid.NewGuid());

		_ = fakePaymentServiceClient.GetWithdrawFormAsync(Arg.Is(command.WithdrawFormId))
			.Returns(new WithdrawFormInfo
			{
				Id = command.WithdrawFormId,
				ActualAmount = 10M,
				Amount = 100M,
				Status = PaymentFlowState.Paid
			});

		await sut.ExecuteAsync(command);

		_ = fakeCashServiceClient.Received(1)
			.CommitWithdrawAsync(
				Arg.Is(command.WalletId),
				Arg.Is(100M),
				Arg.Is(10M),
				Arg.Any<string>(),
				Arg.Is(command.WithdrawFormId.ToString()),
				Arg.Any<Guid?>());
	}

	[TestMethod]
	public async Task Fix_Reject_WithdrawForm()
	{
		var fakePaymentServiceClient = Substitute.For<IPaymentServiceClient>();
		var fakeCashServiceClient = Substitute.For<ICashServiceClient>();

		var sut = new FixWithdrawRejectCommandService(fakePaymentServiceClient, fakeCashServiceClient);

		var command = new FixWithdrawRejectCommand(Guid.NewGuid(), Guid.NewGuid());

		_ = fakePaymentServiceClient.GetWithdrawFormAsync(Arg.Is(command.WithdrawFormId))
			.Returns(new WithdrawFormInfo
			{
				Id = command.WithdrawFormId,
				ActualAmount = 10M,
				Amount = 100M,
				Status = PaymentFlowState.Reject
			});

		await sut.ExecuteAsync(command);

		_ = fakeCashServiceClient.Received(1)
			.AdjustSystemAsync(
				Arg.Is(command.WalletId),
				Arg.Is(0M),
				Arg.Is(-100M),
				Arg.Any<string>(),
				Arg.Is(command.WithdrawFormId.ToString()),
				Arg.Is(Guid.Empty));
	}
}
