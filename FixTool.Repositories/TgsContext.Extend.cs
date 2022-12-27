using Microsoft.EntityFrameworkCore;

namespace Tgs.Payment.FixTools.Repositories.Entities;

public partial class TgsContext
{
#pragma warning disable CA1822 // 將成員標記為靜態
	partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
#pragma warning restore CA1822 // 將成員標記為靜態
	{
		_ = modelBuilder.Entity<WrongWallet>()
			.HasNoKey();

		_ = modelBuilder.Entity<WalletTransaction>()
			.HasNoKey();
	}
}
