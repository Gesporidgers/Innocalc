using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innocalc.Utility
{
	public abstract class Measure
	{
		public string Name
		{
			get;
			set;
		}

		public double Multiplier
		{
			get ;
			set;
		}

		public abstract List<Measure> GetMeasure();
    }
}