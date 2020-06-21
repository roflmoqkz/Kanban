using Presentation.Model;
using Presentation.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class BoardViewModel
    {
        private BackendController controller;
        public UserModel user { get; private set; }
        public BoardModel Board { get; set; }
        public string Title { get; private set; }
        public BoardViewModel(UserModel user)
        {
            controller = user.Controller;
            this.user = user;
            this.Board = user.GetBoard();
            Title = "User: " + user.Email + " Board: " + Board.Email;
        }
        public void updateBoard()
        {
            this.Board.refresh();
        }
        public void editTask(int id)
        {
            TaskModel t = Board.findTask(id);
            TaskView tv = new TaskView(t, user);
            tv.ShowDialog();
            updateBoard();
        }
        public void deleteTask(int id)
        {
            TaskModel t = Board.findTask(id);
            if (MessageBox.Show("Are you sure you want to delete the task named" + t.Title, "Conformation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                controller.DeleteTask(user.Email, t.ordinal, id);
                updateBoard();
            }
        }
        public void advanceTask(int id)
        {
            TaskModel t = Board.findTask(id);
            controller.AdvanceTask(user.Email, t.ordinal, id);
            updateBoard();
        }
        public void Logout()
        {
            controller.Logout(user.Email);
        }
    }
}
