using Innocalc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innocalc.Models
{
	class Calc
	{
		const float oil_c = 2.05f * 10 * 10 * 10;
		const double oil_v = 1.38f * .000001f;
		const int oil_rho = 875;
		const float oil_lambda = .109f;
		const int air_pressure = 101;
		float air_rho, air_c, air_v, eps1 = 1.23f;
		double wall_Prandtl;

		public Calc(int t_air_out, int t_air_in)
		{
			float din = FileSearcher.Search(t_air_in, "air_density.json");
			float dout = FileSearcher.Search(t_air_out, "air_density.json");
			float vin = FileSearcher.Search(t_air_in, "air_viscosity.json");
			float vout = FileSearcher.Search(t_air_out, "air_viscosity.json");
			float cin = FileSearcher.Search(t_air_in, "air_thermal_capacity.json");
			float cout = FileSearcher.Search(t_air_in, "air_thermal_capacity.json");

			air_rho = (din + dout) / 2;
			air_v = ((vin + vout) / 2) * .00001f;
			air_c = ((cin + cout) / 2) * 1000;
		}

		public double c_W_air(int V, int t_air_out, int t_air_in) => (V / 3600.0) * air_rho * ((t_air_out + 273) - (t_air_in + 273)) * air_c;
		public double c_V_oil(double W, int t_oil_in, int t_oil_out) => W * 1000 / (oil_rho * oil_c * ((t_oil_in + 273) - (t_oil_out + 273)));
		public double c_Oil_Prandtl()
		{
			double p = (oil_v * oil_rho * oil_c) / oil_lambda;
			wall_Prandtl = p; // возможно поменяется
			return p;
		}
		public double c_Oil_Speed(double V, int n12, int d, float s) => 4 * V / (n12 * Math.PI * (d - 2 * s) * (d - 2 * s));
		public double c_Oil_Reynolds(double Vm1, int d, float s) => (Vm1 * (d - 2 * s)) / oil_v;
		public double c_Oil_Nusselt(double oil_Prandtl, double oil_Reynolds) => 0.021 * Math.Pow(oil_Reynolds, 0.8) * Math.Pow(oil_Prandtl, 0.43) * Math.Pow((oil_Prandtl / wall_Prandtl), 0.25) * eps1;
	}

	class TempCalcMethod
	{
		public string Name { get; set; }
		public delegate double CalculateTemp(int t_air_in, int t_air_out, int t_oil_in, int t_oil_out);
		public CalculateTemp method { get; set; }

		public static List<TempCalcMethod> GetMethods()
		{
			return new List<TempCalcMethod>()
			{
				new TempCalcMethod
				{
					Name = "Прямоток",
					method = StraightFlow
				},
				new TempCalcMethod
				{
					Name = "Противоток",
					method = ReverseFlow
				},
				new TempCalcMethod
				{
					Name = "Перекрёстный ток",
					method = CrossFlow
				}
			};
		}
		private static double StraightFlow(int t_air_in, int t_air_out, int t_oil_in, int t_oil_out) =>
			((t_oil_in - t_air_out) - (t_oil_out - t_air_in)) /
			Math.Log((double)(t_oil_in - t_air_out) / (t_oil_out - t_air_in));
		private static double ReverseFlow(int t_air_in, int t_air_out, int t_oil_in, int t_oil_out) =>
			((t_oil_in - t_air_in) - (t_oil_out - t_air_out)) /
			Math.Log((double)(t_oil_in - t_air_in) / (t_oil_out - t_air_out));
		private static double CrossFlow(int t_air_in, int t_air_out, int t_oil_in, int t_oil_out) =>
			(StraightFlow(t_air_in, t_air_out, t_oil_in, t_oil_out) + ReverseFlow(t_air_in, t_air_out, t_oil_in, t_oil_out)) / 2;
	}
}
