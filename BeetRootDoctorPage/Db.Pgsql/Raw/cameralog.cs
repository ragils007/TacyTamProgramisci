using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Db.Pgsql.Raw
{
    public class cameralog
    {
        public long id { get; set; }
        public long camera_fk { get; set; }
        public long field_fk { get; set; }
        public string url { get; set; }
        public string geolat { get; set; }
        public string geolon { get; set; }
        public string name { get; set; }
        public DateTime log_date { get; }
        public int wykrytocolumn1 { get; }
    }
}
