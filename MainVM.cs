using Innocalc.Models;
using Innocalc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private TempCalcMethod _selected;

		Calc calc;


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

		public List<TempCalcMethod> Methods => TempCalcMethod.GetMethods();

		public TempCalcMethod Selected
		{
			get => _selected;
			set
			{
				_selected = value;
				OnPropertyChanged(nameof(Selected));
			}
		}

		public ICommand CalcWCommand
		{
			get
			{
				return calc_W ??= new RelayCommand(_ => true, _ =>
				{
					calc = new Calc(T_air_out, T_air_in);
					W_air = Math.Round(calc.c_W_air(Air_v, T_air_out, T_air_in) / 1000, 2);
				});
			}
		}

		public ICommand CalcVCommand
		{
			get
			{
				return calc_V ??= new RelayCommand(_ => true, _ => Oil_v = calc.c_V_oil(W_air, T_oil_in, T_oil_out)); // Какие единицы измерения надо?
			}
		}

		public ICommand CalcDtCommand
		{
			get
			{
				return calc_dT ??= new RelayCommand(_ => true, _ => Dt = Selected.method(T_air_in + 273, T_air_out + 273, T_oil_in + 273, T_oil_out + 273));
			}
		}
	
		public ICommand CalcGeoCommand
		{
			get
			{
				return calc_Geometry ??= new RelayCommand(_ => true, _ =>
				{
					double Pr_o = calc.c_Oil_Prandtl();
					double Vm1 = calc.c_Oil_Speed(Oil_v, N12, D_out*.001f, S*.001f);
					double Re_o = calc.c_Oil_Reynolds(Vm1, D_out*.001f, S*.001f);
					double Nuss = calc.c_Oil_Nusselt(Pr_o, Re_o);
				});
			}
		}
	}
}
