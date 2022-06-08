namespace Tgs.Payment.FixTool.ViewModels;

/// <summary>
/// WithdrawFormViewModel
/// </summary>
public class WithdrawFormViewModel
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public Guid Id { get; set; }

	/// <summary>
	/// Gets or sets the order no.
	/// </summary>
	/// <value>
	/// The order no.
	/// </value>
	public string OrderNo { get; set; } = default!;

	/// <summary>
	/// Gets or sets the brand identifier.
	/// </summary>
	/// <value>
	/// The brand identifier.
	/// </value>
	public Guid BrandId { get; set; }

	/// <summary>
	/// Gets or sets the user identifier.
	/// </summary>
	/// <value>
	/// The user identifier.
	/// </value>
	public string PlayerCode { get; set; } = default!;

	/// <summary>
	/// Gets or sets the type of the user.
	/// </summary>
	/// <value>
	/// The type of the user.
	/// </value>
	public string Status { get; set; } = default!;

	/// <summary>
	/// Gets or sets the amount.
	/// </summary>
	/// <value>
	/// The amount.
	/// </value>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets the created on.
	/// </summary>
	/// <value>
	/// The created on.
	/// </value>
	public DateTime CreatedOn { get; set; }

	/// <summary>
	/// Gets or sets the updated on.
	/// </summary>
	/// <value>
	/// The updated on.
	/// </value>
	public DateTime UpdatedOn { get; set; }
}
