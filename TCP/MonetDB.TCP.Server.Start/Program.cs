using System;
using System.Threading;
using MonetDB.Enums.Logging;
using MonetDB.Helpers.Logging;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;

namespace MonetDB.TCP.Server.Start
{
    internal static class Program
    {
        private readonly static Mutex Mutex = new Mutex(true, @"MonetDB.TCP.Server");

        internal static void Main(string[] args)
        {
            if (Mutex.WaitOne(TimeSpan.Zero, true))
            {
                var server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(MonetSettings.Tcp.Port));

                server.AddService<IMonetDbtcpService, MonetDbtcpService>(new MonetDbtcpService());

                server.ClientConnected += delegate (object sender, ServiceClientEventArgs eventArgs)
                {
                    LoggerHelper.Write(LoggerOption.Info, @"Client#{0} connected", eventArgs.Client.ClientId);
                };
                server.ClientDisconnected += delegate (object sender, ServiceClientEventArgs eventArgs)
                {
                    LoggerHelper.Write(LoggerOption.Warning, @"Client#{0} disconnected", eventArgs.Client.ClientId);
                };

                //Start server
                server.Start();

                //Wait user to stop server by pressing Enter
                LoggerHelper.Write(LoggerOption.Info, @"MonetDB Api Server started successfully.");

                LoggerHelper.Write(LoggerOption.Warning, @"Press enter to stop...");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    //remove server
                    server.RemoveService<IMonetDbtcpService>();

                    //Stop server
                    server.Stop();
                }

                Mutex.ReleaseMutex();
            }
            else
            {
                LoggerHelper.Write(LoggerOption.Warning, @"Only one instance at a time");

                Console.ReadLine();

                Environment.Exit(Environment.ExitCode);
            }

        }
    }
}
