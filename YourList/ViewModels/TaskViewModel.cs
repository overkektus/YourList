﻿using System;
using System.Windows.Input;
using YourList.Models;
using YourList.Commands;

namespace YourList.ViewModels
{
    class TaskViewModel : ViewModelBase
    {
        public Task Task;

        public TaskViewModel(Task task)
        {
            Task = task;
        }

        public bool Done
        {
            get { return Task.Done; }
            set
            {
                Task.Done = value;
                OnPropertyChanged("Done");
            }
        }

        public string Title
        {
            get { return Task.Title; }
            set
            {
                Task.Title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Note
        {
            get { return Task.Note; }
            set
            {
                Task.Note = value;
                OnPropertyChanged("Note");
            }
        }

        public string Difficult
        {
            get { return Task.Difficult; }
            set
            {
                Task.Difficult = value;
                OnPropertyChanged("Difficult");
            }
        }

        public DateTime DeadLine
        {
            get { return Task.DeadLine; }
            set
            {
                Task.DeadLine = value;
                OnPropertyChanged("DeadLine");
            }
        }

        public DateTime? ReminderTime
        {
            get { return Task.ReminderTime; }
            set
            {
                Task.ReminderTime = value;
                OnPropertyChanged("ReminderTime");
            }
        }

        public DateTime? ReminderDate
        {
            get { return Task.ReminderDate; }
            set
            {
                Task.ReminderDate = value;
                OnPropertyChanged("ReminderDate");
            }
        }

        /*
        public DateTime? Reminder
        {
            get { return Task.Reminder; }
            set
            {
                Task.Reminder = value;
                OnPropertyChanged("Reminder");
            }
        }
        */

        private DelegateCommand addTaskCommand;

        public ICommand AddTaskCommand
        {
            get
            {
                if(addTaskCommand == null)
                {
                    addTaskCommand = new DelegateCommand(AddTask);
                }
                return addTaskCommand;
            }
        }

        private void AddTask()
        {

        }

    }
}
