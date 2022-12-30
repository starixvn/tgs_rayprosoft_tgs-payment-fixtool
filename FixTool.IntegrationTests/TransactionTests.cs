using Jering.Javascript.NodeJS;
using NSubstitute;
using System.Runtime.InteropServices;
using Tgs.Payment.FixTools;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FixTool.IntegrationTests;

public class TransactionTests
{
	private readonly ITestOutputHelper m_TestOutputHelper;

	public TransactionTests(ITestOutputHelper testOutputHelper)
	{
		m_TestOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
	}
	[Fact]
	public async Task 取得錢包交易記錄()
	{
		// Arrange
		var channelName = Guid.NewGuid().ToString();

		var fakeTgsRepository = Substitute.For<ITgsRepository>();

		using var web = TestFixToolApplication.Builder
			.UseTestChannelServer(channelName)
			.ConfigureServices(services => services
				.AddSingleton(fakeTgsRepository))
			.ConfigureLogging(logging => logging.AddXUnit(m_TestOutputHelper))
			.Build();

		await web.StartAsync();

		var walletId = Guid.NewGuid();

		var transactions = new[] {
			new WalletTransaction(
				Guid.NewGuid(),
				TransactionType.Deposited,
				100M,
				0M,
				100M,
				0M,
				null,
				DateTime.UtcNow),
			new WalletTransaction(
				Guid.NewGuid(),
				TransactionType.GameProviderFundOut,
				-100M,
				0M,
				0M,
				0M,
				null,
				DateTime.UtcNow)
		};

		_ = fakeTgsRepository.ListTransactionsAsync(Arg.Is(walletId), Arg.Any<CancellationToken>())
			.Returns(transactions.ToAsyncEnumerable());

		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "true" : "false";

		var nodeModule = $$"""
			const axiosConfigFactory = require('./models/axios-configuration.js').default;
			const FixToolService = require('./services/fix-tool-service.js').default;

			module.exports = async (id) =>
			{
				let instance = axiosConfigFactory("{{channelName}}", {{isWindows}});

				let svc = new FixToolService(instance);

				let result = await svc.listTransactions(id);

				return result;
			};
		""";

		// Act
		var actual = await StaticNodeJSService.InvokeFromStringAsync<string>(
			nodeModule,
			args: new[] { walletId.ToString() });

		// Assert
		Assert.Equal(
			transactions,
			JsonSerializer.Deserialize<WalletTransaction[]>(
				actual!,
				new JsonSerializerOptions(JsonSerializerDefaults.Web)
				{
					Converters = { new JsonStringEnumConverter() }
				}));
	}
}
