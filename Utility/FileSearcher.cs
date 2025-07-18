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
		}
	}
}
