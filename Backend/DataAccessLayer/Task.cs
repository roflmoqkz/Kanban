using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Task : DalObject<Task>
    {
        public int id { get; }
        public string email { get; }
        public string creationTime { get; }
        public string dueDate { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public long column { get;}
        private const string DS = "yyyy-MM-dd HH:mm:ss";
        public Task(int id, string email, DateTime creationTime, DateTime dueDate, string title, string description,long column)
        {
            this.id = id;
            this.email = email;
            this.creationTime = creationTime.ToString(DS);
            this.dueDate = dueDate.ToString(DS);
            this.title = title;
            this.description = description;
            this.column = column;
        }
        public void Add()
        {
            DalC.query($"INSERT INTO tasks (id,email,creationTime,dueDate,title,description,column) VALUES ({id},'{email}','{creationTime}','{dueDate}','{title}','{description}',{column})");
        }
        public void UpdateDueDate(DateTime dueDate)
        {
            this.dueDate = dueDate.ToString(DS);
            DalC.query($"UPDATE tasks SET dueDate='{dueDate}' WHERE id={id} AND email='{email}'");
        }
        public void UpdateTitle(string title)
        {
            this.title = title;
            DalC.query($"UPDATE tasks SET title='{title}' WHERE id={id} AND email='{email}'");
        }
        public void UpdateDescription(string desctiption)
        {
            this.description = desctiption;
            DalC.query($"UPDATE tasks SET description='{description}' WHERE id={id} AND email='{email}'");
        }
        public void Delete()
        {
            DalC.query($"DELETE FROM tasks WHERE id={id} AND email='{email}'");
        }
    }
}
