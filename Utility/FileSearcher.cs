using System.IO;
using System.Text.Json;
using MathNet.Numerics.Interpolation;

namespace Innocalc.Utility
{
	class FileSearcher
	{
		public static float Search(int val, string file)
		{
			var Dict = JsonSerializer.Deserialize<Dictionary<double, double>>(File.ReadAllText("Data\\" + file));
			CubicSpline cubicSpline = CubicSpline.InterpolateAkima(Dict.Keys.AsEnumerable(), Dict.Values.AsEnumerable());
			float res = (float)cubicSpline.Interpolate(val);
			return res;
			var keys = Dict.Keys.ToList();
			keys.Sort();
			/*if (val < keys[0])
				return Dict[keys[0]];
			else if (val > keys[^1])
				return Dict[keys[^1]];
			else if (Dict.ContainsKey(val))
				return Dict[val];
			else
			{
				int lowerKey = keys.Where(k => k < val).Max();
				int upperKey = keys.Where(k => k > val).Min();

				float lowerValue = Dict[lowerKey];
				float upperValue = Dict[upperKey];

				float interpolatedValue = lowerValue + (upperValue - lowerValue) * (val - lowerKey) / (upperKey - lowerKey);
				return interpolatedValue;
			}*/
		}
	}
}
