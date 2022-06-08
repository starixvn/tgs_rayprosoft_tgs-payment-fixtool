using Microsoft.EntityFrameworkCore;
using Tgs.Payment.FixTools.Repositories.Entities;

namespace Tgs.Payment.FixTools.Repositories;

internal class TgsRepository : ITgsRepository
{
	private readonly TgsContext m_TgsContext;

	public TgsRepository(TgsContext tgsContext)
	{
		m_TgsContext = tgsContext ?? throw new ArgumentNullException(nameof(tgsContext));
	}

	public IAsyncEnumerable<WrongWallet> ListWrongWalletsAsync(CancellationToken cancellationToken = default)
	{
		return m_TgsContext.Set<WrongWallet>()
			.FromSqlInterpolated($@"
				SELECT
					W.id AS ""Id"",
					W.brand_id AS ""BrandId"",
					W.player_code AS ""PlayerCode"",
					W.username AS ""UserName"",
					W.balance AS ""Balance"",
					W.locked_balance AS ""LockedBalance"",
					WF.sum_amount AS ""InProgressAmount""
				FROM
					(
						SELECT
							w1.id,
							p1.brand_id,
							w1.player_code,
							p1.username,
							w1.balance,
							w1.locked_balance
						FROM
							dbo.wallets AS w1
							JOIN
							dbo.players AS p1 ON w1.player_code = p1.player_code
						WHERE
							locked_balance > 0
					) AS W
					LEFT JOIN
					(
						SELECT
							user_id,
							Sum(amount) AS ""sum_amount""
						FROM
							dbo.withdraw_forms
						WHERE
							status IN(1000, 8000)
							AND
							user_type = 1
						GROUP BY
							user_id
					) AS WF ON W.player_code = WF.user_id
				WHERE
					W.locked_balance != WF.sum_amount
					OR
					WF.user_id IS NULL")
			.ToArray()
			.ToAsyncEnumerable();
	}

	public async ValueTask<WrongWallet?> GetWrongWalletAsync(Guid walletId, CancellationToken cancellationToken = default)
	{
		return await m_TgsContext.Set<WrongWallet>()
			.FromSqlInterpolated($@"
				SELECT
					W.id AS ""Id"",
					W.brand_id AS ""BrandId"",
					W.player_code AS ""PlayerCode"",
					W.username AS ""UserName"",
					W.balance AS ""Balance"",
					W.locked_balance AS ""LockedBalance"",
					WF.sum_amount AS ""InProgressAmount""
				FROM
					(
						SELECT
							w1.id,
							p1.brand_id,
							w1.player_code,
							p1.username,
							w1.balance,
							w1.locked_balance
						FROM
							dbo.wallets AS w1
							JOIN
							dbo.players AS p1 ON w1.player_code = p1.player_code
						WHERE
							w1.id = {walletId}
					) AS W
					LEFT JOIN
					(
						SELECT
							user_id,
							Sum(amount) AS ""sum_amount""
						FROM
							dbo.withdraw_forms
						WHERE
							status IN(1000, 8000)
							AND
							user_type = 1
						GROUP BY
							user_id
					) AS WF ON W.player_code = WF.user_id
				WHERE
					W.locked_balance != WF.sum_amount
					OR
					WF.user_id IS NULL")
			.FirstOrDefaultAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public IAsyncEnumerable<WalletTransaction> ListTransactionsAsync(Guid walletId, CancellationToken cancellationToken = default)
	{
		return m_TgsContext.Transactions
			.Where(trx => trx.WalletId == walletId)
			.OrderByDescending(trx => trx.RecordedOn)
			.Select(trx => new WalletTransaction(
				trx.Id,
				(TransactionType)trx.Type,
				trx.Amount,
				trx.LockAmount,
				trx.Balance,
				trx.LockedBalance,
				trx.ExternalTransactionId,
				trx.RecordedOn))
			.ToArray()
			.ToAsyncEnumerable();
	}
}
