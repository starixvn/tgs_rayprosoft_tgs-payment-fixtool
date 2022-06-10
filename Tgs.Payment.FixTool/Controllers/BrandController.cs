using System.Collections.Immutable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tgs.Payment.FixTools;
using Tgs.Payment.FixTools.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTool.Controllers;
/// <summary>
/// Brand
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{

	/// <summary>
	/// 列出所有的Brand Code對應
	/// </summary>
	/// <param name="queryService">The query service.</param>
	/// <returns></returns>
	[HttpGet(Name = "GetBrandInfoMap")]
	public async ValueTask<IReadOnlyDictionary<string, string>> GetBrandInfoMapAsync(
		[FromServices] IQueryService<AllBrandCodeMapQuery, BrandInfo> queryService)
	{
		var dict = await queryService.QueryAsync(new AllBrandCodeMapQuery())
			.ToDictionaryAsync(
				info => info.Id.ToString(),
				info => info.Code)
			.ConfigureAwait(false);

		return dict.ToImmutableDictionary();
	}
}
