using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntroSE.Kanban.Backend.ServiceLayer;
using Presentation.Model;

namespace Presentation
{
    class BackendController
    {
        Service service = new Service();
        public void LoadData()
        {
            Response response = service.LoadData();
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void DeleteData()
        {
            Response response = service.DeleteData();
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void Register(string email, string password, string nickname)
        {
            Response response = service.Register(email,password,nickname);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void Register(string email, string password, string nickname, string emailHost)
        {
            Response response = service.Register(email, password, nickname,emailHost);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            Response response = service.AssignTask(email, columnOrdinal, taskId, emailAssignee);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void DeleteTask(string email, int columnOrdinal, int taskId)
        {
            Response response = service.DeleteTask(email, columnOrdinal, taskId);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public UserModel Login(string email, string password)
        {
            Response<User> response = service.Login(email,password);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new UserModel(this, email);
        }
        public void Logout(string email)
        {
            Response response = service.Logout(email);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public BoardModel GetBoard(string email)
        {
            Response<Board> response = service.GetBoard(email);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new BoardModel(this,response.Value.emailCreator, response.Value.ColumnsNames);
        }
        public void LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            Response response = service.LimitColumnTasks(email,columnOrdinal,limit);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void ChangeColumnName(string email, int columnOrdinal, string newName)
        {
            Response response = service.ChangeColumnName(email,columnOrdinal,newName);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public TaskModel AddTask(string email, string title, string description, DateTime dueDate)
        {
            Response<Task> response = service.AddTask(email,title,description,dueDate);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return createTask(response.Value);
        }
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response response = service.UpdateTaskDueDate(email, columnOrdinal, taskId,dueDate);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            Response response = service.UpdateTaskTitle(email, columnOrdinal, taskId, title);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            Response response = service.UpdateTaskDescription(email, columnOrdinal, taskId, description);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public void AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            Response response = service.AdvanceTask(email, columnOrdinal, taskId);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public ColumnModel GetColumn(string email, string columnName)
        {
            Response<Column> response = service.GetColumn(email,columnName);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new ColumnModel(this,response.Value.Name,response.Value.Limit,response.Value.Tasks);
        }
        public ColumnModel GetColumn(string email, int columnOrdinal)
        {
            Response<Column> response = service.GetColumn(email, columnOrdinal);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new ColumnModel(this, response.Value.Name, response.Value.Limit, response.Value.Tasks);
        }
        public void RemoveColumn(string email, int columnOrdinal)
        {
            Response response = service.RemoveColumn(email, columnOrdinal);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public ColumnModel AddColumn(string email, int columnOrdinal, string Name)
        {
            Response<Column> response = service.AddColumn(email, columnOrdinal,Name);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new ColumnModel(this, response.Value.Name, response.Value.Limit, response.Value.Tasks);
        }
        public ColumnModel MoveColumnRight(string email, int columnOrdinal)
        {
            Response<Column> response = service.MoveColumnRight(email, columnOrdinal);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new ColumnModel(this, response.Value.Name, response.Value.Limit, response.Value.Tasks);
        }
        public ColumnModel MoveColumnLeft(string email, int columnOrdinal)
        {
            Response<Column> response = service.MoveColumnLeft(email, columnOrdinal);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            return new ColumnModel(this, response.Value.Name, response.Value.Limit, response.Value.Tasks);
        }
        public TaskModel createTask(Task t)
        {
            return new TaskModel(this, t.Id, t.CreationTime, t.DueDate, t.Title, t.Description, t.emailAssignee);
        }
    }
}
