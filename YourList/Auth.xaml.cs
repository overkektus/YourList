using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using YourList.ViewModels;
using YourList.Models;

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
            DataContext = new AuthViewModel();
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            List<User> UL = new List<User>();
            string Log = UserNameTextBox.Text.ToString();
            string Pass = FloatingPasswordBox.Password.ToString();

            UL =  Database.GetUsers();

            for (int i = 0; i < UL.Count; i++)
            {
                if (UL[i].Login == Log && UL[i].Password == Pass)
                {
                    mainWnd.Auth(UL[i].Id, UL[i].Login, UL[i].Password, UL[i].Img);
                    mainWnd.Show();
                    this.Close();
                }
                else
                {
                    
                }
            }

        }
    }
}
