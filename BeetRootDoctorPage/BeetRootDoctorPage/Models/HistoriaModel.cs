using System;
using Db.Pgsql.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Db.Pgsql;
using System.Data;

namespace BeetRootDoctorPage.Models
{
    public class HistoriaModel
    {
        public static List<HistoriaViewModel> GetHistoriaZdarzen()
        {
            List<HistoriaViewModel> historiaZdarzen = new List<HistoriaViewModel>();

            var db = new Postgres();
            var cameralogDt = db.Query("SELECT * FROM cameralog");


            foreach (DataRow dr in cameralogDt.Fetch().Rows)
            {
                HistoriaViewModel newObj = new HistoriaViewModel();
                newObj.id = Convert.ToInt32(dr["id"]);
                newObj.aktualnaNazwaKamery = Convert.ToString(dr["camera_fk"]);
                newObj.aktualnaNazwaPola = Convert.ToString(dr["field_fk"]);
                newObj.historycznaNazwaKamery = Convert.ToString(dr["name"]);
                newObj.historycznaNazwaPola = Convert.ToString(dr["field_fk"]);
                newObj.urlZdjecia = Convert.ToString(dr["url"]);
                newObj.geolat = Convert.ToString(dr["geolat"]);
                newObj.geolon = Convert.ToString(dr["geolon"]);

                historiaZdarzen.Add(newObj);
            }

            historiaZdarzen.Add(new HistoriaViewModel()
            {
                id = 1,
                aktualnaNazwaKamery = "KAMERA_1",
                aktualnaNazwaPola = "POLE_1",
                historycznaNazwaKamery = "KAMERA_A",
                historycznaNazwaPola = "POLE_A",
                urlZdjecia = "TEST_URL",
                geolat = "0000",
                geolon = "1111"
            });

            return historiaZdarzen;
        }
    }
}
