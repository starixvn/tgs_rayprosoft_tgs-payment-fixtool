using Microsoft.AspNetCore.Mvc;
using Tgs.Payment.FixTool.ViewModels;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTool.Controllers;

/// <summary>
/// Transaction
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
	/// <summary>
	/// Lists the transactions asynchronous.
	/// </summary>
	/// <param name="queryService">The query service.</param>
	/// <param name="walletId">The wallet identifier.</param>
	/// <returns></returns>
	[HttpGet("{walletId}", Name = "ListTransactions")]
	public IAsyncEnumerable<TransactionViewModel> ListTransactionsAsync(
		[FromServices] IQueryService<WalletTransactionsQuery, WalletTransaction> queryService,
		Guid walletId)
	{
		var query = new WalletTransactionsQuery(walletId);

		return queryService.QueryAsync(query, HttpContext.RequestAborted)
			.Select(trx => new TransactionViewModel
			{
				Id = trx.Id,
				Amount = trx.Amount,
				Balance = trx.Balance,
				ExternalTransactionId = trx.ExternalTransactionId,
				LockAmount = trx.LockAmount,
				LockedBalance = trx.LockedBalance,
				RecordedOn = trx.RecordedOn,
				Type = trx.Type.ToString()
			});
	}
}
