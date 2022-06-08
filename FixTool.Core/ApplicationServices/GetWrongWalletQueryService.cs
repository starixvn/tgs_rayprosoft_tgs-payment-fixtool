using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class GetWrongWalletQueryService : IGetService<GetWrongWalletQuery, WrongWallet?>
{
	private readonly ITgsRepository m_TgsRepository;

	public GetWrongWalletQueryService(ITgsRepository tgsRepository)
	{
		m_TgsRepository = tgsRepository ?? throw new ArgumentNullException(nameof(tgsRepository));
	}

	public ValueTask<WrongWallet?> GetAsync(GetWrongWalletQuery query, CancellationToken cancellationToken = default)
		=> m_TgsRepository.GetWrongWalletAsync(query.WalletId, cancellationToken);
}
