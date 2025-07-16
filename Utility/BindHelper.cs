using System.ComponentModel;

namespace Innocalc.Utility
{
	public class BindHelper : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string Name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
		}
	}
}
