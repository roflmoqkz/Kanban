using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic;
using Presentation.View;

namespace Presentation.ViewModel
{
    class TaskViewModel : NotifiableObject
    {
        BackendController controller;
        UserModel user;
        TaskModel task;
        string buttonAction = "Apply";
        string email;
        string title;
        string description;
        DateTime creationTime;
        DateTime dueDate;
        Visibility taskVisible;
        public string ButtonAction { get { return buttonAction; } set { buttonAction = value; RaisePropertyChanged("ButtonAction"); } }
        public string Email { get { return email; } set { email = value; RaisePropertyChanged("Email"); } }
        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); } }
        public string Description { get { return description; } set { description = value; RaisePropertyChanged("Description"); } }
        public DateTime CreationTime { get { return creationTime; } set { creationTime = value; RaisePropertyChanged("CreationTime"); } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; RaisePropertyChanged("DueDate"); } }
        public Visibility TaskVisible { get { return taskVisible; } set { taskVisible = value; RaisePropertyChanged("TaskVisible"); } }
        public TaskViewModel(TaskModel task, UserModel user)
        {
            controller = user.Controller;
            this.task = task;
            this.user = user;
            if (task == null)
            {
                buttonAction = "Create";
                email = user.Email;
                title = "Task Title";
                description = "Task Descripion";
                creationTime = DateTime.Now;
                dueDate = DateTime.Now + TimeSpan.FromDays(1);
                taskVisible = Visibility.Hidden;
            }
            else
            {
                email = task.EmailAssignee;
                title = task.Title;
                description = task.Description;
                creationTime = task.CreationTime;
                dueDate = task.DueDate;
                taskVisible = Visibility.Visible;
            }
        }
        public bool button()
        {
            if (task != null && user.Email != task.EmailAssignee)
                return true;
            try
            {
                if (task == null)
                {
                    controller.AddTask(email, title, description, dueDate);
                }
                else
                {
                    controller.UpdateTaskTitle(email, task.ordinal, task.Id, title);
                    controller.UpdateTaskDescription(email, task.ordinal, task.Id, description);
                    controller.UpdateTaskDueDate(email, task.ordinal, task.Id, dueDate);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }
        }
        public void assignTask()
        {
            DialogBox db = new DialogBox("Select the email you want to assign to");
            db.ShowDialog();
            if (db.result == null)
                return;
            try
            {
                controller.AssignTask(email, task.ordinal, task.Id, db.result);
                Email = db.result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
    }
}
