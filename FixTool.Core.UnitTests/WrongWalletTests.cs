using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;

namespace FixTool.Core.UnitTests;

[TestClass]
public class WrongWalletTests
{
	[TestMethod]
	public async Task Query_All_Wrong_Wallets()
	{
		var fakeTgsRepository = Substitute.For<ITgsRepository>();

		var sut = new WrongWalletsQueryService(fakeTgsRepository);

		var query = new WrongWalletsQuery();

		_ = await sut.QueryAsync(query).ToArrayAsync();

		_ = fakeTgsRepository.Received(1)
			.ListWrongWalletsAsync();
	}

	[TestMethod]
	public async Task Get_Wrong_Wallet_By_WalletId()
	{
		var fakeTgsRepository = Substitute.For<ITgsRepository>();

		var sut = new GetWrongWalletQueryService(
			fakeTgsRepository);

		var query = new GetWrongWalletQuery(Guid.NewGuid());

		_ = await sut.GetAsync(query);

		_ = fakeTgsRepository.Received(1)
			.GetWrongWalletAsync(Arg.Is(query.WalletId));
	}
}
