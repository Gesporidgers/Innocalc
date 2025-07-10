using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innocalc.Utility
{
    class LengthConverter
    {
        public static double Convert(string fromUnit, string toUnit, double value)
        {
            if (fromUnit != "m.")
                switch (fromUnit)
                {
                    case "mm.": value *= .001; break;
                    case "cm.": value *= .01; break;
                }
            if (fromUnit != toUnit)
                switch (toUnit)
                {
                    case "mm.": value *= 1000; break;
                    case "cm.": value *= 100; break;
                }
            else return value;
                fromUnit = toUnit;
            return value;
        }
    }
}
