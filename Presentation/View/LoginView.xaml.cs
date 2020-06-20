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
using IntroSE.Kanban.Backend;
using Presentation.Model;
using Presentation.ViewModel;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        LoginViewModel vm = new LoginViewModel();
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void JoinCheck_Checked(object sender, RoutedEventArgs e)
        {
            HostText.Background = Brushes.White;
            HostText.IsEnabled = true;
        }

        private void JoinCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            HostText.Background = new SolidColorBrush(Color.FromRgb(243, 243, 243));
            HostText.IsEnabled = false;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            vm.Register();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserModel u = vm.Login();
            if (u != null)
            {
                BoardView boardView = new BoardView(u);
                boardView.Show();
                this.Close();
            }
        }
    }
}
