using Innocalc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innocalc.Models
{
	public class Length : Measure
	{
		public override List<Measure> GetMeasure()
		{
			return new List<Measure>()
			{
				new Length
				{
					Name ="mm.",
					Multiplier = 1000
				},
				new Length
				{
					Name ="m.",
					Multiplier = 0.001
				},

			};
		}
	}
}