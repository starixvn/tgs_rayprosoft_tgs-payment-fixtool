using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class WalletTransactionsQueryService : IQueryService<WalletTransactionsQuery, WalletTransaction>
{
	private readonly ITgsRepository m_TgsRepository;

	public WalletTransactionsQueryService(ITgsRepository tgsRepository)
	{
		m_TgsRepository = tgsRepository ?? throw new ArgumentNullException(nameof(tgsRepository));
	}

	public IAsyncEnumerable<WalletTransaction> QueryAsync(WalletTransactionsQuery query, CancellationToken cancellationToken = default)
		=> m_TgsRepository.ListTransactionsAsync(query.WalletId, cancellationToken);
}
