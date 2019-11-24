using Db.Pgsql;
using Db.Pgsql.Factory;
using Db.Pgsql.Raw;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeetRootDoctorPage.Models
{
    public class LonLatWykr
    {
        public string lon { get; set; }
        public string lat { get; set; }
        public long is_wykryty { get; set; }
    }

    public class MapModel
    {
        public static List<LonLatWykr> GetCamerasList()
        {
            using (var pg = new Postgres())
            {
                var data = pg.Query(@"select id, lat, lon, is_wykryty from
(
    select c.id, c.geolat as lat
        , c.geolon  as lon
        , case when wykrytocolumn1 = 1 then 1 else 0 end as is_wykryty 
        , rank() over(partition by c.id order by cl.id desc) as rn
    from cameras c
    left outer join cameralog cl on cl.camera_fk = c.id and cl.field_fk = c.currentfield_fk
    order by c.id
) X
where rn = 1").Fetch();

                var ret = new List<LonLatWykr>();
                foreach (DataRow row in data.Rows)
                {
                    var item = new LonLatWykr
                    {
                        lon = Convert.ToString(row["lon"]),
                        lat = Convert.ToString(row["lat"]),
                        is_wykryty = Convert.ToInt64(row["is_wykryty"]),
                    };
                    ret.Add(item);
                }

                return ret;
            }
        }
    }
}
