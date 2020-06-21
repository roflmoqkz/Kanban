using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class User : DalObject<User>
    {
        public string email { get; }
        public string password { get; }
        public string nickname { get; }
        public string board { get; }

        public User(string email,string password,string nickname,string board)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.board = board;
        }
        public void Add()
        {
            DalC.query($"INSERT INTO users (email,password,nickname,board) VALUES ('{email}','{password}','{nickname}','{board}')");
        }

    }
}
