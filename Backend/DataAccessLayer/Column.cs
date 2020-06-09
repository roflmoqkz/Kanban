using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Column : DalObject<Column>
    {
        public long id { get; private set; }
        public string name { get;}
        public int limit { get; private set; }
        public string email { get; }
        public int ordinal { get; private set; }
        public Column(string name,int limit,string email,int ordinal) : base()
        {
            this.name = name;
            this.limit = limit;
            this.email = email;
            this.ordinal = ordinal;
        }
        public Column(long id, string name, int limit, string email, int ordinal) : base()
        {
            this.id = id;
            this.name = name;
            this.limit = limit;
            this.email = email;
            this.ordinal = ordinal;
        }
        public void Add()
        {
            DalC.query($"INSERT INTO columns (name,collimit,email,ordinal) VALUES ('{name}',{limit},'{email}',{ordinal})");
            List<object[]> rows = DalC.select($"SELECT id FROM columns WHERE email = '{email}' AND ordinal = {ordinal} ORDER BY id", 1);
            id = (long)rows[rows.Count - 1][0];
        }
        public void UpdateLimit(int limit)
        {
            this.limit = limit;
            DalC.query($"UPDATE columns SET limit={limit} WHERE id={id}");
        }
        public void UpdateOrdinal(int ordinal)
        {
            this.ordinal = ordinal;
            DalC.query($"UPDATE columns SET ordinal={ordinal} WHERE id={id}");
        }
        public void Delete()
        {
            DalC.query($"DELETE FROM columns WHERE id={id}");
            DalC.query($"DELETE FROM tasks WHERE column={id}");
        }
    }
}
