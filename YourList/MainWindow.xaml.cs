using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Data.SqlClient;
using MaterialDesignThemes.Wpf;
using System.Data;
using YourList.Models;
using YourList.ViewModels;

namespace YourList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int idUsr;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Auth(int _id, string _login, string _password, string _img)
        {
            DataContext = new ApplicationViewModel(_id, _login, _password, _img);
            idUsr = _id;
            SetCurrentUser(_login, _img);
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2500);
            }).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue.Enqueue("Welcome to YourList " +
                    _login);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SetCurrentUser(string _login, string _img)
        {
            Login.Text = _login;
            var uriSource = new Uri(User.imgPath + _img, UriKind.Relative);
            Avatar.Source = new BitmapImage(uriSource);
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
