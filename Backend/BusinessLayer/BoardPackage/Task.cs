using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Task
    {
        private int Id;
        private string Email;
        private string Assigned;
        private DateTime CreationTime;
        private DateTime DueDate;
        private string Title;
        private string Description;
        private DataAccessLayer.Task dal;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int MAX_TITLE = 50;
        private const int MAX_DESC = 300;

        public Task(int Id, string Email, string Assigned, DateTime CreationTime, string Title, string Description, DateTime DueDate)
        {
            this.Id = Id;
            this.Email = Email;
            this.Assigned = Assigned;
            this.CreationTime = CreationTime;
            this.DueDate = DueDate;
            this.Title = Title;
            this.Description = Description;
        }

        public Task(DataAccessLayer.Task t)
        {
            this.Id = t.id;
            this.CreationTime = DateTime.Parse(t.creationTime);
            this.DueDate = DateTime.Parse(t.dueDate);
            this.Title = t.title;
            this.Description = t.description;
            this.dal = t;
        }

        public int TaskId { get { return Id; } }
        
        public string TaskAssigned { get { return Assigned; } }
        public string TaskTitle { get { return Title; } }

        public string TaskDescription { get { return Description; } }

        public DateTime TaskCreationTime { get { return CreationTime; } }

        public DateTime TaskDueDate { get { return DueDate; } }
        public void AssignTask(string emailAssignee)
        {
            this.Email = emailAssignee;
            dal.UpdateAssigned(emailAssignee);
            logger.Info("task has been successfully assigned");
        }
        public void EditTitle(string title)
        {
            if (title.Length > MAX_TITLE | title.Equals(""))
            {
                logger.Warn("illega title");
                throw new Exception("Illegal Title!");
            }
            this.Title = title;
            dal.UpdateTitle(title);
            logger.Info("title has been successfully edited");
        }
        public void EditDueDate(DateTime dDate)
        {
            if (dDate.CompareTo(DateTime.Today) < 0)
            {
                logger.Warn("Can't Set the Due Date To Be a Past Date");
                throw new Exception("Can't Set the Due Date To Be a Past Date!");
            }
            this.DueDate = dDate;
            dal.UpdateDueDate(dDate);
            logger.Info("DateTime has been successfully edited");
        }

        public void EditDescription(string description)
        {
            if (description.Length > MAX_DESC)
            {
                logger.Warn("illegal description");
                throw new Exception("Illegal Description!");
            }
            this.Description = description;
            dal.UpdateDescription(description);
            logger.Info("description has been successfully edited");
        }
        public void AddToDatabase(string email,long column)
        {
            dal = new DataAccessLayer.Task(Id, email,Email, CreationTime, DueDate, Title, Description, column);
            dal.Add();
        }
        public void DeleteFromDatabase()
        {
            dal.Delete();
        }
        /*public DataAccessLayer.Task ToDalObject()
        {
            DataAccessLayer.Task t = new DataAccessLayer.Task();
            t.id = this.Id;
            t.title = this.Title;
            t.creationtime = this.CreationTime;
            t.duedate = this.DueDate;
            t.description = this.Description;
            return t;
        }*/
    }
}
