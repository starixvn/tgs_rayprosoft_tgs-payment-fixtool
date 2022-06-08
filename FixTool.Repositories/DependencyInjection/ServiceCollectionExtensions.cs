using Microsoft.EntityFrameworkCore;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.Repositories;
using Tgs.Payment.FixTools.Repositories.Entities;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPaymentFixToolRepositories(
		this IServiceCollection services,
		Action<IServiceProvider, DbContextOptionsBuilder> dbContextConfigAction)
		=> services
			.AddTransient<ITgsRepository, TgsRepository>()
			.AddDbContext<TgsContext>(dbContextConfigAction);
}
