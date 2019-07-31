using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Pinger
{
    class PingParam
    {
        public int TimeOut { get; set; }
        public int Ttl { get; set; }
        public int DataSize { get; set; }
        public PingOptions Poptions { get; set; }
    }
}
