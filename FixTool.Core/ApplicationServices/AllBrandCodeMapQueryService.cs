using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class AllBrandCodeMapQueryService : IQueryService<AllBrandCodeMapQuery, BrandInfo>
{
	private readonly ITgsRepository m_TgsRepository;

	public AllBrandCodeMapQueryService(ITgsRepository tgsRepository)
	{
		m_TgsRepository = tgsRepository ?? throw new ArgumentNullException(nameof(tgsRepository));
	}

	public IAsyncEnumerable<BrandInfo> QueryAsync(AllBrandCodeMapQuery query, CancellationToken cancellationToken = default)
		=> m_TgsRepository.ListBrandInfoAsync(cancellationToken);
}
