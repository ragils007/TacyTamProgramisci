using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Pgsql.Factory
{
    public class fieldsFactory
    {
        private readonly Postgres pg;

        public fieldsFactory(Postgres db)
        { 
            this.pg = db; 
        }

        public void Add(string name)
        {
            this.pg.Execute($@"INSERT INTO fields(fieldname) VALUES (''{name}')");
        }

        public void Edit(long id, string name)
        {
            this.pg.Execute($@"UPDATE fields SET fieldname='{name}' WHERE id = {id}");
        }

        public void Delete(long id)
        {
            this.pg.Execute($@"DELETE FROM fields WHERE id = {id}");
        }
    }
}
