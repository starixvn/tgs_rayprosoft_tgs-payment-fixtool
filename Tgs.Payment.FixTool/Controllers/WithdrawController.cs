using Microsoft.AspNetCore.Mvc;
using Tgs.Payment.FixTool.ViewModels;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTool.Controllers;

/// <summary>
/// Withdraw
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class WithdrawController : ControllerBase
{
	/// <summary>
	/// 依玩家ID查詢提款表單
	/// </summary>
	/// <param name="queryService">The query service.</param>
	/// <param name="brandId">The brand identifier.</param>
	/// <param name="playerCode">The player code.</param>
	/// <returns></returns>
	[HttpGet("forms/{brandId}/{playerCode}", Name = "QueryPlayerWithdrawForms")]
	public IAsyncEnumerable<WithdrawFormViewModel> QueryPlayerWithdrawFormsAsync(
		[FromServices] IQueryService<PlayerWithdrawFormsQuery, WithdrawFormInfo> queryService,
		Guid brandId,
		string playerCode)
	{
		var query = new PlayerWithdrawFormsQuery(
			brandId,
			playerCode);

		return queryService.QueryAsync(query, HttpContext.RequestAborted)
			.Select(info => new WithdrawFormViewModel
			{
				Id = info.Id,
				OrderNo = info.OrderNo,
				PlayerCode = info.UserId,
				Status = info.Status.ToString(),
				Amount = info.Amount,
				BrandId = info.BrandId,
				CreatedOn = info.CreatedOn,
				UpdatedOn = info.UpdatedOn
			});
	}

	/// <summary>
	/// 修復已支付帳變
	/// </summary>
	/// <param name="fixService">The fix service.</param>
	/// <param name="formId">The form identifier.</param>
	/// <param name="walletId">The wallet identifier.</param>
	/// <returns></returns>
	[HttpPost("paid/{formId}/{walletId}", Name = "FixPaid")]
	public ValueTask FixPaidAsync(
		[FromServices] ICommandService<FixWithdrawPaidCommand> fixService,
		Guid formId,
		Guid walletId)
	{
		var command = new FixWithdrawPaidCommand(formId, walletId);

		return fixService.ExecuteAsync(command);
	}

	/// <summary>
	/// 修復已拒絕帳變
	/// </summary>
	/// <param name="fixService">The fix service.</param>
	/// <param name="formId">The form identifier.</param>
	/// <param name="walletId">The wallet identifier.</param>
	/// <returns></returns>
	[HttpPost("reject/{formId}/{walletId}", Name = "FixReject")]
	public ValueTask FixRejectAsync(
		[FromServices] ICommandService<FixWithdrawRejectCommand> fixService,
		Guid formId,
		Guid walletId)
	{
		var command = new FixWithdrawRejectCommand(formId, walletId);

		return fixService.ExecuteAsync(command);
	}

	/// <summary>
	/// 修復申請失敗帳變
	/// </summary>
	/// <param name="fixService">The fix service.</param>
	/// <param name="formId">The form identifier.</param>
	/// <param name="walletId">The wallet identifier.</param>
	/// <returns></returns>
	[HttpPost("init/{formId}/{walletId}", Name = "FixInit")]
	public ValueTask FixInitAsync(
		[FromServices] ICommandService<FixWithdrawInitCommand> fixService,
		Guid formId,
		Guid walletId)
	{
		var command = new FixWithdrawInitCommand(formId, walletId);

		return fixService.ExecuteAsync(command, HttpContext.RequestAborted);
	}
}
