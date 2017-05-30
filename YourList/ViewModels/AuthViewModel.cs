using System.Windows;
using System.Collections.Generic;
using YourList.Commands;
using YourList.Models;
using System.Windows.Controls;
using System.IO;
using System.Linq;

namespace YourList.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {

        public Auth authwnd;
        private string login;
        private string password;

        public AuthViewModel(Auth _authwnd)
        {
            authwnd = _authwnd;

            CreateImgDir();
        }

        public void CreateImgDir()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string[] imgDir = Directory.GetDirectories(currentDir, "Img");
            if (imgDir.Count() == 0)
                Directory.CreateDirectory(currentDir + "/Img");
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #region Commands    

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                    (closeCommand = new RelayCommand(obj =>
                    {
                        Window authwnd = obj as Window;
                        authwnd.Close();
                    }));
            }
        }

        private RelayCommand logInCommand;
        public RelayCommand LogInCommand
        {
            get
            {
                return logInCommand ??
                    (logInCommand = new RelayCommand(obj =>
                    {
                        var passwordBox = obj as PasswordBox;
                        Password = passwordBox.Password;

                        List<User> UL = new List<User>();
                        UL = Database.GetUsers();

                        for (int i = 0; i < UL.Count; i++)
                        {
                            if (UL[i].Login == Login && UL[i].Password == Password)
                            {
                                MainWindow mainWnd = new MainWindow();
                                mainWnd.Auth(UL[i].Id, UL[i].Login, UL[i].Password, UL[i].Img);
                                mainWnd.Show();
                                authwnd.Close();
                                
                            }
                            else
                            {
                            }
                        }
                    }));
            }
        }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand
        {
            get
            {
                return registrationCommand ??
                    (registrationCommand = new RelayCommand(obj =>
                    {
                        CloseCommand.Execute(obj as Window);
                        Registration reg = new Registration();
                        reg.Show();
                    }));
            }
        }

        #endregion
    }
}
