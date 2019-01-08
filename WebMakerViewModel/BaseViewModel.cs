using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WebMaker.ViewModel
{
    /// <summary>
    /// Základní ViewModel, ze kterého dědí všechny ostatní ViewModely
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Spustí se, pokud se nějaká property změní
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Spustí event PropertyChanged
        /// </summary>
        /// <param name="propertyName">Názec změněné property</param>
        protected void RaiseNotifyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
