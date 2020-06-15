using Microsoft.EntityFrameworkCore;
using NotifyDNS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NotifyDNS.Core
{
    public class NotifyDNSDbContext : DbContext
    {
        public NotifyDNSDbContext(IServiceProvider services)
        {

        }

        public DbSet<NotifyModel> NotifyModels { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("Data Source=notifydns.db");
    }
}
