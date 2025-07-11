using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innocalc.Utility;

namespace Innocalc.Models
{
	class TimeConverter : IConverter
	{
		public double Convert(string fromUnit, string toUnit, double value) { 
			if (fromUnit != "s.")
				switch (fromUnit)
				{
					case "hr.": value /= 3600; break;
					case "min.": value /= 60; break;
				}
			if (toUnit != fromUnit)
				switch (toUnit)
				{
					case "hr.": value *= 3600; break;
					case "min.": value *= 60; break;
				}
			return value;
		}
	}
}