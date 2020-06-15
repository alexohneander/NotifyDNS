using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Net.Security;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotifyDNS;
using NotifyDNS.Core;
using NotifyDNS.Core.Models;
using NotifyDNS.Networking;

namespace NotifyDNS.Schedule
{
    public class Scheduler
    {
        private readonly NotifyDNSDbContext _context;
        private Timer _timer;

        public Scheduler(IServiceProvider services)
        {
            _context = new NotifyDNSDbContext(services);
        }

        public void CheckNotifysForSchedule()
        {
            var notifys = _context.NotifyModels.ToList();
            var notifysNotScheduled = notifys.Where(n => (n as NotifyModel).IsScheduled == false).ToList();

            StartScheduleForNotScheduledNotifys(notifysNotScheduled);
        }

        public async void CheckSchedules(Object source, ElapsedEventArgs e)
        {
            var notifys = await _context.NotifyModels.ToListAsync();
            var notifysScheduled = notifys.Where(n => n.IsScheduled == true).ToList();

            foreach(var n in notifysScheduled)
            {
                DNSCheck.CheckDNSEntry(n, _context);
            }

            Console.WriteLine("Scheduler: Check Schedules - " + DateTime.Now);
        }

        private static void StartScheduleForNotScheduledNotifys(List<NotifyModel> notifys)
        {

        }

        public async void StartSchedulerAsync()
        {
            _timer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            _timer.Elapsed += CheckSchedules;
            _timer.Enabled = true;

            Console.WriteLine("Scheduler: Start Schedules");
        }
    }
}
