using Innocalc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innocalc.Models
{
    class VolumeConverter : IConverter
    {
		public double Convert(string fromUnit, string toUnit, double value)
		{
			if (fromUnit == toUnit)
				return value;
			if (fromUnit != "м³")
				value /= 1000;
			else
				value *= 1000;
			return value;
		}

	}
}
