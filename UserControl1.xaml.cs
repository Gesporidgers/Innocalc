using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Innocalc
{
	/// <summary>
	/// Логика взаимодействия для UserControl1.xaml
	/// </summary>
	public partial class UserControl1 : UserControl
	{
		public static readonly DependencyProperty CpmProperty =
			DependencyProperty.Register("Cpm", typeof(float), typeof(UserControl1));
		public static readonly DependencyProperty VmProperty =
			DependencyProperty.Register("Vm", typeof(float), typeof(UserControl1));
		public static readonly DependencyProperty RhoProperty =
			DependencyProperty.Register("RhoM", typeof(float), typeof(UserControl1));
		public static readonly DependencyProperty LambdaProperty =
			DependencyProperty.Register("LambdaM", typeof(float), typeof(UserControl1));

		public UserControl1()
		{
			InitializeComponent();
		}

		public float Cpm
		{
			get => (float)GetValue(CpmProperty);
			set => SetValue(CpmProperty, value);
		}
		public float Vm
		{
			get => (float)GetValue(VmProperty);
			set => SetValue(VmProperty, value);
		}
		public float RhoM
		{
			get => (float)GetValue(RhoProperty);
			set => SetValue(RhoProperty, value);
		}
		public float LambdaM
		{
			get => (float)GetValue(LambdaProperty);
			set => SetValue(LambdaProperty, value);
		}
	}
}
