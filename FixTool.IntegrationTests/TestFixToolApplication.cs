using System.Runtime.InteropServices;
using HttpOverStream.NamedPipe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace FixTool.IntegrationTests;

internal static class TestFixToolApplication
{
	public static IHostBuilder Builder => new FixToolApplication()
		.Builder
		.UseEnvironment(Environments.Development);

	internal static IHostBuilder UseTestChannelServer(this IHostBuilder hostBuilder, string channelName)
	{
		var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

		return isWindows
			? hostBuilder
				.ConfigureWebHost(webHostBuilder => webHostBuilder
					.UseHttpOverStreamServer(new NamedPipeListener(channelName)))
			: hostBuilder
				.ConfigureWebHost(webHostBuilder => webHostBuilder
					.UseKestrel(options => options.ListenUnixSocket(
						 $"/tmp/{channelName}.sock")));
	}

	private class FixToolApplication : WebApplicationFactory<Program>
	{
		private IHostBuilder? m_HostBuilder;

		public IHostBuilder Builder
		{
			get
			{
				_ = Server;
				return m_HostBuilder!;
			}
		}

		protected override IHost CreateHost(IHostBuilder builder)
		{
			m_HostBuilder = builder;

			return base.CreateHost(builder);
		}
	}
}
