using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YourList.Models
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string login;
        private string password;
        private string img;

        static public string imgPath = @"Img\";

        public User (int _id, string _login, string _password, string _img)
        {
            Id = _id;
            Login = _login;
            Password = _password;
            Img = _img;
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
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

        public string Img
        {
            get { return img; }
            set
            {
                img = value;
                OnPropertyChanged("Img");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
