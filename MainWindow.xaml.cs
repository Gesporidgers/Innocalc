using System.Windows;

namespace Innocalc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	MainVM vm;
	public MainWindow()
	{
		vm = new MainVM();
		DataContext = vm;
		InitializeComponent();
	}
}