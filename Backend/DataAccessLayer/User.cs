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

        public User(string email,string password,string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        public void Add()
        {
            DalC.query($"INSERT INTO users (email,password,nickname) VALUES ('{email}','{password}','{nickname}')");
        }

    }
}
