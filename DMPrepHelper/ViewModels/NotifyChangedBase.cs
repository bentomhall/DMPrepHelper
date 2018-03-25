using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DMPrepHelper.ViewModels
{
    public class NotifyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool IsDirty = false;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName]string name = null)
        {
            bool propertyChanged = false;

            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(name);
                propertyChanged = true;
            }

            return propertyChanged;
        }

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
