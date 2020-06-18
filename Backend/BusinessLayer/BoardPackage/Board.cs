using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{

    class Board
    {
        private List<Column> Columns;
        private int TaskId;
        private string Email;
        private DataAccessLayer.Board dal;
        private DataAccessLayer.DalController ColumnData = new DataAccessLayer.DalController("data.db");
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Board(string email)
        {
            
            this.Columns = new List<Column>();
            Columns.Add(new Column("backlog"));
            Columns.Add(new Column("in progress"));
            Columns.Add(new Column("done"));
            for (int i = 0; i < 3; i++)
            {
                Columns[i].AddToDatabase(email, i);
            }
            TaskId = 1;
            this.Email = email;
        }

        public Board(DataAccessLayer.Board b)
        {
            List<Column> clms = new List<Column>();
            this.Columns = clms;
            this.TaskId = b.taskid;
            this.Email = b.email;
            this.dal = b;
            List<DataAccessLayer.Column> columns = ColumnData.LoadColumns(this.Email);
            foreach (DataAccessLayer.Column c in columns)
            {
                Columns.Add(new Column(c));
            }
        }
        public List<string> getColumnNames()
        {
            List<string> list = new List<string>();
            foreach (Column c in Columns)
                list.Add(c.name);
            return list;
        }

        public int getNumColumns()
        {
            return Columns.Count;
        }
        public string getEmail()
        {
            return Email;
        }

        public int taskid { get { return TaskId; } set { this.TaskId = value; dal.UpdateTaskID(TaskId); } }

        public Column GetColumn(string columnName)
        {
            foreach (Column c in Columns)
            {
                if (c.name == columnName)
                {
                    logger.Info("getting column");
                    return c;
                }
            }
            logger.Warn("no such column");
            throw new Exception("No such column!");
        }

        public Column GetColumn(int columnOrdinal)
        {
            if (columnOrdinal > Columns.Count | columnOrdinal < 0)
            {
                logger.Warn("no such column");
                throw new Exception("No such column!");
            }
            logger.Info("getting column");
            return Columns[columnOrdinal];
        }

        public void LimitColumnTasks(int columnOrdinal, int Setlimit)
        {
            if (GetColumn(columnOrdinal).tasks.Count > Setlimit && Setlimit != -1)
            {
                logger.Warn("The Number of Tasks Surpasses The Wanted Limit");
                throw new Exception("The Number of Tasks Surpasses The Wanted Limit!");
            }
            GetColumn(columnOrdinal).limit = Setlimit;
            logger.Info("Tasks in Column with" + columnOrdinal + "has been limited");
        }
        public void ChangeColumnName(int columnOrdinal, string newName)
        {
            GetColumn(columnOrdinal).name = newName;
            logger.Info("Tasks in Column with" + columnOrdinal + "has been limited");
        }

        public void RemoveColumn(string email, int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal >= Columns.Count)
            {
                logger.Warn("Invalid Column Number");
                throw new Exception("Invalid Column Number");
            }
            Column c = Columns[columnOrdinal];
            c.DeleteFromDatabase();
            for (int i = columnOrdinal; i < Columns.Count; i++)
            {
                Columns[i].UpdateOrdinal(i - 1);
            }
            int nc = columnOrdinal == 0 ? 0 : columnOrdinal - 1;
            Columns.RemoveAt(columnOrdinal);
            foreach (Task t in c.tasks)
            {
                //c.RemoveTask(t);
                Columns[nc].AddTask(t);
            }
        }

        public Column AddColumn(string email, int columnOrdinal, string Name)
        {
            if (columnOrdinal < 0 || columnOrdinal > Columns.Count)
            {
                logger.Warn("Invalid Column Number");
                throw new Exception("Invalid Column Number");
            }
            Column c = new Column(Name);
            c.AddToDatabase(email,columnOrdinal);
            for (int i = columnOrdinal; i < Columns.Count; i++)
            {
                Columns[i].UpdateOrdinal(i + 1);
            }
            Columns.Insert(columnOrdinal, c);
            return c;
        }

        public Column MoveColumnRight(string email, int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal >= Columns.Count)
            {
                logger.Warn("Invalid Column Number");
                throw new Exception("Invalid Column Number");
            }
            if (columnOrdinal == Columns.Count - 1)
            {
                logger.Warn("This is Already the Rightmost Column");
                throw new Exception("This is Already the Rightmost Column");
            }
            Column c = Columns[columnOrdinal];
            c.UpdateOrdinal(columnOrdinal + 1);
            Columns[columnOrdinal + 1].UpdateOrdinal(columnOrdinal);
            Columns.RemoveAt(columnOrdinal);
            Columns.Insert(columnOrdinal + 1,c);
            return c;
        }

        public Column MoveColumnLeft(string email, int columnOrdinal)
        {
            if (columnOrdinal < 0 || columnOrdinal >= Columns.Count)
            {
                logger.Warn("Invalid Column Number");
                throw new Exception("Invalid Column Number");
            }
            if (columnOrdinal == 0)
            {
                logger.Warn("This is Already the Leftmost Column");
                throw new Exception("This is Already the Leftmost Column");
            }
            Column c = Columns[columnOrdinal];
            c.UpdateOrdinal(columnOrdinal - 1);
            Columns[columnOrdinal - 1].UpdateOrdinal(columnOrdinal);
            Columns.RemoveAt(columnOrdinal);
            Columns.Insert(columnOrdinal - 1, c);
            return c;
        }
        public void AddToDatabase()
        {
            dal = new DataAccessLayer.Board(TaskId,Email);
            dal.Add();
        }
        /*public DataAccessLayer.Board ToDalObject()
        {
            DataAccessLayer.Board b = new DataAccessLayer.Board();
            List<DataAccessLayer.Column> list = new List<DataAccessLayer.Column>();
            Columns.ForEach(m => list.Add(m.ToDalObject()));
            b.taskid = this.TaskId;
            b.email = this.Email;
            b.columns = list;
            return b;
        }*/
    }
    
}

