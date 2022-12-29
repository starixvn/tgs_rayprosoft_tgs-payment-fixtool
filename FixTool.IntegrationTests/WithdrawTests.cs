using System.Runtime.InteropServices;
using HttpOverStream.NamedPipe;
using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Tgs.Payment.FixTools;
using Xunit.Abstractions;

namespace FixTool.IntegrationTests;

public class WithdrawTests
{
	private readonly ITestOutputHelper m_TestOutputHelper;

	public WithdrawTests(ITestOutputHelper testOutputHelper)
	{
		m_TestOutputHelper = testOutputHelper;
	}

	[Fact]
	public async Task 取得有問題的錢包列表()
	{
		// Arrange
		var channelName = Guid.NewGuid().ToString();

		var fakeTgsRepository = Substitute.For<ITgsRepository>();

		using var web = TestFixToolApplication.Builder
			.UseTestChannelServer(channelName)
			.ConfigureServices(services => services
				.AddSingleton(fakeTgsRepository))
			.ConfigureLogging(logging => logging.AddXUnit(m_TestOutputHelper))
			.Build()!;

		await web.StartAsync();

		var wrongWallets = new[] {
			new WrongWallet(
				Guid.NewGuid(),
				Guid.NewGuid(),
				"player1",
				"tester",
				1000M,
				0M,
				500M)
		};

		_ = fakeTgsRepository.ListWrongWalletsAsync(Arg.Any<CancellationToken>())
			.Returns(wrongWallets.ToAsyncEnumerable());

		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "true" : "false";

		var nodeModule = $$"""
			const axiosConfigFactory = require('./models/axios-configuration.js').default;
			const FixToolService = require('./services/fix-tool-service.js').default;

			module.exports = async () =>
			{
				let instance = axiosConfigFactory("{{channelName}}", {{isWindows}});

				let svc = new FixToolService(instance);

				let result = await svc.listWrongWallets();

				return result;
			};
		""";

		// Act
		var actual = await StaticNodeJSService.InvokeFromStringAsync<WrongWallet[]>(nodeModule);

		// Assert
		Assert.Equal(wrongWallets, actual);
	}
}
