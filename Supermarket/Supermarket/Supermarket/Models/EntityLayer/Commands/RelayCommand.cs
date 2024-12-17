using System;
using System.Windows;
using System.Windows.Input;

namespace Supermarket.ViewModels
{
    class RelayCommand<T> : ICommand
    {
        private Action<T> commandTask;
        private Predicate<T> canExecuteTask;

        public RelayCommand(Action<T> workToDo)
            : this(workToDo, DefaultCanExecute)
        {
            commandTask = workToDo;
        }

        public RelayCommand(Action<T> workToDo, Predicate<T> canExecute)
        {
            commandTask = workToDo;
            canExecuteTask = canExecute;
        }

        private static bool DefaultCanExecute(T parameter)
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteTask != null && canExecuteTask((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            if (parameter is T typedParameter)
            {
                commandTask(typedParameter);
            }
            else
            {
                // Afișăm tipul real al parametrului și valoarea sa
                MessageBox.Show($"Invalid parameter type. Expected: {typeof(T)}, Actual: {parameter?.GetType()}");

                // Aruncăm o excepție pentru a identifica mai ușor problema
                throw new ArgumentException("Invalid parameter type.");
            }
        }




    }
}
