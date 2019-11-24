using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeetRootDoctorPage.Models
{
    public class HistoriaModel
    {
        public static List<HistoriaViewModel> GetHistoriaZdarzen()
        {
            List<HistoriaViewModel> historiaZdarzen = new List<HistoriaViewModel>();
            /*
              //#TODO Zapytanie o zdarzenia
              foreach (var rawWizyta in raw)
              {
                  listaWizyt.Add(new WizytyViewModel()
                  {
                      dataRozpoczeciaWizyty = rawWizyta.DATA_OD,
                      dataZakonczeniaWizyty = rawWizyta.DATA_DO,
                      czasWizyty = rawWizyta.DATA_DO != null ? rawWizyta.DATA_DO - rawWizyta.DATA_OD : null,
                      kodKrotkiKlienta = wybranyKlient.KodKrotki,
                      idPracownika = rawWizyta.PRACOWNIK_W_FK,
                      notatka = rawWizyta.NOTATKA
                  });
              }
              */

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
