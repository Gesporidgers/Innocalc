using Innocalc.Models;
using Innocalc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Innocalc
{
	class MainVM : BindHelper
	{
		private int _t_oil_in;
		private int _t_oil_out;
		private int _t_air_in;
		private int _t_air_out;
		private float _oil_press;
		private int _air_v;
		private int _h;
		private int _b;
		private int _z;
		private int _d_out;
		private float _s;
		private float _beta;
		private int _u;
		private int _n11;
		private int _n12;
		private double _w_air;
		private double _oil_v;
		private double _dT;
		private float _s1;
		private float _s2;
		private double _l;
		private int _nn;
		private int _nn_row;
		private double _h_final;
		private double _z_final;
		private double _delta_p;

		private Visibility _resultsVisible = Visibility.Collapsed;

		private TempCalcMethod _selected;
		private string _len;
		private string _time;

		Calc calc;
		Models.LengthConverter conv1 = new Models.LengthConverter();
		Models.TimeConverter conv2 = new Models.TimeConverter();


		private ICommand calc_W;
		private ICommand calc_V;
		private ICommand calc_dT;
		private ICommand calc_Geometry;

		public int T_oil_in
		{
			get => _t_oil_in;
			set
			{
				_t_oil_in = value;
				OnPropertyChanged(nameof(T_oil_in));
			}
		}
		public int T_oil_out
		{
			get => _t_oil_out;
			set
			{
				_t_oil_out = value;
				OnPropertyChanged(nameof(T_oil_out));
			}
		}
		public int T_air_in
		{
			get => _t_air_in;
			set
			{
				_t_air_in = value;
				OnPropertyChanged(nameof(T_air_in));
			}
		}
		public int T_air_out
		{
			get => _t_air_out;
			set
			{
				_t_air_out = value;
				OnPropertyChanged(nameof(T_air_out));
			}
		}

		public float Oil_press
		{
			get => _oil_press;
			set
			{
				_oil_press = value;
				OnPropertyChanged(nameof(Oil_press));
			}
		}
		public int Air_v
		{
			get => _air_v;
			set
			{
				_air_v = value;
				OnPropertyChanged(nameof(Air_v));
			}
		}

		public int H
		{
			get => _h;
			set
			{
				_h = value;
				OnPropertyChanged(nameof(H));
			}
		}
		public int B
		{
			get => _b;
			set
			{
				_b = value;
				OnPropertyChanged(nameof(B));
			}
		}
		public int Z
		{
			get => _z;
			set
			{
				_z = value;
				OnPropertyChanged(nameof(Z));
			}
		}
		public int D_out
		{
			get => _d_out;
			set
			{
				_d_out = value;
				OnPropertyChanged(nameof(D_out));
			}
		}
		public float S
		{
			get => _s;
			set
			{
				_s = value;
				OnPropertyChanged(nameof(S));
			}
		}
		public float Beta
		{
			get => _beta;
			set
			{
				_beta = value;
				OnPropertyChanged(nameof(Beta));
			}
		}
		public int U
		{
			get => _u;
			set
			{
				_u = value;
				OnPropertyChanged(nameof(U));
			}
		}
		public int N11
		{
			get => _n11;
			set
			{
				_n11 = value;
				OnPropertyChanged(nameof(N11));
			}
		}
		public int N12
		{
			get => _n12;
			set
			{
				_n12 = value;
				OnPropertyChanged(nameof(N12));
			}
		}
		public float S1
		{
			get => _s1;
			set
			{
				_s1 = value;
				OnPropertyChanged(nameof(S1));
			}
		}
		public float S2
		{
			get => _s2;
			set
			{
				_s2 = value;
				OnPropertyChanged(nameof(S2));
			}
		}
		public double W_air
		{
			get => _w_air;
			set
			{
				_w_air = value;
				OnPropertyChanged(nameof(W_air));
			}
		}
		public double Oil_v
		{
			get => _oil_v;
			set
			{
				_oil_v = value;
				OnPropertyChanged(nameof(Oil_v));
			}
		}
		public double Dt
		{
			get => _dT;
			set
			{
				_dT = value;
				OnPropertyChanged(nameof(Dt));
			}
		}
		public double L
		{
			get => _l;
			private set
			{
				_l = value;
				OnPropertyChanged(nameof(L));
			}
		}
		public int NN
		{
			get => _nn;
			private set
			{
				_nn = value;
				OnPropertyChanged(nameof(NN));
			}
		}
		public int NN_row
		{
			get => _nn_row;
			set
			{
				_nn_row = value;
				OnPropertyChanged(nameof(NN_row));
			}
		}
		public double H_final
		{
			get => _h_final;
			private set
			{
				_h_final = value;
				OnPropertyChanged(nameof(H_final));
			}
		}
		public double Z_final
		{
			get => _z_final;
			private set
			{
				_z_final = value;
				OnPropertyChanged(nameof(Z_final));
			}
		}
		public double Delta_P
		{
			get => _delta_p;
			private set
			{
				_delta_p = value;
				OnPropertyChanged(nameof(Delta_P));
			}
		}
		public Visibility ResultsVisible
		{
			get => _resultsVisible;
			set
			{
				_resultsVisible = value;
				OnPropertyChanged(nameof(ResultsVisible));
			}
		}

		public List<TempCalcMethod> Methods => TempCalcMethod.GetMethods();
		public string[] Len => ["m.", "cm.", "mm."];
		public string[] Time => ["hr.", "min.", "s."];

		public string LengthMeasure
		{
			get => _len;
			set
			{
				H_final = conv1.Convert(_len, value, H_final);
				Z_final = conv1.Convert(_len, value, Z_final);
				_len = value;
				OnPropertyChanged(nameof(LengthMeasure));
			}
		}
		public string TimeMeasure
		{
			get => _time;
			set
			{
				Oil_v = conv2.Convert(_time, value, Oil_v);
				_time = value;
				OnPropertyChanged(nameof(TimeMeasure));
			}
		}
		public TempCalcMethod Selected
		{
			get => _selected;
			set
			{
				_selected = value;
				OnPropertyChanged(nameof(Selected));
			}
		}


		public ICommand CalcWCommand            // сделать включение кнопки при введённых данных (видимо объём)
		{
			get
			{
				return calc_W ??= new RelayCommand(_ => Air_v != 0, _ =>
				{
					calc = new Calc(T_air_out, T_air_in);
					W_air = Math.Round(calc.c_W_air(Air_v, T_air_out, T_air_in), 2);
				});
			}
		}

		public ICommand CalcVCommand
		{
			get
			{
				return calc_V ??= new RelayCommand(_ => W_air != 0, _ => Oil_v = calc.c_V_oil(W_air, T_oil_in, T_oil_out)); // Какие единицы измерения надо?
			}
		}

		public ICommand CalcDtCommand           // сделать включение кнопки при выбранном методе и введённых температурах (тут вопрос, ведь температура может быть 0)
		{
			get
			{
				return calc_dT ??= new RelayCommand(_ => Selected != null, _ => Dt = Selected.method(T_air_in + 273, T_air_out + 273, T_oil_in + 273, T_oil_out + 273));
			}
		}

		public ICommand CalcGeoCommand              // Возможно выполнять все расчёты в параллельке для быстроты вычислений?
		{
			get
			{
				return calc_Geometry ??= new RelayCommand(_ => Oil_v != 0 && D_out != 0 && N12 != 0 && N11 != 0 && S1 != 0 && S2 != 0 && S != 0 && Beta != 0 && U != 0 && H != 0 && Z != 0 && B != 0 && Dt != 0 && W_air != 0, _ =>
				{
					double Pr_o = calc.c_Oil_Prandtl();
					double Vm1 = calc.c_Oil_Speed(Oil_v, N12, D_out * .001f, S * .001f);
					double Re_o = calc.c_Oil_Reynolds(Vm1, D_out * .001f, S * .001f);   // позже должно быть предупреждение о режиме течения
					double Nuss = calc.c_Oil_Nusselt(Pr_o, Re_o);
					double alpha_o = calc.c_Oil_HeatTransfer(Nuss, D_out * .001f, S * .001f);
					double alpha_o2 = calc.c_Oil_HeatTransfer2(alpha_o);
					double Cs = Math.Pow((S1 - D_out) / (S2 - D_out), 0.1);
					float air_s = H * .001f * B * .001f, b_n = N11 * S1, H_n = S2;
					double live_sec = calc.c_Live_section(air_s, S1 * .001f, D_out * .001f, Beta * .001f, U * .001f);
					double air_speed = Air_v / live_sec / 3600;
					double Pr_a = calc.c_Air_Prandtl();
					double F_r = calc.c_Rib_Surface(b_n, H_n, N11, D_out, U);
					double F_br = calc.c_Surface_betwRib(D_out, Beta, U);
					double d_h = calc.c_Air_HydroDiameter(S1, D_out, U, Beta);
					double Re_a = calc.c_Air_Reynolds(air_speed, d_h * .001);
					double S_in = Math.PI * (D_out - S * 2);
					double Betta = calc.c_Finnig_out(S1, S2, D_out, Beta, U, S);
					double n_p = .6 * Math.Pow(Betta, .07);
					double Nu_a = calc.c_Air_Nusselt(Cs, Betta, Re_a, n_p);
					double air_alpha = calc.c_Air_Heat_Transfer(Nu_a, d_h * .001);
					double rho1 = calc.c_Finning1(H_n, D_out, b_n);
					double h1 = calc.c_Rib_Height(D_out * .001, rho1);
					double m22 = calc.c_Complex_Char(air_alpha, Beta * .001f);
					double E = calc.c_Rib_Efficiency(m22, h1);
					double alpha1_pr = calc.c_HeatTr_to_Finning(air_alpha, F_r * .001, F_br * .001, E);
					double K_out = calc.c_K_out(alpha1_pr, S * .001f, Betta, alpha_o2);
					double Fport = calc.c_F(W_air, K_out, Dt);
					L = Fport / (F_br * .001 + F_r * .001);
					NN = (int)Math.Round(L / (B * .001));   // округлять по правилам математики? потому что при исходных данных вышло 26.817 после округления 27
					NN_row = NN / N11;
					H_final = NN * S1 * .001 / N11;
					Z_final = S2 * .001 * N11;
					Delta_P = calc.c_Hydro_Resistance(Re_o, NN, N12, B * .001f, D_out * .001f, S * .001f, Vm1);
					ResultsVisible = Visibility.Visible;
				});
			}
		}
		public MainVM()
		{
			_len = Len[0];
			_time = Time[2];
		}
	}
}
