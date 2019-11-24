using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeetRootDoctorPage.Models
{
    public class HistoriaViewModel
    {
        public int id { get; set; }
        public string aktualnaNazwaKamery { get; set; }
        public string aktualnaNazwaPola { get; set; }
        public string historycznaNazwaKamery { get; set; }
        public string historycznaNazwaPola { get; set; }
        public string urlZdjecia { get; set; }
        public string geolat { get; set; }
        public string geolon { get; set; }
    }
}