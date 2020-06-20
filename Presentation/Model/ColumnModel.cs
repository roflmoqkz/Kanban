using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Presentation.Model
{
    class ColumnModel : NotifiableModelObject
    {
        string name;
        int limit;
        ObservableCollection<TaskModel> Tasks;
        public string Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }
        public int Limit { get { return limit; } set { limit = value; RaisePropertyChanged("Limit"); } }
        public ColumnModel(BackendController controller,string name, int limit, IReadOnlyCollection<Task> tasks) : base(controller)
        {
            Name = name;
            Limit = limit;
            foreach (Task t in tasks)
            {
                Tasks.Add(new TaskModel());
            }
        }
    }
}
