using System.Collections.ObjectModel;
using YourList.Commands;
using YourList.Models;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Windows;
using YourList.Views;
using System;

namespace YourList.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        public string currentHeader = "All";
        private Task selectedTask;
        public User user;
        public ObservableCollection<Task> taskList;

        public string CurrentHeader
        {
            get { return currentHeader; }
            set
            {
                currentHeader = value;
                OnPropertyChanged("CurrentHeader");
            }
        }

        public ObservableCollection<Task> TaskList
        {
            get { return taskList; }
            set
            {
                taskList = value;
                OnPropertyChanged("TaskList");
            }
        }

        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public ApplicationViewModel(int idUsr, string _login, string _password, string _img)
        {
            TaskList = Database.GetTasks(idUsr, "sp_GetAllTask");
            user = new User(idUsr, _login, _password, _img);
        }

        #region Commands


        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        #region dialog

        private RelayCommand runDialogCommand;
        public RelayCommand RunDialogCommand => runDialogCommand ??
                    (runDialogCommand = new RelayCommand(async obj =>
                    {
                        var view = new AddDialog
                        {
                            DataContext = new AddDialogViewModel(user, ref taskList)
                        };

                        var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
                    }));

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }

        
        #endregion


        private RelayCommand getAllTaskCommand;
        public RelayCommand GetAllTaskCommand
        {
            get
            {
                return getAllTaskCommand ??
                    (getAllTaskCommand = new RelayCommand(obj =>
                    {
                        CurrentHeader = obj as string;
                        TaskList.Clear();
                        ObservableCollection<Task> tmpList = new ObservableCollection<Task>();
                        tmpList = Database.GetTasks(user.Id, "sp_GetAllTask");
                        foreach(Task t in tmpList)
                        {
                            TaskList.Add(t);
                        }
                    }));
            }
        }

        private RelayCommand getTodayTaskCommand;
        public RelayCommand GetTodayTaskCommand
        {
            get
            {
                return getTodayTaskCommand ??
                    (getTodayTaskCommand = new RelayCommand(obj =>
                    {
                        CurrentHeader = obj as string;
                        TaskList.Clear();
                        ObservableCollection<Task> tmpList = new ObservableCollection<Task>();
                        tmpList = Database.GetTasks(user.Id, "sp_GetTodayTask");
                        foreach (Task t in tmpList)
                        {
                            TaskList.Add(t);
                        }
                    }));
            }
        }

        private RelayCommand getMonthTaskCommand;
        public RelayCommand GetMonthTaskCommand
        {
            get
            {
                return getMonthTaskCommand ??
                    (getMonthTaskCommand = new RelayCommand(obj =>
                    {
                        CurrentHeader = obj as string;
                        TaskList.Clear();
                        ObservableCollection<Task> tmpList = new ObservableCollection<Task>();
                        tmpList = Database.GetTasks(user.Id, "sp_GetMonthTask");
                        foreach (Task t in tmpList)
                        {
                            TaskList.Add(t);
                        }
                    }));
            }
        }

        private RelayCommand getDoneTaskCommand;
        public RelayCommand GetDoneTaskCommand
        {
            get
            {
                return getDoneTaskCommand ??
                    (getDoneTaskCommand = new RelayCommand(obj =>
                    {
                        CurrentHeader = obj as string;
                        TaskList.Clear();
                        ObservableCollection<Task> tmpList = new ObservableCollection<Task>();
                        tmpList = Database.GetTasks(user.Id, "sp_GetDoneTask");
                        foreach (Task t in tmpList)
                        {
                            TaskList.Add(t);
                        }
                    }));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      MessageBox.Show("kek");
                  }));
            }
        }

        private RelayCommand logOutCommand;
        public RelayCommand LogOutCommand
        {
            get
            {
                return logOutCommand ??
                    (logOutCommand = new RelayCommand(obj =>
                    {
                        Window mainwnd = obj as Window;
                        mainwnd.Close();
                        Auth auth = new Auth();
                        auth.Show();
                    }));
            }
        }

        #endregion
    }
}
