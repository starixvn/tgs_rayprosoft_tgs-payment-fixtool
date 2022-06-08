using Microsoft.AspNetCore.Mvc;
using Tgs.Payment.FixTool.ViewModels;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTool.Controllers;

/// <summary>
/// Wallet
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
	/// <summary>
	/// 列出有問題的錢包
	/// </summary>
	/// <param name="queryService">The query service.</param>
	/// <returns></returns>
	[HttpGet(Name = "ListWrongWallets")]
	public IAsyncEnumerable<WrongWalletViewModel> ListWrongWalletsAsync(
		[FromServices] IQueryService<WrongWalletsQuery, WrongWallet> queryService)
		=> queryService.QueryAsync(new WrongWalletsQuery(), HttpContext.RequestAborted)
			.Select(ww => new WrongWalletViewModel
			{
				Id = ww.Id,
				BrandId = ww.BrandId,
				PlayerCode = ww.PlayerCode,
				UserName = ww.UserName,
				Balance = ww.Balance,
				LockedBalance = ww.LockedBalance,
				InProgressAmount = ww.InProgressAmount
			});

	/// <summary>
	/// 取得有問題錢包資料
	/// </summary>
	/// <param name="getService">The get service.</param>
	/// <param name="walletId">The wallet identifier.</param>
	/// <returns></returns>
	[HttpGet("{walletId}", Name = "GetWrongWallet")]
	public async Task<WrongWalletViewModel?> GetWrongWalletAsync(
		[FromServices] IGetService<GetWrongWalletQuery, WrongWallet?> getService,
		Guid walletId)
	{
		var query = new GetWrongWalletQuery(walletId);

		var wrongWallet = await getService
			.GetAsync(query, HttpContext.RequestAborted)
			.ConfigureAwait(false);

		return wrongWallet is null
			? null
			: new WrongWalletViewModel
			{
				Id = wrongWallet.Id,
				BrandId = wrongWallet.BrandId,
				PlayerCode = wrongWallet.PlayerCode,
				UserName = wrongWallet.UserName,
				Balance = wrongWallet.Balance,
				LockedBalance = wrongWallet.LockedBalance,
				InProgressAmount = wrongWallet.InProgressAmount
			};
	}
}
