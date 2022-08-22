using Microsoft.EntityFrameworkCore;

namespace Tgs.Payment.FixTools.Repositories.Entities;

public partial class TgsContext
{
	partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
	{
		_ = modelBuilder.Entity<WrongWallet>()
			.HasNoKey();

		_ = modelBuilder.Entity<WalletTransaction>()
			.HasNoKey();
	}
}
