using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class BoardModel : NotifiableModelObject
    {
        public string Email { get; set; }
        public ObservableCollection<ColumnModel> Columns { get; set; }
        void initColumns()
        {
            BoardModel b = Controller.GetBoard(Email);
            this.Columns = b.Columns;
        }
        public BoardModel(BackendController controller,string email,IReadOnlyCollection<string> columnNames) : base(controller)
        {
            this.Email = email;
            this.Columns = new ObservableCollection<ColumnModel>();
            for (int i = 0; i < columnNames.Count; i++)
            {
                Columns.Add(controller.GetColumn(email, i));
            }
        }
        public void refresh()
        {
            this.Columns.Clear();
            BoardModel b = Controller.GetBoard(Email);
            for (int i = 0; i < b.Columns.Count; i++)
            {
                Columns.Add(Controller.GetColumn(Email, i));
            }
        }
        public void RemoveColumn(int ordinal)
        {
            Controller.RemoveColumn(Email,ordinal);
            initColumns();
        }
        public void addColumn(int ordinal,ColumnModel column,string name)
        {
            Controller.AddColumn(Email, ordinal,name);
            initColumns();
        }
        public void moveColumnRight(int ordinal)
        {
            Controller.MoveColumnRight(Email, ordinal);
            initColumns();
        }
        public void moveColumnLeft(int ordinal)
        {
            Controller.MoveColumnLeft(Email, ordinal);
            initColumns();
        }
        public void advanceTask(int ordinal,TaskModel task)
        {
            Controller.AdvanceTask(Email, ordinal, task.Id);
            initColumns();
        }
        public TaskModel findTask(int id)
        {
            foreach (ColumnModel column in Columns)
            {
                foreach (TaskModel task in column.Tasks)
                {
                    if (task.Id == id)
                        return task;
                }
            }
            return null;
        }
    }
}
