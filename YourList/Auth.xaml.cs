using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using YourList.Models;
using YourList.ViewModels;
using System.Collections.Generic;

namespace YourList
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        MainWindow mainWnd = new MainWindow();

        public Auth()
        {
            InitializeComponent();
            DataContext = new AuthViewModel(this);
        }
    }
}
