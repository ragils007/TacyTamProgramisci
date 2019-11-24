using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Db.Pgsql.Raw
{
    public class fields
    {
        public long id { get; set; }
        public string fieldname { get; set; }
    }
}
