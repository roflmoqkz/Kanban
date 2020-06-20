using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    class BoardModel : NotifiableModelObject
    {
        string email;
        public ObservableCollection<ColumnModel> Columns { get; set; }
        public BoardModel(BackendController controller,string email,IReadOnlyCollection<string> columnNames) : base(controller)
        {
            this.email = email;
            this.Columns = new ObservableCollection<ColumnModel>();
            foreach (string s in columnNames)
            {
                Columns.Add(controller.GetColumn(email, s));
            }
            Columns.CollectionChanged += HandleChange;
        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ColumnModel y in e.OldItems)
                {
                    Controller.RemoveColumn(email, y.ordinal);
                }

            }
        }
    }
}
