using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Labanov
{
        public class CommandVM : ICommand
        { 


        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        Action action;
        Func<bool> canRun;
        private Action<object> removePatient;
        private Func<object, bool> canRemovePatient;

        public CommandVM(Action action, Func<bool> canRun = null)
            {
                this.action = action;
            this.canRun = canRun;   
            if (canRun == null) 
                canRun = () => true;
            }

        public CommandVM(Action<object> removePatient, Func<object, bool> canRemovePatient)
        {
            this.removePatient = removePatient;
            this.canRemovePatient = canRemovePatient;
        }

        public bool CanExecute(object? parameter)
            {
                return canRun();
            }

            public void Execute(object? parameter)
            {
                action();
            }
        }
    }