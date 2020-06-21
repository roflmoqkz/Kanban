using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class BoardViewModel
    {
        private BackendController controller;
        public UserModel user { get; private set; }
        public BoardModel Board { get; private set; }
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
            Board = controller.GetBoard(user.Email);
        }
        public void Logout()
        {
            controller.Logout(user.Email);
        }
    }
}
