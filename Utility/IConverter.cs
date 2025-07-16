using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innocalc.Utility
{
	interface IConverter
	{
		double Convert(string fromUnit, string toUnit, double value);
	}
}
