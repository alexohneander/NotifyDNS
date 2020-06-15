using NotifyDNS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyDNS.Core.Models
{
    public class NotifyModel : IData
    {
        public Guid Id                  { get; set; }
        public String Domain            { get; set; }
        public String CurrentIP         { get; set; }
        public String DestinationIP     { get; set; }

        public Boolean IsScheduled      { get; set; }
        public Boolean IsNotified       { get; set; }
    }
}
