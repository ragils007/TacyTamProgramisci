using Db.Pgsql.Factory;
using System;

namespace Db.Pgsql.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Postgres();
            var ret = db.Fetch("SELECT * FROM cameras");

            var fac = new camerasFactory(db);
            fac.Add(1, "Nowa kamera", "12.34", "34.56");
            fac.Edit(20, 1, "Zmiana!", "23.322", "123.45");
            fac.Delete(25);
        }
    }
}
