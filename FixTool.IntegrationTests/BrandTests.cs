using System.Runtime.InteropServices;
using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Tgs.Payment.FixTools;
using Xunit.Abstractions;

namespace FixTool.IntegrationTests;

public class BrandTests
{
	private readonly ITestOutputHelper m_TestOutputHelper;

	public BrandTests(ITestOutputHelper testOutputHelper)
	{
		m_TestOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
	}

	[Fact]
	public async Task 取得BrandCode對應表()
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

		var brandInfos = new[] {
			new BrandInfo(Guid.NewGuid(), "Test1"),
			new BrandInfo(Guid.NewGuid(), "Test2")
		};

		_ = fakeTgsRepository.ListBrandInfoAsync(Arg.Any<CancellationToken>())
			.Returns(brandInfos.ToAsyncEnumerable());

		var expected = brandInfos.ToDictionary(info => info.Id.ToString(), info => info.Code);

		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "true" : "false";

		var nodeModule = $$"""
			const axiosConfigFactory = require('./models/axios-configuration.js').default;
			const FixToolService = require('./services/fix-tool-service.js').default;

			module.exports = async () =>
			{
				let instance = axiosConfigFactory("{{channelName}}", {{isWindows}});

				let svc = new FixToolService(instance);

				let result = await svc.listBrandMaps();

				return result;
			};
		""";

		// Act
		var actual = await StaticNodeJSService.InvokeFromStringAsync<Dictionary<string, string>>(nodeModule);

		// Assert
		Assert.Equal(expected, actual);
	}
}
