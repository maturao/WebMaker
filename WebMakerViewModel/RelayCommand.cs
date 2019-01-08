using System;
using System.Windows.Input;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// Třída pro jednoduché přesměrování metod na ICommand pomocí delegátu Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;

        /// <summary>
        /// Vytvoří instanci třídy RelayCommand
        /// </summary>
        /// <param name="action">Delegát Action s parametrem object (použije se jako parametr pro ICommnad.Execute())</param>
        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        /// <summary>
        /// Vytvoří instanci třídy RelayCommand
        /// </summary>
        /// <param name="action">Delegát Action bez parametru</param>
        public RelayCommand(Action action)
        {
            this.action = (parameter) => action();
        }

        /// <summary>
        /// Event, který se spustí, pokud nastanou změny, které způsubí změnu toho, zda se command může spustit
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, args) => { };

        /// <summary>
        /// Zda se ICommand může v aktuálním stavu spustit 
        /// </summary>
        /// <param name="parameter">Parametr pro ICommand</param>
        /// <returns>Vždy true</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Metoda, která se spustí, pokud se spustí command
        /// </summary>
        /// <param name="parameter">Parametr pro ICommand</param>
        public void Execute(object parameter) => action(parameter);
    }
}