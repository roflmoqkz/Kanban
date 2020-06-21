﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class TaskModel : NotifiableModelObject
    {
        int id;
        DateTime creationTime;
        DateTime dueDate;
        string title;
        string description;
        string emailAssignee;
        public string email { get; set; }
        public int ordinal { get; set; }
        public int Id { get { return id; } set { id = value; RaisePropertyChanged("Id"); } }
        public DateTime CreationTime { get { return creationTime; } set { creationTime = value; RaisePropertyChanged("CreationTime"); } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; RaisePropertyChanged("DueDate"); Controller.UpdateTaskDueDate(email, ordinal, id, value); } }
        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); Controller.UpdateTaskTitle(email, ordinal, id, value); } }
        public string Description { get { return description; } set { description = value; RaisePropertyChanged("Description"); Controller.UpdateTaskDescription(email, ordinal, id, value); } }
        public string EmailAssignee { get { return emailAssignee; } set { emailAssignee = value; RaisePropertyChanged("EmailAssignee"); Controller.AssignTask(email, ordinal, id, value); } }
        public TaskModel(BackendController controller,string email,int ordinal, int id, DateTime creationTime, DateTime dueDate, string title, string description, string emailAssignee) : base(controller)
        {
            this.email = email;
            this.ordinal = ordinal;
            this.Id = id;
            this.CreationTime = creationTime;
            this.DueDate = dueDate;
            this.Title = title;
            this.Description = description;
            this.emailAssignee = emailAssignee;
        }
    }
}
