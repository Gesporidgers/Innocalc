﻿using System.Windows.Input;

namespace Innocalc.Utility
{
	public class RelayCommand : ICommand
	{
		private Predicate<object> _canExecute;

		private Action<object> _execute;

		public RelayCommand(Predicate<object> canExecute, Action<object> execute)
		{
			this._canExecute = canExecute;
			this._execute = execute;
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter) => _canExecute(parameter);

		public void Execute(object parameter) => _execute(parameter);
	}
}
