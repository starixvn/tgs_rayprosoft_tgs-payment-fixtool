namespace Tgs.Payment.FixTool.ViewModels;

/// <summary>
/// 問題錢包資料
/// </summary>
public class WrongWalletViewModel
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public Guid Id { get; set; }

	/// <summary>
	/// Gets or sets the brand identifier.
	/// </summary>
	/// <value>
	/// The brand identifier.
	/// </value>
	public Guid BrandId { get; set; }

	/// <summary>
	/// Gets or sets the player code.
	/// </summary>
	/// <value>
	/// The player code.
	/// </value>
	public string PlayerCode { get; set; } = default!;

	/// <summary>
	/// Gets or sets the name of the user.
	/// </summary>
	/// <value>
	/// The name of the user.
	/// </value>
	public string UserName { get; set; } = default!;

	/// <summary>
	/// Gets or sets the balance.
	/// </summary>
	/// <value>
	/// The balance.
	/// </value>
	public decimal Balance { get; set; }

	/// <summary>
	/// Gets or sets the locked balance.
	/// </summary>
	/// <value>
	/// The locked balance.
	/// </value>
	public decimal LockedBalance { get; set; }

	/// <summary>
	/// Gets or sets the in progress amount.
	/// </summary>
	/// <value>
	/// The in progress amount.
	/// </value>
	public decimal? InProgressAmount { get; set; }
}
