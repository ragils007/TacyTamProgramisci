﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Db.Pgsql.Raw
{
    public class cameras
    {
        public long id { get; set; }
        public long currentfield_fk { get; set; }
        public string name { get; set; }
        public string geolat { get; set; }
        public string geolon { get; set; }
    }
}
