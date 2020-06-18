using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DalObject<T> where T : DalObject<T>
    {
        protected DalController DalC;
        public DalObject ()
        {
            DalC = new DalController("data.db");
        }
        public virtual void add()
        {
            
        }
    }
}
