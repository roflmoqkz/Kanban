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
        string email;
        public ObservableCollection<ColumnModel> Columns { get; set; }
        void initColumns()
        {
            BoardModel b = Controller.GetBoard(email);
            this.Columns = b.Columns;
        }
        public BoardModel(BackendController controller,string email,IReadOnlyCollection<string> columnNames) : base(controller)
        {
            this.email = email;
            this.Columns = new ObservableCollection<ColumnModel>();
            for (int i = 0; i < columnNames.Count; i++)
            {
                Columns.Add(controller.GetColumn(email, i));
            }
        }
        public void RemoveColumn(int ordinal)
        {
            Controller.RemoveColumn(email,ordinal);
            initColumns();
        }
        public void addColumn(int ordinal,ColumnModel column,string name)
        {
            Controller.AddColumn(email, ordinal,name);
            initColumns();
        }
        public void moveColumnRight(int ordinal)
        {
            Controller.MoveColumnRight(email, ordinal);
            initColumns();
        }
        public void moveColumnLeft(int ordinal)
        {
            Controller.MoveColumnLeft(email, ordinal);
            initColumns();
        }
        public void advanceTask(int ordinal,TaskModel task)
        {
            Controller.AdvanceTask(email, ordinal, task.Id);
            initColumns();
        }
    }
}
