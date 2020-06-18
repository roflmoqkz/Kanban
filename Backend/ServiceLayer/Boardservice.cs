using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class BoardService
    {
        private BoardController BC = new BoardController();

        internal BoardService() { }
        public Response addBoard(string email)
        {
            try
            {
                BC.addBoard(email);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response assignBoard(string email,string emailHost)
        {
            try
            {
                BC.assignBoard(email,emailHost);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                BC.AssignTask(email, columnOrdinal, taskId, emailAssignee);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response DeleteTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                BC.DeleteTask(email, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response emailExists(string email)
        {
            try
            {
                BC.emailExists(email);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response LoadData()
        {
            try
            {
                BC.LoadAllBoards();
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response DeleteData()
        {
            try
            {
                DataAccessLayer.DalController dal = new DataAccessLayer.DalController("data.db");
                dal.DeleteData();
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response<Board> GetBoard(string email)
        {
            try
            {
                Board b = new Board(BC.getBoard(email));
                return new Response<Board>(b);
            }
            catch (Exception e)
            {
                return new Response<Board>(e.Message);
            }
        }

        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            try
            {
                BC.LimitColumnTasks(email, columnOrdinal, limit);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response ChangeColumnName(string email, int columnOrdinal, string newName)
        {
            try
            {
                BC.ChangeColumnName(email, columnOrdinal, newName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            try
            {

                Task tsk = new Task(BC.AddTask(email, title, description, dueDate));
                return new Response<Task>(tsk);
            }
            catch (Exception e)
            {
                return new Response<Task>(e.Message);
            }
        }

        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                BC.AdvanceTask(email, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                BC.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            try
            {
                BC.UpdateTaskTitle(email, columnOrdinal, taskId, title);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            try
            {
                BC.UpdateTaskDescription(email, columnOrdinal, taskId, description);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<Column> GetColumn(string email, string columnName)
        {
            try
            {
                Column c = new Column(BC.GetColumn(email, columnName));
                return new Response<Column>(c);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            try
            {
                Column c = new Column(BC.GetColumn(email, columnOrdinal));
                return new Response<Column>(c);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }

        public Response RemoveColumn(string email, int columnOrdinal)
        {
            try
            {
               BC.RemoveColumn(email, columnOrdinal);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<Column> AddColumn(string email, int columnOrdinal,string Name)
        {
            try
            {
                Column c = new Column(BC.AddColumn(email,columnOrdinal,Name));
                return new Response<Column>(c);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }

        public Response<Column> MoveColumnRight(string email, int columnOrdinal)
        {
            try
            {
                Column c = new Column(BC.MoveColumnRight(email, columnOrdinal));
                return new Response<Column>(c);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }

        public Response<Column> MoveColumnLeft(string email, int columnOrdinal)
        {
            try
            {
                Column c = new Column(BC.MoveColumnLeft(email, columnOrdinal));
                return new Response<Column>(c);
            }
            catch (Exception e)
            {
                return new Response<Column>(e.Message);
            }
        }
    }
}
