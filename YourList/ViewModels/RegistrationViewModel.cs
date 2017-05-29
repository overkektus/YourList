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

namespace YourList.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public Registration regwnd;

        public User regUser;
        private string login;
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

                        String Img = openFileDialog.SafeFileName;
                        regUser.Img = Img;

                        string path = openFileDialog.FileName;

                        //System.Windows.MessageBox.Show(path);

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
                        Window authwnd = obj as Window;
                        authwnd.Close();
                    }));
            }
        }

        #endregion
    }
}
