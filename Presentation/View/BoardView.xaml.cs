using Presentation.Model;
using Presentation.View;
using Presentation.ViewModel;
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
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class BoardView : Window
    {
        BoardViewModel vm;
        public BoardView(UserModel user)
        {
            InitializeComponent();
            this.vm = new BoardViewModel(user);
            this.DataContext = vm;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskView view = new TaskView(null, vm.user);
            view.ShowDialog();
            vm.updateBoard();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            vm.Logout();
            LoginView LoginView = new LoginView();
            LoginView.Show();
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            vm.editTask((int)(sender as Button).Tag);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            vm.deleteTask((int)(sender as Button).Tag);
        }

        private void Advance_Click(object sender, RoutedEventArgs e)
        {
            vm.advanceTask((int)(sender as Button).Tag);
        }
    }
}
