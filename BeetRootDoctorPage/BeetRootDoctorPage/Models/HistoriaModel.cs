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
            var cameralogDt = db.Query("SELECT cl.*, c.name as nameFromCameras, f.fieldname as nameFromField  FROM cameralog cl join cameras c on c.id = cl.camera_fk join fields f on f.id = cl.field_fk ");

            foreach (DataRow dr in cameralogDt.Fetch().Rows)
            {
                HistoriaViewModel newObj = new HistoriaViewModel();
                newObj.id = Convert.ToInt32(dr["id"]);
                newObj.aktualnaNazwaKamery = Convert.ToString(dr["namefromcameras"]);
                newObj.nazwaPola = Convert.ToString(dr["namefromfield"]);
                newObj.historycznaNazwaKamery = Convert.ToString(dr["name"]);
                newObj.urlZdjecia = Convert.ToString(dr["url"]);
                newObj.geolat = Convert.ToString(dr["geolat"]);
                newObj.geolon = Convert.ToString(dr["geolon"]);
                newObj.dataZdarzenia = Convert.ToString(dr["log_date"]);
                historiaZdarzen.Add(newObj);
            }
            return historiaZdarzen;
        }
    }
}
