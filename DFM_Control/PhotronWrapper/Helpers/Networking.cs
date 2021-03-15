using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhotronWrapper.Helpers
{
    public static class Networking
    {
        /// <summary>
        /// converts from IP string to long
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static UInt32 IPToInt(string addr)
        {
            //split ip into array
            string[] ipAddress = addr.Split('.');

            int ipAddress0 = int.Parse(ipAddress[0]);
            int ipAddress1 = int.Parse(ipAddress[1]);
            int ipAddress2 = int.Parse(ipAddress[2]);
            int ipAddress3 = int.Parse(ipAddress[3]);

            UInt32 address = (UInt32)((ipAddress0 << 24) + (ipAddress1 << 16) + (ipAddress2 << 8) + ipAddress3);
            return address;
        }
    }
}
