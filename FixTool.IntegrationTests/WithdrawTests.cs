using System.Runtime.InteropServices;
using ExpectedObjects;
using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Tgs.CashService.Client;
using Tgs.Payment.Clients;
using Tgs.Payment.FixTool.ViewModels;
using Xunit.Abstractions;

namespace FixTool.IntegrationTests;

public class WithdrawTests
{
	private readonly ITestOutputHelper m_TestOutputHelper;

	public WithdrawTests(ITestOutputHelper testOutputHelper)
	{
		m_TestOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
	}

	[Fact]
	public async Task 列出玩家的提款單()
	{
		// Arrange
		var channelName = Guid.NewGuid().ToString();

		var fakePaymentServiceClient = Substitute.For<IPaymentServiceClient>();

		using var web = TestFixToolApplication.Builder
			.UseTestChannelServer(channelName)
			.ConfigureServices(services => services
				.AddSingleton(fakePaymentServiceClient))
			.ConfigureLogging(logging => logging.AddXUnit(m_TestOutputHelper))
			.Build();

		await web.StartAsync();

		var brandId = Guid.NewGuid();
		var playerCode = "player1";

		var withdrawInfos = new[]
		{
			new Tgs.Payment.Clients.WithdrawFormInfo
			{
				Id = Guid.NewGuid(),
				Amount = 1000M,
				BrandId = brandId,
				CreatedOn = DateTime.UtcNow,
				OrderNo = "test123",
				UserId = playerCode,
				UserName = "test1",
				Status = PaymentFlowState.Approved,
				UpdatedOn= DateTime.UtcNow
			},
			new Tgs.Payment.Clients.WithdrawFormInfo
			{
				Id = Guid.NewGuid(),
				Amount = 1000M,
				BrandId = brandId,
				CreatedOn = DateTime.UtcNow,
				OrderNo = "test456",
				UserId = playerCode,
				UserName = "test1",
				Status = PaymentFlowState.Reject,
				UpdatedOn= DateTime.UtcNow
			}
		};

		_ = fakePaymentServiceClient.QueryWithdrawFormsAsync(
			Arg.Is(brandId),
			Arg.Is(playerCode),
			Arg.Is(UserType.Player),
			Arg.Any<CancellationToken>())
			.Returns(withdrawInfos.ToAsyncEnumerable());

		var expected = withdrawInfos.Select(info => new
		{
			info.Id,
			info.Amount,
			BrandId = brandId,
			OrderNo = info.OrderNo!,
			PlayerCode = info.UserId!,
			Status = info.Status!.ToString(),
		});

		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "true" : "false";

		var nodeModule = $$"""
			const axiosConfigFactory = require('./models/axios-configuration.js').default;
			const FixToolService = require('./services/fix-tool-service.js').default;

			module.exports = async (brandId, playerCode) =>
			{
				let instance = axiosConfigFactory("{{channelName}}", {{isWindows}});

				let svc = new FixToolService(instance);

				let result = await svc.listPlayerWithdrawForms(brandId, playerCode);

				return result;
			};
		""";

		// Act
		var actual = await StaticNodeJSService.InvokeFromStringAsync<WithdrawFormViewModel[]>(
			 nodeModule,
			 args: new[] { brandId.ToString(), playerCode });

		// Assert
		expected.ToExpectedObject().ShouldMatch(actual);
	}

	[Fact]
	public async Task 修復已支付提款單()
	{
		// Arrange
		var channelName = Guid.NewGuid().ToString();

		var fakePaymentServiceClient = Substitute.For<IPaymentServiceClient>();
		var fakeCashServiceClient = Substitute.For<ICashServiceClient>();

		using var web = TestFixToolApplication.Builder
			.UseTestChannelServer(channelName)
			.ConfigureServices(services => services
				.AddSingleton(fakePaymentServiceClient)
				.AddSingleton(fakeCashServiceClient))
			.ConfigureLogging(logging => logging.AddXUnit(m_TestOutputHelper))
			.Build();

		await web.StartAsync();

		var withdrawFormId = Guid.NewGuid();
		var walletId = Guid.NewGuid();

		_ = fakePaymentServiceClient.GetWithdrawFormAsync(Arg.Is(withdrawFormId), Arg.Any<CancellationToken>())
			.Returns(new WithdrawFormInfo
			{
				Id = withdrawFormId,
				Status = PaymentFlowState.Paid,
				Amount = 1000M,
				ActualAmount = 900M
			});

		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "true" : "false";

		var nodeModule = $$"""
			const axiosConfigFactory = require('./models/axios-configuration.js').default;
			const FixToolService = require('./services/fix-tool-service.js').default;

			module.exports = async (formId, walletId) =>
			{
				let instance = axiosConfigFactory("{{channelName}}", {{isWindows}});

				let svc = new FixToolService(instance);

				let result = await svc.fixWithdrawPaid(formId, walletId);

				return result;
			};
		""";

		// Act
		await StaticNodeJSService.InvokeFromStringAsync(
			 nodeModule,
			 args: new[] { withdrawFormId.ToString(), walletId.ToString() });

		// Assert
		_ = fakeCashServiceClient.Received(1)
			.CommitWithdrawAsync(
				Arg.Is(walletId),
				Arg.Is(1000M),
				Arg.Is(900M),
				Arg.Is("mark up the account"),
				Arg.Is(withdrawFormId.ToString()),
				Arg.Any<Guid?>(),
				Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task 修復已拒絕提款單()
	{
		// Arrange
		var channelName = Guid.NewGuid().ToString();

		var fakePaymentServiceClient = Substitute.For<IPaymentServiceClient>();
		var fakeCashServiceClient = Substitute.For<ICashServiceClient>();

		using var web = TestFixToolApplication.Builder
			.UseTestChannelServer(channelName)
			.ConfigureServices(services => services
				.AddSingleton(fakePaymentServiceClient)
				.AddSingleton(fakeCashServiceClient))
			.ConfigureLogging(logging => logging.AddXUnit(m_TestOutputHelper))
			.Build();

		await web.StartAsync();

		var withdrawFormId = Guid.NewGuid();
		var walletId = Guid.NewGuid();

		_ = fakePaymentServiceClient.GetWithdrawFormAsync(Arg.Is(withdrawFormId), Arg.Any<CancellationToken>())
			.Returns(new WithdrawFormInfo
			{
				Id = withdrawFormId,
				Status = PaymentFlowState.Reject,
				Amount = 1000M
			});

		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "true" : "false";

		var nodeModule = $$"""
			const axiosConfigFactory = require('./models/axios-configuration.js').default;
			const FixToolService = require('./services/fix-tool-service.js').default;

			module.exports = async (formId, walletId) =>
			{
				let instance = axiosConfigFactory("{{channelName}}", {{isWindows}});

				let svc = new FixToolService(instance);

				let result = await svc.fixWithdrawReject(formId, walletId);

				return result;
			};
		""";

		// Act
		await StaticNodeJSService.InvokeFromStringAsync(
			 nodeModule,
			 args: new[] { withdrawFormId.ToString(), walletId.ToString() });

		// Assert
		_ = fakeCashServiceClient.Received(1)
			.AdjustSystemV2Async(
				Arg.Is(walletId),
				Arg.Is(0M),
				Arg.Is(-1000M),
				Arg.Is("mark up the account"),
				Arg.Is(withdrawFormId.ToString()),
				Arg.Any<Guid>(),
				Arg.Any<CancellationToken>());
	}
}
