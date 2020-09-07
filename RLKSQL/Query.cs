using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RKSQL
{
    public class Query
    {
        public string CommandType { get; set; }

        public string Table { get; set; }

        public IEnumerable<string> Columns { get; set; }

        public string PrintQuery()
        {
            string query = "";

            query = string.Concat(CommandType, " ", Columns," FROM ", Table);

            return query;
        }
    }
}
