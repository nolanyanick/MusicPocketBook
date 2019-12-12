using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MusicPocketBook
{
    /// <summary>
    /// A command whose sole purpose is to 
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        private Action<Object> _actionP1;
        private Action _action;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> action)            
        {
            _actionP1 = action;
        }

        public RelayCommand(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            _actionP1 = null;
            
        }

        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(object parameters)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged; 

        public void Execute(object parameter)
        {
            if (_actionP1 == null && _action == null)
            {
                _execute(parameter);
            }
            else if (parameter != null)
            {
                _actionP1(parameter);
            }
            else
            {
                _action();
            }
        }

        #endregion // ICommand Members
    }
}