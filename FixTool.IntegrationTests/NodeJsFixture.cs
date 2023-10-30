using Jering.Javascript.NodeJS;
using PhoenixToolkits.Xunit.AssemblyFixture;

namespace FixTool.IntegrationTests;

[AssemblyFixture]
internal class NodeJsFixture
{
	public NodeJsFixture()
	{
		StaticNodeJSService.Configure<NodeJSProcessOptions>(
			nodeOptions => nodeOptions.ProjectPath =
				Path.GetFullPath(Path.Combine(
					Environment.CurrentDirectory,
					"../../../..",
					"fixtool_client")));
		StaticNodeJSService.Configure<OutOfProcessNodeJSServiceOptions>(
			processOptions => processOptions.Concurrency = Concurrency.MultiProcess);
	}
}
