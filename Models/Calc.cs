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
		// Константы для масла
		const float oil_c = 2.05f * 10 * 10 * 10;
		const double oil_v = 1.38 * .000001;
		const int oil_rho = 875;
		const float oil_lambda = .109f;

		const int air_pressure = 101; // кПа

		float air_rho, air_c, eps1 = 1.23f, eps2 = .6f, air_lambda; // Поиск по файлу у eps1
		double wall_Prandtl, air_v;

		const float phi = .85f;
		const int lamda_liq = 92;

		// Константы для пучка
		const float c1 = .36f, Cz = 1f, m1 = .5f;


		public Calc(int t_air_out, int t_air_in)
		{
			float din = FileSearcher.Search(t_air_in, "air_density.json");
			float dout = FileSearcher.Search(t_air_out, "air_density.json");
			float vin = FileSearcher.Search(t_air_in, "air_viscosity.json");
			float vout = FileSearcher.Search(t_air_out, "air_viscosity.json");
			float cin = FileSearcher.Search(t_air_in, "air_thermal_capacity.json");
			float cout = FileSearcher.Search(t_air_in, "air_thermal_capacity.json");
			float lin = FileSearcher.Search(t_air_in, "air_thermal_conductivity.json");
			float lout = FileSearcher.Search(t_air_out, "air_thermal_conductivity.json");

			air_rho = (din + dout) / 2;
			air_v = ((vin + vout) / 2) * .00001f;
			air_c = ((cin + cout) / 2) * 1000;
			air_lambda = (lin + lout) / 2;
		}

		public double c_W_air(int V, int t_air_out, int t_air_in) => (V / 3600.0) * air_rho * ((t_air_out + 273) - (t_air_in + 273)) * air_c;
		public double c_V_oil(double W, int t_oil_in, int t_oil_out) => W * 1000 /
			(oil_rho * oil_c * ((t_oil_in + 273) - (t_oil_out + 273)));
		public double c_Oil_Prandtl()
		{
			double p = (oil_v * oil_rho * oil_c) / oil_lambda;
			wall_Prandtl = p; // возможно поменяется
			return p;
		}
		public double c_Oil_Speed(double V, int n12, float d, float s) => 4 * V / (n12 * Math.PI * (d - 2 * s) * (d - 2 * s));
		public double c_Oil_Reynolds(double Vm1, float d, float s) => (Vm1 * (d - 2 * s)) / oil_v;
		public double c_Oil_Nusselt(double oil_Prandtl, double oil_Reynolds) => 0.021 * Math.Pow(oil_Reynolds, 0.8) *
			Math.Pow(oil_Prandtl, 0.43) *
			Math.Pow((oil_Prandtl / wall_Prandtl), 0.25) * eps1;
		public double c_Oil_HeatTransfer(double Nusselt, float d, float s) => (Nusselt * oil_lambda) / (d - 2 * s);
		public double c_Oil_HeatTransfer2(double alpha_o) => alpha_o * eps2;
		public double c_Air_Prandtl() => (air_c * air_v * air_rho) / air_lambda;
		public double c_Live_section(float air_s, float S1, float d, float beta, float u) => air_s *
			(((S1 - d) * (u - beta)) /
			(S1 * u));
		public double c_Rib_Surface(float b, float H, int n11, float d, float u) => 2 * (
			(b * H / n11) - 0.785 * d * d) * (1 / u);
		public double c_Surface_betwRib(float d, float beta, float u) => Math.PI * d * (1 - beta / u);
		public double c_Air_HydroDiameter(float S1, float d, float u, float beta) => 2 * (
			((S1 - d) * (u - beta)) /
			((S1 - d) + (u - beta)));
		public double c_Air_Reynolds(double air_speed, double d_h) => (air_speed * d_h) / air_v;
		public double c_Finnig_out(float S1, float S22, float d, float beta, float u, float s)
		{
			double a = 2 * (S1 * S22 - 0.785 * d * d) / u;
			double b = Math.PI * d * (1 - beta / u);
			return (a + b) /
				(Math.PI * (d - s * 2));
		}
		public double c_Air_Nusselt(double Cs, double beta_out, double Re_a, double n_p) => c1 * Cs * Cz * Math.Pow(beta_out, -0.5) * Math.Pow(Re_a, n_p);
		public double c_Air_Heat_Transfer(double Nu_a, double d_h) => Nu_a * air_lambda / d_h;
		public double c_Finning1(double Hn, float d, float b_n) => 1.28 * (Hn / d) * Math.Sqrt(b_n / Hn - 0.2);
		public double c_Rib_Height(double d, double rho1) => .5 * d * (rho1 - 1) * (1 + .35*Math.Log(rho1));
		public double c_Complex_Char(double alpha_a, float beta) => Math.Sqrt((2 * alpha_a) / (lamda_liq * beta));
		public double c_Rib_Efficiency(double m22, double h1) => Math.Tanh(m22 * h1) / (m22 * h1);
		public double c_HeatTr_to_Finning(double alpha_a,double F_r, double F_br, double E)
		{
			double a = F_r * E * phi / (F_r + F_br);
			double b = 1 - (F_br / (F_r + F_br));
			return alpha_a * (a + b);
		}
		public double c_K_out(double alpha1, float s, double betta, double alpha_mp) => 
			1 / (
			(1 / alpha1) + (s / lamda_liq) + (betta / alpha_mp));
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
