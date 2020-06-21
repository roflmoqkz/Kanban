using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Presentation.Model
{
    public class ColumnModel : NotifiableModelObject
    {
        string name;
        int limit;
        int ordinal;
        ObservableCollection<TaskModel> Tasks;
        string email;
        public string Name { get { return name; } set {
                name = value;
                RaisePropertyChanged("Name");
                Controller.ChangeColumnName(email, ordinal, value);
            } }
        public int Limit { get { return limit; } set {
                limit = value;
                RaisePropertyChanged("Limit");
                Controller.LimitColumnTasks(email, ordinal, limit);
            } }
        public int Ordinal { get { return ordinal; } set {
                ordinal = value;
                RaisePropertyChanged("Ordinal");
                foreach (TaskModel t in Tasks)
                {
                    t.ordinal = ordinal;
                }
            } }
        public ColumnModel(BackendController controller,string name, int limit,int ordinal, IReadOnlyCollection<Task> tasks,string email) : base(controller)
        {
            this.Tasks = new ObservableCollection<TaskModel>();
            this.name = name;
            this.limit = limit;
            this.ordinal = ordinal;
            this.email = email;
            foreach (Task t in tasks)
            {
                Tasks.Add(new TaskModel(this.Controller,email,ordinal,t.Id,t.CreationTime,t.DueDate,t.Title,t.Description,t.emailAssignee));
            }
        }
        void initTasks()
        {
            ColumnModel c = Controller.GetColumn(email, ordinal);
            Tasks = c.Tasks;
        }
        public void addTask(string title,string description,DateTime dueDate)
        {
            Controller.AddTask(email, title, description, dueDate);
        }
        public void deleteTask(TaskModel task)
        {
            Controller.DeleteTask(email, ordinal, task.Id);
        }
    }
}
