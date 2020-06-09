using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board : DalObject<Board>
    {
        public int taskid { get; private set; }
        public string email { get; }
        public Board(int taskid,string email) : base()
        {
            this.taskid = taskid;
            this.email = email;
        }
        public void Add()
        {
            DalC.query($"INSERT INTO boards (taskId,email) VALUES ({taskid},'{email}')");
        }
        public void UpdateTaskID(int taskid)
        {
            this.taskid = taskid;
            DalC.query($"UPDATE boards SET taskId={taskid} WHERE email='{email}'");
        }

    }
}
