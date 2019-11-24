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
            var defaultSchema = "postgres";
            var pass = "Qwerty123";

            var connStr = $@"Server={ip};Port={port};Database={dbName};User Id={userName};Password={pass};Pooling=true;MinPoolSize=1;MaxPoolSize=5;ConnectionIdleLifetime=15;";
            this.Cnn = new NpgsqlConnection(connStr);
            this.Cnn.Open();

            //this.Execute("SET search_path TO postgres");
        }

        public int Execute(string query, params object[] args)
        {
            using (var cmd = new NpgsqlCommand(query, this.Cnn))
            {
                cmd.CommandTimeout = 10000;
                //RunningCommand = cmd;

                //if (this.trans != null)
                //{
                //    cmd.Transaction = this.trans;
                //    if (this.TransactionCommitEvery > 0 && this.transactionLineNo == this.TransactionCommitEvery)
                //    {
                //        this.CommitAndBeginTransaction();
                //        this.transactionLineNo = 0;
                //    }
                //    this.transactionLineNo++;
                //}

                //this.ProcessParameters(cmd, args);

                // this.cnn.ClientInfo = this.ClientInfo;

                int countRecord = 0;

                countRecord = cmd.ExecuteNonQuery();
                //RunningCommand = null;

                return countRecord;
            }
        }


        public DataTable Fetch(string query, params object[] args)
        {
            var myTable = new DataTable("dataTable");

            if (this.Cnn.State.ToString() == "Closed") this.Cnn.Open();
            using (var cmd = new NpgsqlCommand(query, this.Cnn))
            {
                cmd.CommandTimeout = 10000;
                //this.ProcessParameters(cmd, args);

                using (var myDataAdp = new NpgsqlDataAdapter(cmd))
                {
                    using (new NpgsqlCommandBuilder(myDataAdp))
                    {
                        try
                        {
                            myDataAdp.Fill(myTable);
                        }
                        catch (NpgsqlException er)
                        {
                            throw er;
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            //RunningCommand = null;
                        }
                    }
                }
            }
            return myTable;
        }

        public void Dispose()
        {
            this.Cnn.Close();
        }
    }
}
