using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer.UserPackage;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class BoardController
    {
        private Dictionary<string, Board> map;
        private DataAccessLayer.DalController BoardData;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int DEFAULT_LIMIT = -1;
        private const int MAX_TITLE = 50;
        private const int MAX_DESC = 300;

        public BoardController()
        {
            
            map = new Dictionary<string, Board>();
            BoardData = new DataAccessLayer.DalController("data.db");
        }
        
        public void LoadAllBoards()
        {
            if (map.Count != 0)
            {
                logger.Error("BoardController: Data already been loaded.");
                throw new Exception("Data Already Been Loaded!");
            }
            List<DataAccessLayer.Board> boards = BoardData.LoadAllBoards();
            foreach (DataAccessLayer.Board b in boards)
            {
                map.Add(b.email, new Board(b));
            }

            logger.Info("BoardController: Data has started loading!!.");

        }
        public void emailExists(string email)
        {
            if (!map.ContainsKey(email) || map[email].getEmail() != email)
            {
                logger.Warn("there is no board with this host email");
                throw new Exception("invalid email host");
            }
        }
        public void assignBoard(string email,string emailHost)
        {
            map.Add(email, map[emailHost]);
            logger.Info("BoardController: assigning board of host : " + emailHost + ", to user " + email + " succeeded!!");
        }
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            emailExists(emailAssignee);
            email = email.ToLower();
            if (!map.ContainsKey(email))
            {
                logger.Warn("email doesn't exist");
                throw new Exception("Invalid Parmeters!");
            }
            Board b = getBoard(email);
            Task t = b.GetColumn(columnOrdinal).GetTask(taskId);
            t.AssignTask(emailAssignee);
            logger.Info("BoardController: assigning the task: " + taskId + ", in column: " + columnOrdinal + " succeeded!!");
        }
        public void DeleteTask(string email, int columnOrdinal, int taskId)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            Task t = b.GetColumn(columnOrdinal).GetTask(taskId);
            b.GetColumn(columnOrdinal).RemoveTask(t);
            logger.Info("BoardController: deleting the task: " + taskId + ", in column: " + columnOrdinal + " succeeded!!");
        }
        public Board getBoard(string email)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            if (!map.ContainsKey(email))
            {
                logger.Warn("BoardController: " + email + " is not used!");
                throw new Exception("email doesnt exist");
            }
            logger.Info("BoardController: returning board for user : " + email + "!");
            return map[email];
        }

        public void addBoard(string email)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            if (map.ContainsKey(email))
            {
                logger.Warn("BoardController: " + email + " is in use!");
                throw new Exception("email already exist");
            }
            Board b = new Board(email);
            map.Add(email, b);
            b.AddToDatabase();
            logger.Info("BoardController: adding board for user : " + email + " succeeded!");
        }

        public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            if (email == null | title == null)
            {
                logger.Warn("invalid email or title");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            title = Trim(title);
            Task t = b.GetColumn(columnOrdinal).GetTask(taskId);
            if (columnOrdinal == b.getNumColumns())
            {
                logger.Warn("BoardController: updating the titile of task: " + taskId + ", in column: " + columnOrdinal + " failed!!");
                throw new Exception("Can't update the title of a finished task!");
            }
            t.EditTitle(title);
            logger.Info("BoardController: updating the title of task: " + taskId + ", in column: " + columnOrdinal + " succeeded!!");
        }

        public void UpdateTaskDueDate(string email, int columnOrd, int taskId, DateTime dDate)
        {
            if (email == null | dDate == null)
            {
                logger.Warn("invalid email or dateTime");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            Task t = b.GetColumn(columnOrd).GetTask(taskId);
            if (columnOrd == b.getNumColumns())
            {
                logger.Warn("BoardController: updating duedate of task: " + taskId + ", in column: " + columnOrd + " failed!!");
                throw new Exception("Can't update the due date of a finished task!");
            }
            t.EditDueDate(dDate);
            logger.Info("BoardController: updating duedate of task: " + taskId + ", in column: " + columnOrd + " succeeded!!");
        }

        public void UpdateTaskDescription(string email, int columnOrd, int taskId, string descrip)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            if (descrip == null)
                descrip = "";
            Board b = getBoard(email);
            descrip = Trim(descrip);
            Task t = b.GetColumn(columnOrd).GetTask(taskId);
            if (columnOrd == b.getNumColumns())
            {
                logger.Warn("BoardController: updating the description of task: " + taskId + ", in column: " + columnOrd + " failed!!");

                throw new Exception("Can't update the description of a finished task!");
            }
            t.EditDescription(descrip);
            logger.Info("BoardController: updating the description of task: " + taskId + ", in column: " + columnOrd + " succeeded!!");

        }

        public Task AddTask(string email, string title, string description, DateTime dueDate)
        {
            if (email == null | title == null | dueDate == null)
            {
                logger.Warn("invalid email or title or dateTime");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            if (description == null)
                description = "";
            Board b = getBoard(email);
            title = Trim(title);
            description = Trim(description);
            if (title.Length > MAX_TITLE | title.Equals(""))
            {
                logger.Warn("BoardController: adding task to the user with email:" + email + "has failed");

                throw new Exception("Illegal Title!");
            }
            if (description.Length > MAX_DESC)
            {
                logger.Warn("BoardController: adding task to the user with email:" + email + "has failed");

                throw new Exception("Illegal Description!");
            }
            if (dueDate.CompareTo(DateTime.Today) < 0)
            {
                logger.Warn("BoardController: adding task to the user with email:" + email + "has failed");

                throw new Exception("Can't Set the Due Date To Be a Past Date!");
            }
            if (b.GetColumn(0).limit != DEFAULT_LIMIT & b.GetColumn(0).tasks.Count == b.GetColumn(0).limit)
            {
                logger.Warn("BoardController: adding task to the user with email:" + email + "has failed");

                throw new Exception("Can't Add! Column Limit Has Been Reached!");
            }
            int id = b.taskid;
            Task toAdd = new Task(id,email, DateTime.Now, title, description, dueDate);
            b.taskid = id + 1;
            b.GetColumn(0).AddTask(toAdd);
            logger.Info("BoardController: adding task to the user with email:" + email + "has succeeded!!");
            return toAdd;

        }

        public void LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeter!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            b.LimitColumnTasks(columnOrdinal, limit);
            logger.Info("BoardController: limiting the tasks in email:" + email + " in column "+ columnOrdinal+ " to " + limit + " has succeeded!!");
        }

        public void ChangeColumnName(string email, int columnOrdinal, string newName)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeter!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            b.ChangeColumnName(columnOrdinal, newName);
            logger.Info("BoardController: changing the column name in email:" + email + " of column " + columnOrdinal + " to " + newName + " has succeeded!!");
        }

        public void AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid email!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            Column fromColumn = b.GetColumn(columnOrdinal);
            Task toAdvance = fromColumn.GetTask(taskId);
            if (columnOrdinal == b.getNumColumns())
            {
                logger.Warn("BoardController: advancing task to the user with email:" + email + "has failed");

                throw new Exception("Can't Advance A Task That Has Been Finished!");
            }

            Column ToColumn = b.GetColumn(columnOrdinal + 1);

            if (ToColumn.limit != DEFAULT_LIMIT & ToColumn.tasks.Count == ToColumn.limit)
            {
                logger.Warn("BoardController: advancing task to the user with email:" + email + "has failed");
                throw new Exception("Can't Advance! Column Limit Has Been Reached!");
            }
            fromColumn.RemoveTask(toAdvance);
            ToColumn.AddTask(toAdvance);
            logger.Info("BoardController: advancing task to the user with email:" + email + "has succeeded!!");

        }

        public Column GetColumn(string email, string columnName)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            logger.Info("BoardController: returning column for user : " + email + "!");
            return b.GetColumn(columnName);
        }

        public Column GetColumn(string email, int columnOrdinal)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeters!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            logger.Info("BoardController: returning column for user : " + email + "!");
            return b.GetColumn(columnOrdinal);
        }

        public void RemoveColumn(string email, int columnOrdinal)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeter!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            b.RemoveColumn(email, columnOrdinal);
            logger.Info("BoardController: removing column to the user with email:" + email + "has succeeded!!");
        }

        public Column AddColumn(string email, int columnOrdinal, string Name)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeter!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            logger.Info("BoardController: adding column to the user with email:" + email + "has succeeded!!");
            return b.AddColumn(email, columnOrdinal, Name);
        }

        public Column MoveColumnRight(string email, int columnOrdinal)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeter!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            logger.Info("BoardController: moving column to the right to the user with email:" + email + "has succeeded!!");
            return b.MoveColumnRight(email, columnOrdinal);
        }

        public Column MoveColumnLeft(string email, int columnOrdinal)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("Invalid Parmeter!");
            }
            email = email.ToLower();
            Board b = getBoard(email);
            logger.Info("BoardController: moving column to the left to the user with email:" + email + "has succeeded!!");
            return b.MoveColumnLeft(email, columnOrdinal);
        }

        private string Trim(string inputStr)
        {
            for (int i = 0; i < inputStr.Length; i++)
                if (inputStr.ElementAt(i) != ' ')
                    return inputStr;
            return "";
        }

    }
}
