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
            this.pg.Query("INSERT INTO fields(fieldname) VALUES (:name)")
                .Bind("name", name)
                .Execute();
        }

        public void Edit(long id, string name)
        {
            this.pg.Query("UPDATE fields SET fieldname = :name WHERE id = :id")
                .Bind("name", name)
                .Bind("id", id)
                .Execute();
        }

        public void Delete(long id)
        {
            this.pg.Query("DELETE FROM fields WHERE id = :id")
                .Bind("id", id)
                .Execute();
        }
    }
}
