using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    class TaskModel : NotifiableModelObject
    {
        int id;
        DateTime creationTime;
        DateTime dueDate;
        string title;
        string description;
        string emailAssignee;
        public int Id { get { return id; } set { id = value; RaisePropertyChanged("Id"); } }
        public DateTime CreationTime { get { return creationTime; } set { creationTime = value; RaisePropertyChanged("CreationTime"); } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; RaisePropertyChanged("DueDate"); } }
        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); } }
        public string Description { get { return description; } set { description = value; RaisePropertyChanged("Description"); } }
        public string EmailAssignee { get { return emailAssignee; } set { emailAssignee = value; RaisePropertyChanged("EmailAssignee"); } }
        public TaskModel(BackendController controller, int id, DateTime creationTime, DateTime dueDate, string title, string description, string emailAssignee) : base(controller)
        {
            this.Id = id;
            this.CreationTime = creationTime;
            this.DueDate = dueDate;
            this.Title = title;
            this.Description = description;
            this.emailAssignee = emailAssignee;
        }
    }
}
