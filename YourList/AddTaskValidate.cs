﻿using System;
using System.ComponentModel;
using MaterialDesignColors.WpfExample.Domain;

namespace YourList
{
    class AddTaskValidate
    {
        public class SampleDialogViewModel : INotifyPropertyChanged
        {
            private string _name;

            public string Name
            {
                get { return _name; }
                set
                {
                    this.MutateVerbose(ref _name, value, RaisePropertyChanged());
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private Action<PropertyChangedEventArgs> RaisePropertyChanged()
            {
                return args => PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
