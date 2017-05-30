using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourList.Commands;
using YourList.Models;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;

namespace YourList.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public Registration regwnd;

        public User regUser;
        private string login = "";
        private string password;

        public RegistrationViewModel(Registration _regwnd)
        {
            regwnd = _regwnd;
            regUser = new User();
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

        private RelayCommand addAvatarCommand;
        public RelayCommand AddAvatarCommand
        {
            get
            {
                return addAvatarCommand ??
                    (addAvatarCommand = new RelayCommand(obj =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "png files(*.png)|*.png|jpg Files (*.jpg)|*.jpg|jpeg files (*.jpeg)|*.jpeg|bmp files (*.bmp)|*.bmp";
                        openFileDialog.ShowDialog();

                        //сохраняю имя файла
                        String Img = openFileDialog.SafeFileName;
                        regUser.Img = Img;

                        //копирую аватарку в /Img/
                        string path = openFileDialog.FileName;
                        File.Copy(path, User.imgPath + Img);

                        //отображаю в окне аватарку
                        BitmapImage B = new BitmapImage();
                        B.BeginInit();
                        B.UriSource = new Uri(path);
                        regwnd.Avatar.Source = B;
                        B.EndInit();
                    }));
            }
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                    (closeCommand = new RelayCommand(obj =>
                    {
                        regwnd.Close();
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
                        regwnd.Close();
                        Auth authwnd = new Auth();
                        authwnd.Show();
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
                        var passwordBox = obj as PasswordBox;
                        Password = passwordBox.Password;

                        if (Login.Length == 0)
                        {
                            System.Windows.MessageBox.Show("Введите логин");
                        }
                        else regUser.Login = Login;

                        if (Password.Length == 0)
                        {

                        }
                        else regUser.Password = Password;


                        if(regUser.Login.Length != 0 && regUser.Password.Length != 0)
                            Database.Registration(regUser);
                    }));
            }
        }

        #endregion
    }
}
