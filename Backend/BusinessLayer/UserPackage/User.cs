using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{



    class User
    {
        private string Password;
        private string Email;
        private string Nickname;
        private string Board;
        private bool logged_in;
        private DataAccessLayer.User dal;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public User(string email, string password, string nickname,string board)
        {
            this.Email = email;
            this.Nickname = nickname;
            this.Password = password;
            this.Board = board;
            dal = new DataAccessLayer.User(email, password, nickname,board);
            logged_in = false;
        }

        public User(DataAccessLayer.User u)
        {
            this.Password = u.password;
            this.Email = u.email;
            this.Nickname = u.nickname;
            this.Board = u.board;
            dal = u;
            logged_in = false;
        }
        public void login(string email, string password)
        {
            if (logged_in)
            {
                logger.Warn("cant login if the user is already logged in");
                throw new Exception("cant login if user is already logged in");
            }
            if (!this.Email.Equals(email))
            { 
                logger.Warn("wrong email");

                throw new Exception("email mismatch");
            }
            if (!this.Password.Equals(password))
            { 
                throw new Exception("password mismatch");
            }

            logged_in = true;
        }

        public void logout()
        {
            if (!logged_in)
            {
                logger.Warn("cant logout if the user is not logged in");

                throw new Exception("the user cant logout if he is not logged in");
            }
            this.logged_in = false;
        }



        public String getEmail()
        {
            return Email;
        }


        public string getNickname()
        {
            return this.Nickname;
        }


        public bool isloggedin()
        {
            return logged_in;
        }


        /*public DataAccessLayer.User ToDalObject()
        {
            DataAccessLayer.User u = new DataAccessLayer.User(this.Email,this.Password,this.Nickname);
            return u;
        }*/

        public void AddToDatabase()
        {
            DataAccessLayer.User u = new DataAccessLayer.User(this.Email, this.Password, this.Nickname,this.Board);
            u.Add();
        }


    }
}

