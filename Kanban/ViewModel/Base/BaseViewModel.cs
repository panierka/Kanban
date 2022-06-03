using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.ViewModel.Base
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new(propertyName));
            }
        }
    }
}
