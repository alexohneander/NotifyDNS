using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyDNS.Schedule.Services
{
    public class ScheduleService : IScheduleService
    {
        private IServiceProvider _serviceProvider;
        public ScheduleService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Initialize();
        }

        public void Initialize()
        {
            Scheduler scheduler = new Scheduler(_serviceProvider);
            scheduler.CheckNotifysForSchedule();
            scheduler.StartSchedulerAsync();

        }
    }
}
