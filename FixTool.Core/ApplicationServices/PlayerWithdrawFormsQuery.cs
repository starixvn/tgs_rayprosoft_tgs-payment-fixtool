using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgs.Payment.FixTools.ApplicationServices;
public record PlayerWithdrawFormsQuery(
	Guid BrandId,
	string PlayerCode);
