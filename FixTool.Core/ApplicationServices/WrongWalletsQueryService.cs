using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class WrongWalletsQueryService : IQueryService<WrongWalletsQuery, WrongWallet>
{
	private readonly ITgsRepository m_TgsRepository;

	public WrongWalletsQueryService(ITgsRepository tgsRepository)
	{
		m_TgsRepository = tgsRepository ?? throw new ArgumentNullException(nameof(tgsRepository));
	}

	public IAsyncEnumerable<WrongWallet> QueryAsync(WrongWalletsQuery query, CancellationToken cancellationToken = default)
		=> m_TgsRepository.ListWrongWalletsAsync(cancellationToken);
}
