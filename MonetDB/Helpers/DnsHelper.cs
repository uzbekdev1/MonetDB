using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class DnsHelper
    {

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static int GetAvailablePort()
        {
            var port = 0;
            var listener = new TcpListener(IPAddress.Loopback, 0);

            try
            {
                listener.Start();
                port = ((IPEndPoint) listener.LocalEndpoint).Port;
                listener.Stop();
            }
            catch (Exception)
            {
                port = 50000;
            }

            return port;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="host"/> is <see langword="null" />.</exception>
        /// <exception cref="InvalidCastException">Ip address wrong</exception>
        public static string GetIpResolver(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentNullException(nameof(host));

            if (host.Equals("(local)", StringComparison.InvariantCultureIgnoreCase) ||
                host.Equals("localhost", StringComparison.InvariantCultureIgnoreCase) ||
                host.Equals(".", StringComparison.InvariantCultureIgnoreCase))
                return @"127.0.0.1";

            IPAddress ipAddress;

            if (IPAddress.TryParse(host, out ipAddress))
                return host;

            throw new InvalidCastException($@"Ip address - '{host}' is wrong");
        }

    }
}
