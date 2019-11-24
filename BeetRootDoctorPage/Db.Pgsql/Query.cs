using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Db.Pgsql
{
    public class Query
    {
        private readonly Postgres Pg;
        public readonly string QueryText;
        private Dictionary<string, object> Params = new Dictionary<string, object>();

        public Query(Postgres db, string text)
        {
            this.Pg = db;
            this.QueryText = text;
        }

        public Query Bind<T>(string paramName, T value)
        {
            if (this.Params.ContainsKey(paramName)) this.Params[paramName] = value;
            else this.Params.Add(paramName, value);

            return this;
        }

        public DataTable Fetch()
        {
            var myTable = new DataTable("dataTable");

            if (this.Pg.Cnn.State.ToString() == "Closed") this.Pg.Cnn.Open();
            using (var cmd = new NpgsqlCommand(this.QueryText, this.Pg.Cnn))
            {
                cmd.CommandTimeout = 10000;
                this.ProcessParameters(cmd);

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

        private void ProcessParameters(NpgsqlCommand cmd)
        {
            if (this.Params.Count == 0) return;

            foreach (var dict in this.Params)
            {
                // Parametry InputOutput są już dodane do cmd
                if (cmd.Parameters.Contains(dict.Key)) (cmd.Parameters[dict.Key]).Value = dict.Value;
                else
                {
                    var param = new NpgsqlParameter(dict.Key, GetPgsqlDbType(dict.Value)) { Value = dict.Value };
                    cmd.Parameters.Add(param);
                }
            }
        }

        public static DbType GetPgsqlDbType(object o)
        {
            var type = o?.GetType();

            if (type?.IsArray ?? false == true)
            {
                var ienum = (IEnumerable)o;
                var enumerator = ienum.GetEnumerator();
                enumerator.Reset();
                enumerator.MoveNext();
                var obj = enumerator.Current;
                var elementType = obj?.GetType();

                if (elementType == typeof(string)) return DbType.String;
                if (elementType == typeof(DateTime)) return DbType.Date;
                if (elementType == typeof(long)) return DbType.Int64;
                if (elementType == typeof(int)) return DbType.Int32;
                if (elementType == typeof(short)) return DbType.Int16;
                //if (elementType == typeof(byte)) return DbType.Byte;
                if (elementType == typeof(byte[])) return DbType.Binary;
                if (elementType == typeof(decimal)) return DbType.Decimal;
                if (elementType == typeof(float)) return DbType.Single;
                if (elementType == typeof(double)) return DbType.Double;
                if (elementType == typeof(bool)) return DbType.Int32;
            }

            if (o is string) return DbType.String;
            if (o is DateTime) return DbType.Date;
            if (o is long) return DbType.Int64;
            if (o is int) return DbType.Int32;
            if (o is short) return DbType.Int16;
            if (o is byte) return DbType.Byte;
            if (o is decimal) return DbType.Decimal;
            if (o is float) return DbType.Single;
            if (o is double) return DbType.Double;
            if (o is byte[]) return DbType.Binary;
            return DbType.String;
        }

        public int Execute()
        {
            using (var cmd = new NpgsqlCommand(this.QueryText, this.Pg.Cnn))
            {
                cmd.CommandTimeout = 10000;
                this.ProcessParameters(cmd);

                int countRecord = 0;

                countRecord = cmd.ExecuteNonQuery();

                return countRecord;
            }
        }

    }
}
