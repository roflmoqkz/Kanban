using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Column
    {
        private string Name;
        private int Limit;
        private List<Task> Tasks;
        private DataAccessLayer.Column dal;
        private DataAccessLayer.DalController TaskData = new DataAccessLayer.DalController("data.db");
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Column(string Name)
        {
            this.Name = Name;
            Limit = -1;
            Tasks = new List<Task>();
        }

        public Column(DataAccessLayer.Column c)
        {
            this.Name = c.name;
            this.Limit = c.limit;
            List<Task> tsk = new List<Task>();
            this.Tasks = tsk;
            this.dal = c;
            List<DataAccessLayer.Task> tasks = TaskData.LoadTasks(this.dal.id);
            foreach (DataAccessLayer.Task t in tasks)
            {
                Tasks.Add(new Task(t));
            }
        }
        public void UpdateOrdinal(int ordinal)
        {
            dal.UpdateOrdinal(ordinal);
        }
        public int limit { get { return Limit; } set { Limit = value; dal.UpdateLimit(limit); } }
        public string name { get { return Name; } set { Name = value; dal.UpdateName(Name); } }
        public List<Task> tasks { get { return Tasks; } }
         
        public List<Task> getTask()
        {
            return Tasks;
        }
        public Task GetTask(int id)
        {
            for (int i = 0; i < Tasks.Count; i = i + 1)
                if (Tasks[i].TaskId == id)
                {
                    logger.Info("returning task with id"+ id + "!");
                    return Tasks[i];
                }
            logger.Warn("Task with id"+ id+ "not found!");
            throw new Exception("Task Not found!");
        }

        public void AddTask(Task toAdd)
        {
            if (!Tasks.Contains(toAdd))
            {
                logger.Info("Added "+ toAdd + "!");
                Tasks.Add(toAdd);
                toAdd.AddToDatabase(dal.email, dal.id);
            }
            else
            {
                logger.Warn("task already exists");
                throw new Exception("Task Already Exists!");
            }
        }

        public void RemoveTask(Task toRemove)
        {
            Tasks.Remove(toRemove);
            toRemove.DeleteFromDatabase();
        }

        public void AddToDatabase(string email,int ordinal)
        {
            dal = new DataAccessLayer.Column(name, limit, email, ordinal);
            dal.Add();
        }
        public void DeleteFromDatabase()
        {
            dal.Delete();
        }
        /*public DataAccessLayer.Column ToDalObject()
        {
            DataAccessLayer.Column c = new DataAccessLayer.Column();
            List<DataAccessLayer.Task> list = new List<DataAccessLayer.Task>();
            for (int i = 0; i < Tasks.Count; i++)
                list.Add(Tasks[i].ToDalObject());
            c.tasks = list;
            c.name = this.Name;
            c.limit = this.Limit;
            return c;
        }*/
    }
}
