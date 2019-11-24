using Npgsql;
using System;
using System.Data;

namespace Db.Pgsql
{
    public class Postgres : IDisposable
    {
        public readonly NpgsqlConnection Cnn;

        public Postgres()
        {
            var ip = "pgchwasty.postgres.database.azure.com";
            var port = 5432;
            var dbName = "postgres";
            var userName = "admin123@pgchwasty";
            var pass = "Qwerty123";

            var connStr = $@"Server={ip};Port={port};Database={dbName};User Id={userName};Password={pass};Pooling=true;MinPoolSize=1;MaxPoolSize=5;ConnectionIdleLifetime=15;";
            this.Cnn = new NpgsqlConnection(connStr);
            this.Cnn.Open();
        }

        public Query Query(string queryText)
        {
            return new Query(this, queryText);
        }

        public void Dispose()
        {
            this.Cnn.Close();
        }
    }
}
