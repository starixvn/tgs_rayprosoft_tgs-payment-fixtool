namespace Tgs.Payment.FixTools;

public interface ITgsRepository
{
	IAsyncEnumerable<WrongWallet> ListWrongWalletsAsync(CancellationToken cancellationToken = default);

	IAsyncEnumerable<WalletTransaction> ListTransactionsAsync(Guid walletId, CancellationToken cancellationToken = default);

	ValueTask<WrongWallet?> GetWrongWalletAsync(Guid walletId, CancellationToken cancellationToken = default);
}
