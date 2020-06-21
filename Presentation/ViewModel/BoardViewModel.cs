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
            try
            {
                TaskModel t = Board.findTask(id);
                if (MessageBox.Show("Are you sure you want to delete the task named" + t.Title, "Conformation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    controller.DeleteTask(user.Email, t.ordinal, id);
                    updateBoard();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void advanceTask(int id)
        {
            try
            {
                TaskModel t = Board.findTask(id);
                controller.AdvanceTask(user.Email, t.ordinal, id);
                updateBoard();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void MoveLeft(int ordinal)
        {
            try
            {
                controller.MoveColumnLeft(user.Email, ordinal);
                updateBoard();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void Rename(int ordinal)
        {
            DialogBox db = new DialogBox("Select the new name of the column");
            db.ShowDialog();
            if (db.result == null)
                return;
            try
            {
                controller.ChangeColumnName(user.Email, ordinal,db.result);
                updateBoard();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void MoveRight(int ordinal)
        {
            try
            {
                controller.MoveColumnRight(user.Email, ordinal);
                updateBoard();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void CreateLeft(int ordinal)
        {
            DialogBox db = new DialogBox("Select the name of the column you want to insert");
            db.ShowDialog();
            if (db.result == null)
                return;
            try
            {
                controller.AddColumn(user.Email, ordinal, db.result);
                updateBoard();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void Remove(int ordinal)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this column", "Conformation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    controller.RemoveColumn(user.Email, ordinal);
                    updateBoard();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void CreateRight(int ordinal)
        {
            DialogBox db = new DialogBox("Select the name of the column you want to insert");
            db.ShowDialog();
            if (db.result == null)
                return;
            try
            {
                controller.AddColumn(user.Email, ordinal+1, db.result);
                updateBoard();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        public void Logout()
        {
            controller.Logout(user.Email);
        }
    }
}
