using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Driver.Helpers;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;
using MonetDB.Helpers.Logging;

namespace MonetDB.Driver.Demo
{
    internal static class DataProviderTest
    {

        internal static string ConnectionString { get; set; }

        internal static void CreateSchemaTest()
        {
            var query = new StringBuilder();

            query.Append("create schema test;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void DropSchemaTest()
        {
            var query = new StringBuilder();

            query.Append("drop schema test;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void CreateTableTest()
        {
            var query = new StringBuilder();

            query.Append("create table test.users(\"id\" int,\"full_name\" text,\"birth_date\" date);");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void DropTableTest()
        {
            var query = new StringBuilder();

            query.Append("DROP TABLE test.users;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void InsertTableTest()
        {
            var query = new StringBuilder();

            for (var i = 0; i < 100; i++)
            {
                query.AppendFormat("insert into test.users(\"id\",\"full_name\",\"birth_date\") values({0},'Elyor Latipov#{0}',NOW());\n", i + 1);
            }

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void UpdateTableTest()
        {
            var query = new StringBuilder();


            query.AppendFormat("update test.users set \"full_name\"='Elyor Latipov' where id>5;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void DeleteTableTest()
        {
            var query = new StringBuilder();


            query.AppendFormat("delete from test.users where id>5;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();
                MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void AggregationTest()
        {
            var query = new StringBuilder();


            query.AppendFormat("select count(*) from test.users;");

            try
            {
                var stopWatch = new Stopwatch();
                var count = 0;

                stopWatch.Start();
                count = MonetDbHelper.ExecuteScalar(ConnectionString, query.ToString()).To<int>();
                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.None, "Total count:{0}", count);
                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void SelectTest()
        {
            var query = new StringBuilder();


            query.AppendFormat("select * from dbo.employee limit 10 offset 20;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();

                var reader = MonetDbHelper.ExecuteReader(ConnectionString, query.ToString());

                LoggerHelper.Write(LoggerOption.None, "------------------------------------------");

                while (reader.Read())
                {
                    LoggerHelper.Write(LoggerOption.None, "\t{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
                }

                LoggerHelper.Write(LoggerOption.None, "------------------------------------------");

                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

        internal static void BulkCopyTest()
        {
            long totalElapsed = 0;
            var tasks = new Task[100];

            try
            {
                for (var i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = Task.Factory.StartNew<long>(() =>
                    {
                        var stopWatch = new Stopwatch();
                        stopWatch.Start();
                        var query = new StringBuilder();

                        query.Append("COPY  INTO test FROM  'C:\\tmp\\test.txt'  USING  DELIMITERS ',','\n','\"';");
                        MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());

                        stopWatch.Stop();

                        Console.WriteLine("\t Elapsed 10 k rows({0} ms)", stopWatch.ElapsedMilliseconds);

                        return stopWatch.ElapsedMilliseconds;
                    }).ContinueWith(delegate (Task<long> task)
                    {
                        totalElapsed += task.Result;
                    });
                }

                Task.WaitAll(tasks);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("10.000 rows loaded({0} ms)", totalElapsed);
        }

        internal static void BatchInsertTest()
        {
            var tasks = new Task[1000];
            long totalElapsed = 0;

            for (var i = 0; i < tasks.Length; i++)
            {
                var query = new StringBuilder();

                for (var j = i; j < i + 10; j++)
                {
                    query.AppendFormat("insert into test.users(\"id\",\"full_name\",\"birth_date\") values({0},'Elyor Latipov#{0}',NOW());",
                        j + 1);
                }

                var stopWatch = new Stopwatch();

                try
                {

                    stopWatch.Start();

                    MonetDbHelper.ExecuteNonQuery(ConnectionString, query.ToString());

                    stopWatch.Stop();

                    Console.WriteLine("Query#{0} executed({1} ms)", i, stopWatch.ElapsedMilliseconds);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                totalElapsed += stopWatch.ElapsedMilliseconds;
            }


            Console.WriteLine("10.000 rows loaded({0} ms)", totalElapsed);
        }

        internal static void ReaderTest()
        {
            var query = new StringBuilder();


            query.AppendFormat("SELECT name FROM schemas;SELECT name FROM tables;");

            try
            {
                var stopWatch = new Stopwatch();

                stopWatch.Start();

                var reader = MonetDbHelper.ExecuteReader(ConnectionString, query.ToString());

                do
                {
                    LoggerHelper.Write(LoggerOption.None, "----------------{0}({1})-------------------", reader.GetName(0), reader.GetDataTypeName(0));
                    var items = new List<object>();

                    while (reader.Read())
                    {
                        items.Add(reader.GetValue(0));
                    }

                    LoggerHelper.Write(LoggerOption.None, String.Join(",", items));
                    LoggerHelper.Write(LoggerOption.None, "------------------------------------------");
                } while (reader.NextResult());


                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Query executed({0} ms)", stopWatch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);
            }

        }

    }
}
