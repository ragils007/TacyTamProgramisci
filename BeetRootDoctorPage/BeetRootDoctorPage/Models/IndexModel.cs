using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeetRootDoctorPage.Models
{
    public class IndexModel
    {
        public static string idKameryWyswietlanej;

        public static void ZmienPodgladKamery(string idKamery)
        {
            idKameryWyswietlanej = idKamery;
        }
    }
}
