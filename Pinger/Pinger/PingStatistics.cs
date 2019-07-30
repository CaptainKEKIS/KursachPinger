using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinger
{
    class PingStatistics
    {
        public int Sent { get; set; }
        public int Received { get; set; }
        public int Loss { get; set; }
        public double LossRate { get; set; }
        public int MinTime { get; set; }
        public int MaxTime { get; set; }
        public double AvgTime { get; set; }
    }
}
