using Tgs.ServiceUtility;

namespace Tgs.Payment.FixTools.ApplicationServices;

internal class FixWithdrawInitCommandService : ICommandService<FixWithdrawInitCommand>
{
	public ValueTask ExecuteAsync(FixWithdrawInitCommand command, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
