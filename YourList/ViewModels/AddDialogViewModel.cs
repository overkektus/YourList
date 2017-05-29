using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using YourList.Views;
using YourList.Commands;
using YourList.Models;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace YourList.ViewModels
{
    public class AddDialogViewModel : INotifyPropertyChanged
    {
        public User user;
        public ObservableCollection<Task> t;

        public AddDialogViewModel(User _user, ref ObservableCollection<Task> t)
        {
            user = _user;
            this.t = t;
        }

        private string title;
        private string difficult;
        private DateTime deadLine = DateTime.Now;
        private DateTime reminderDate = DateTime.Now;
        private DateTime reminderTime;
        private string note;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Difficult
        {
            get { return difficult; }
            set
            {
                difficult = value;
                OnPropertyChanged("Difficult");
            }
        }

        public DateTime DeadLine
        {
            get { return deadLine; }
            set
            {
                deadLine = value;
                OnPropertyChanged("DeadLine");
            }
        }

        public DateTime ReminderDate
        {
            get { return reminderDate; }
            set
            {
                reminderDate = value;
                OnPropertyChanged("ReminderDate");
            }
        }

        public DateTime ReminderTime
        {
            get { return reminderTime; }
            set
            {
                reminderTime = value;
                OnPropertyChanged("ReminderTime");
            }
        }

        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private RelayCommand addDialogCommand;
        public RelayCommand AddDialogCommand
        {
            get
            {
                return addDialogCommand ??
                    (addDialogCommand = new RelayCommand(obj =>
                    {
                        string RTime = ReminderTime.Hour + ":" + ReminderTime.Minute;
                        Task instTask = new Task(false, Title, Note, Difficult, DeadLine, ReminderDate);
                        t.Add(instTask);
                        Database.InsertTask(user.Id, instTask);
                    }));
            }
        }
    }
}
