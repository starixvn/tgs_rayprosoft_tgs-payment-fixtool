namespace Tgs.Payment.FixTool.ViewModels;

/// <summary>
/// Transaction Log
/// </summary>
public class TransactionViewModel
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public Guid Id { get; set; }

	/// <summary>
	/// Gets or sets the type.
	/// </summary>
	/// <value>
	/// The type.
	/// </value>
	public string Type { get; set; } = default!;

	/// <summary>
	/// Gets or sets the amount.
	/// </summary>
	/// <value>
	/// The amount.
	/// </value>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets the lock amount.
	/// </summary>
	/// <value>
	/// The lock amount.
	/// </value>
	public decimal LockAmount { get; set; }

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
	/// Gets or sets the external transaction identifier.
	/// </summary>
	/// <value>
	/// The external transaction identifier.
	/// </value>
	public string? ExternalTransactionId { get; set; }

	/// <summary>
	/// Gets or sets the recorded on.
	/// </summary>
	/// <value>
	/// The recorded on.
	/// </value>
	public DateTime RecordedOn { get; set; }
}
