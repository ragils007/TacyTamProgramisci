using Db.Pgsql.Raw;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Db.Pgsql.Factory
{
    public class camerasFactory
    {
        private readonly Postgres pg;

        public camerasFactory(Postgres db)
        {
            this.pg = db;
        }

        public void Add(long currentFieldfk, string name, string geoLat, string geoLon)
        {
            pg.Query($@"INSERT INTO cameras(currentfield_fk, name, geolat, geolon) VALUES (:fieldId, :name, :lat, :lon)")
                .Bind("fieldId", currentFieldfk)
                .Bind("name", name)
                .Bind("lat", geoLat)
                .Bind("lon", geoLon)
                .Execute();
        }

        public void Edit(long id, long currentFieldfk, string name, string geoLat, string geoLon)
        {
            pg.Query($@"UPDATE cameras SET currentfield_fk=:fieldId, name=:name, geolat=:lat, geolon=:lon WHERE id = :id")
                .Bind("fieldId", currentFieldfk)
                .Bind("name", name)
                .Bind("lat", geoLat)
                .Bind("lon", geoLon)
                .Bind("id", id)
                .Execute();
        }

        public void Delete(long id)
        {
            pg.Query($@"DELETE FROM cameras WHERE id = :id")
                .Bind("id", id)
                .Execute();
        }

        public List<cameras> GetList()
        {
            var data = pg.Query("SELECT * FROM cameras").Fetch();

            var ret = new List<cameras>();
            foreach(DataRow dr in data.Rows)
            {
                var item = new cameras
                {
                    id = Convert.ToInt64(dr["id"]),
                    currentfield_fk = Convert.ToInt64(dr["currentfield_fk"]),
                    name = Convert.ToString(dr["name"]),
                    geolat = Convert.ToString(dr["geolat"]),
                    geolon = Convert.ToString(dr["geolon"])
                };

                ret.Add(item);
            }

            return ret;
        }
    }
}
