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
using IntroSE.Kanban.Backend.ServiceLayer;
using Presentation.ViewModel;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service service = new Service();
        LoginViewModel vm = new LoginViewModel();
        public MainWindow()
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
            if (!vm.Join)
            {
                Response r = service.Register(vm.RegisterEmail, vm.RegisterPassword, vm.Nickname);
                if (r.ErrorOccured)
                    MessageBox.Show(r.ErrorMessage, "Error");
            }
            else
            {
                Response r = service.Register(vm.RegisterEmail, vm.RegisterPassword, vm.Nickname,vm.HostEmail);
                if (r.ErrorOccured)
                    MessageBox.Show(r.ErrorMessage, "Error");
            }
        }
    }
}
