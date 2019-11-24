using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Pgsql.Factory
{
    public class cameralogFactory
    {
        private readonly Postgres Pg;

        public cameralogFactory(Postgres db)
        {
            this.Pg = db;
        }

        public void Add(long camera_fk, long field_fk, string url, string geolat, string geolon, string name)
        {
            this.Pg.Query("INSERT INTO cameralog(camera_fk, field_fk, url, geolat, geolon, name) VALUES (:camera_fk, :field_fk, :url, :geolat, :geolon, :name)")
                .Bind("camera_fk", camera_fk)
                .Bind("field_fk", field_fk)
                .Bind("url", url)
                .Bind("geolat", geolat)
                .Bind("geolon", geolon)
                .Bind("name", name)
                .Execute();
        }
    }
}
