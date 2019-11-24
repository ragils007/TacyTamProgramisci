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
            pg.Execute($@"INSERT INTO cameras(currentfield_fk, name, geolat, geolon) VALUES ({currentFieldfk}, '{name}', '{geoLat}', '{geoLon}')");
        }

        public void Edit(long id, long currentFieldfk, string name, string geoLat, string geoLon)
        {
            pg.Execute($@"UPDATE cameras SET currentfield_fk={currentFieldfk}, name='{name}', geolat='{geoLat}', geolon='{geoLon}' WHERE id = {id}");
        }

        public void Delete(long id)
        {
            pg.Execute($@"DELETE FROM cameras WHERE id = {id}");
        }

        public cameras GetBy_Id(long id)
        {
            var data = this.pg.Fetch($"SELECT * FROM cameras WHERE id={id}");
            if (data.Rows.Count == 0) return null;

            var cam = this.GetItemFromDatarow(data.Rows[0]);
            return cam;
        }

        private cameras GetItemFromDatarow(DataRow dr)
        {
            var item = new cameras
            {
                id = Convert.ToInt64(dr["id"]),
                field_fk = Convert.ToInt64(dr["field_fk"]),
                name = Convert.ToString(dr["name"]),
                geolat = Convert.ToString(dr["geolat"]),
                geolon = Convert.ToString(dr["geolon"]),
            };

            return item;
        }
    }
}
