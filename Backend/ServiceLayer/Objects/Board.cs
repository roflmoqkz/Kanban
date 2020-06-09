﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Board
    {
        public readonly IReadOnlyCollection<string> ColumnsNames;
        internal Board(IReadOnlyCollection<string> columnsNames)
        {
            this.ColumnsNames = columnsNames;
        }

        internal Board(BusinessLayer.BoardPackage.Board b)
        {
            List<string> cn = b.getColumnNames();
            this.ColumnsNames = cn;
        }
    }
}
