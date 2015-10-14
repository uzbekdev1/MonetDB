using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DM.TCP.Framework.Communication.ScsServices.Service;
using MonetDB.Configurations;
using MonetDB.DataProvider;
using MonetDB.Driver.Data;
using MonetDB.Driver.Helpers;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;
using MonetDB.Helpers;
using MonetDB.Helpers.Diagnostic;
using MonetDB.Helpers.Logging;
using MonetDB.TCP.Enums;
using MonetDB.TCP.Models;
using MonetDB.Models;
using MonetDB.Driver.Exceptions;
using MonetDB.Helpers.Win32;

namespace MonetDB.TCP.Server
{
    public class MonetDbtcpService : ScsService, IMonetDbtcpService
    {
        private const string MSERVER_PROCESS_NAME = "mserver5";
        private const string STARTUP_PROCESS_NAME = "MonetDB.TCP.Startup.exe";
        private const string SERVER_OPTIONS_FILE = "serveroptions.bin";
        private readonly MonetConfiguration _serverConfiguration;

        private static string ToTitle(string name)
        {
            return $"MonetDB {name}";
        }

        public MonetDbtcpService()
        {
            _serverConfiguration = MonetConfigurationHelper.GetConfiguration(Path.Combine(IoHelper.CurrentRoot, MonetSettings.MonetConfigurationFile));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Database already exists</exception>
        /// <exception cref="ArgumentNullException"><paramref name="database"/> is <see langword="null" />.</exception>
        public bool CreateDatabase(string database)
        {

            //to check in processes
            var dataDir = Path.Combine(_serverConfiguration.DbFarmDir, database);
            var model = new MonetServerOptionsModel();

            if (DiagnosticHelper.IsRunningApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir)))
            {
                throw new MonetDbException("Database already created");
            }

            try
            {
                //database   
                var serverFileName = string.Format("{0}\\bin\\mserver5.exe", _serverConfiguration.DbInstallerDir);
                var serverFileArgs = string.Format("--set \"prefix={0}\" " +
                                                   "--set \"exec_prefix={0}\" " +
                                                   "--set \"mapi_port={1}\" " +
                                                   "--dbpath=\"{2}\"  " +
                                                   "--set \"gdk_nr_threads={3}\" " +
                                                   "--set \"max_clients={4}\"",
                    _serverConfiguration.DbInstallerDir,
                    model.Port,
                    dataDir,
                    model.Threads,
                    model.MaxClients);

                model.ServerArguments = serverFileArgs;
                model.ServerFileName = serverFileName;

                //environment 
                EnvironmentHelper.SetVariables("PATH", string.Format("{0}\\bin;{0}\\lib;{0}\\lib\\MonetDB5;", _serverConfiguration.DbInstallerDir));
                 
                if (DiagnosticHelper.AutoLaunchApplicationToSleep(serverFileName, serverFileArgs))
                {

                    //server options
                    BinaryFormatterHelper.Serialize(model, Path.Combine(dataDir, SERVER_OPTIONS_FILE));

                    //startup
                    ShellHelper.AddToStartup(ToTitle(database), Path.Combine(IoHelper.CurrentRoot, STARTUP_PROCESS_NAME), $"\"{Path.Combine(dataDir, SERVER_OPTIONS_FILE)}\"");

                    //logging
                    LoggerHelper.Write(LoggerOption.Info, "Client#{0} -> Created '{1}' database", CurrentClient.ClientId, database);

                    return true;
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                throw;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DropDatabase(string database)
        {
            try
            {
                //to check in processes
                var dataDir = Path.Combine(_serverConfiguration.DbFarmDir, database);

                if (!DiagnosticHelper.IsRunningApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir)) &&
                    !Directory.Exists(dataDir))
                {
                    throw new MonetDbException("Database not exists");
                }

                //database
                if (DiagnosticHelper.KillProcessApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir)) ||
                    Directory.Exists(dataDir))
                {
                    //delete
                    Directory.Delete(dataDir, true);

                    //startup
                    ShellHelper.RemoveFromStartup(ToTitle(database));

                    //logging
                    LoggerHelper.Write(LoggerOption.Warning, "Client#{0} -> Droped '{1}' database", CurrentClient.ClientId, database);

                    return true;
                }

            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                throw;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool LoadDatabase(string database)
        {

            //to check in processes
            var dataDir = Path.Combine(_serverConfiguration.DbFarmDir, database);

            if (DiagnosticHelper.IsRunningApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir)))
            {
                throw new MonetDbException("Database already loaded");
            }

            try
            {
                //database   
                var model = BinaryFormatterHelper.Deserialize<MonetServerOptionsModel>(Path.Combine(dataDir, SERVER_OPTIONS_FILE));
                var serverFileName = string.Format("{0}\\bin\\mserver5.exe", _serverConfiguration.DbInstallerDir);
                var serverFileArgs = string.Format("--set \"prefix={0}\" " +
                                                   "--set \"exec_prefix={0}\" " +
                                                   "--set \"mapi_port={1}\" " +
                                                   "--dbpath=\"{2}\"  " +
                                                   "--set \"gdk_nr_threads={3}\" " +
                                                   "--set \"max_clients={4}\"",
                    _serverConfiguration.DbInstallerDir,
                    model.Port,
                    dataDir,
                    model.Threads,
                    model.MaxClients);

                //environment 
                EnvironmentHelper.SetVariables("PATH", string.Format("{0}\\bin;{0}\\lib;{0}\\lib\\MonetDB5;", _serverConfiguration.DbInstallerDir));


                if (DiagnosticHelper.AutoLaunchApplicationToSleep(serverFileName, serverFileArgs))
                {
                    //logging
                    LoggerHelper.Write(LoggerOption.Info, "Client#{0} -> Loaded '{1}' database", CurrentClient.ClientId, database);

                    return true;
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                throw;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool KillDatabase(string database)
        {
            try
            {
                //to check in processes
                var dataDir = Path.Combine(_serverConfiguration.DbFarmDir, database);

                if (!DiagnosticHelper.IsRunningApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir)) &&
                    !Directory.Exists(dataDir))
                {
                    throw new MonetDbException("Database not exists");
                }

                //database
                if (DiagnosticHelper.KillProcessApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir)))
                {
                    //logging
                    LoggerHelper.Write(LoggerOption.Warning, "Client#{0} -> Droped '{1}' database", CurrentClient.ClientId, database);

                    return true;
                }

            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                throw;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DatabaseStatus StatusDatabase(string database)
        {
            try
            {
                var dataDir = Path.Combine(_serverConfiguration.DbFarmDir, database);

                if (Directory.Exists(dataDir))
                {
                    LoggerHelper.Write(LoggerOption.Warning, "Client#{0} -> Checked '{1}' database",
                           CurrentClient.ClientId, database);

                    return DiagnosticHelper.IsRunningApplication(MSERVER_PROCESS_NAME, String.Format("--dbpath=\"{0}\"", dataDir))
                        ? DatabaseStatus.Started
                        : DatabaseStatus.Stoped;
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                throw;
            }

            return DatabaseStatus.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MonetDatabaseInfo> GetDatabases()
        {

            var dataDir = Path.Combine(_serverConfiguration.DbFarmDir);
            var databases = Directory.GetDirectories(dataDir).Select(s => new DirectoryInfo(s));
            var items = new List<MonetDatabaseInfo>();

            foreach (var database in databases)
            {
                var dbDir = Path.Combine(dataDir, database.Name);
                var item = new MonetDatabaseInfo
                {
                    Status = StatusDatabase(database.Name),
                    Database = database.Name,
                    Size = IoHelper.DirSize(database)
                };

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns></returns>
        public bool BulkCopy(MonetBulkCopyModel model)
        {
            try
            {

                var connectionString = model.ToString();
                var stopWatch = new Stopwatch();
                var data = model.Rows.Lz4Decompress().ToStrings();
                var fileName = IoHelper.GetTmpRandomFileName(_serverConfiguration.TempDir);

                stopWatch.Start();

                MonetDbHelper.DumpBinary(data, fileName);

                if (IoHelper.IsNotEmpty(fileName))
                {
                    MonetDbHelper.BinaryCopy(connectionString, model.SchemaName, model.TableName, data.Length, fileName);
                }

                IoHelper.RemoveFile(fileName);

                stopWatch.Stop();

                LoggerHelper.Write(LoggerOption.Info, "Client#{0} -> Loaded '{1}.{2}.{3}'-{4} rows({5} ms)", CurrentClient.ClientId, model.Database, model.SchemaName, model.TableName, data.Length, stopWatch.ElapsedMilliseconds);

                return true;
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public int GetPort(string database)
        {
            var dataDir = Path.Combine(_serverConfiguration.DbFarmDir, database);
            var model = BinaryFormatterHelper.Deserialize<MonetServerOptionsModel>(Path.Combine(dataDir, SERVER_OPTIONS_FILE));

            return model?.Port ?? 0;
        }

    }
}
