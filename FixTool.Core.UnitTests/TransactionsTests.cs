using NSubstitute;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;

namespace FixTool.Core.UnitTests;

[TestClass]
public class TransactionsTests
{
	[TestMethod]
	public async Task List_Transactions_By_WalletId()
	{
		var fakeTgsRepository = Substitute.For<ITgsRepository>();

		var sut = new WalletTransactionsQueryService(fakeTgsRepository);

		var query = new WalletTransactionsQuery(Guid.NewGuid());

		_ = await sut.QueryAsync(query).ToArrayAsync();

		_ = fakeTgsRepository.Received(1)
			.ListTransactionsAsync(Arg.Is(query.WalletId));
	}
}
