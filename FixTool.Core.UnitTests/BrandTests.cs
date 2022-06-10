using NSubstitute;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;

namespace FixTool.Core.UnitTests;

[TestClass]
public class BrandTests
{
	[TestMethod]
	public async Task Get_All_Brand_CodeMap()
	{
		var fakeTgsRepository = Substitute.For<ITgsRepository>();

		var sut = new AllBrandCodeMapQueryService(fakeTgsRepository);

		var query = new AllBrandCodeMapQuery();

		_ = await sut.QueryAsync(query).ToArrayAsync();

		_ = fakeTgsRepository.Received(1)
			.ListBrandInfoAsync();
	}
}
