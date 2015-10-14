
        private static async void BinaryExecutor(string schema, string table, string fileName)
        {
            var query = new StringBuilder();
            query.AppendLine("START TRANSACTION;");
            query.AppendFormat("COPY INTO \"{0}\".\"{1}\" FROM '{2}' USING DELIMITERS ',','\\n','\"';", schema,
                table, fileName);
            query.AppendLine("COMMIT;");

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            using (var conn = new OdbcConnection(MonetConnectionString))
            {
                conn.Open();

                using (var comm = new OdbcCommand(query.ToString(), conn))
                {
                    comm.CommandTimeout = MonetSettings.COMMAND_TIME_OUT;

                    comm.ExecuteNonQuery();
                }

                conn.Close();
            }

            stopWatch.Stop();

            Console.WriteLine("Elapsed time:{0:g}", stopWatch.Elapsed);

            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        private static void SaveAsCsv(object[][] matrix, string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                var data = String.Join(Environment.NewLine, matrix.Select(s => String.Join(",", s.Select(a =>
                {
                    if (a == DBNull.Value)
                    {
                        return "NULL";
                    }

                    if (a is DateTime)
                    {
                        return String.Format("{0:yyyy-MM-dd hh:mm:ss}", (DateTime)a);
                    }

                    if (a is string)
                    {
                        return String.Format("\"{0}\"", a);
                    }

                    return a;
                }))));

                file.Write(data);
                file.Write(Environment.NewLine);
            }
        }

        private static void BinaryCopy()
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            var secondValues = new object[BatchSize][];

            for (var index = 0; index < secondValues.Length; ++index)
                secondValues[index] = new object[_reader.FieldCount];

            long size = 0, rowCounter = 0;

            while (_reader.Read())
            {
                _reader.GetValues(secondValues[size++]);

                if (size % BatchSize == 0 && size > 0)
                {

                    var secondFilePath = Path.Combine(Environment.CurrentDirectory,
                        String.Format("{0}.csv", Guid.NewGuid()));

                    SaveAsCsv(secondValues, secondFilePath);
                    BinaryExecutor("dbo", "sales_fact_1997_100M", secondFilePath);

                    size = 0;
                }

                rowCounter++;
            }

            var lastValues = new object[size][];

            for (var index = 0; index < size; ++index)
                lastValues[index] = secondValues[index];

            var lastFilePath = Path.Combine(Environment.CurrentDirectory, String.Format("{0}.csv", Guid.NewGuid()));

            SaveAsCsv(lastValues, lastFilePath);
            BinaryExecutor("dbo", "sales_fact_1997_100M", lastFilePath);

            stopWatch.Stop();

            Console.WriteLine("Upload done({0:g})!", stopWatch.Elapsed);
        }
