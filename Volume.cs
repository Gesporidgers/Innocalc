using Innocalc.Models;
using Innocalc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innocalc
{
	internal class Volume : Utility.Measure
	{
		public override List<Measure> GetMeasure()
		{
			return new List<Measure>()
			{
				new Length
				{
					Name ="m³",
					Multiplier = .001
				},
				new Length
				{
					Name ="l",
					Multiplier = 1000
				},

			};
		}
	}
}