using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YourList.Commands;
using YourList.Models;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Threading;

namespace YourList.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        private Task selectedTask;
        public User user;
        public ObservableCollection<Task> TaskList { get; set; }

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
            TaskList = Database.GetTasks(idUsr);
            user = new User(idUsr, _login, _password, _img);
        }


        #region Commands


        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        #endregion
    }
}
