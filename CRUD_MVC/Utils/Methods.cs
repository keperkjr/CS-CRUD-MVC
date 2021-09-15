// ============================================================================
//    Author: Kenneth Perkins
//    Date:   May 13, 2021
//    Taken From: http://programmingnotes.org/
//    File:  Utils.cs
//    Description: Handles general utility functions
// ============================================================================
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Utils {
    public static class Methods {
        /// <summary>
        /// Returns the IPv4 address of the calling user. When under a web
        /// environment, returns the <see cref="System.Web.HttpRequest"/> 
        /// IP address, otherwise returns the IP address of the local machine
        /// </summary>
        /// <returns>The IPv4 address of the calling user</returns>
        public static string GetIPv4Address(HttpContext current) {
            var ipAddress = string.Empty;

            // Get client ip address
            if (current != null 
                && current.Request != null) {

                // Get client ip address using ServerVariables
                var request = current.Request;
                ipAddress = current.GetServerVariable("HTTP_X_FORWARDED_FOR");
                if (!string.IsNullOrEmpty(ipAddress)) {
                    var addresses = ipAddress.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (addresses.Length > 0) {
                        ipAddress = addresses[0];
                    }
                }
                if (string.IsNullOrEmpty(ipAddress)) {
                    ipAddress = current.GetServerVariable("REMOTE_ADDR");
                }
                //// Get client ip address using UserHostAddress
                //if (string.IsNullOrEmpty(ipAddress)) {
                //    var clientIPA = GetNetworkAddress(request.Host.ToString());
                //    if (clientIPA != null) {
                //        ipAddress = clientIPA.ToString();
                //    }
                //}
            }

            // Get local machine ip address
            if (string.IsNullOrEmpty(ipAddress)) {
                var loacalIPA = GetNetworkAddress(System.Net.Dns.GetHostName());
                if (loacalIPA != null) {
                    ipAddress = loacalIPA.ToString();
                }
            }
            return ipAddress;
        }

        private static System.Net.IPAddress GetNetworkAddress(string addresses) {
            return System.Net.Dns.GetHostAddresses(addresses)
               .FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }
    }
}// http://programmingnotes.org/