using DnsClient;
using DnsClient.Protocol;
using NotifyDNS.Core;
using NotifyDNS.Core.Models;
using NotifyDNS.Mail;
using System;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace NotifyDNS.Networking
{
    public class DNSCheck
    {
        public static async void CheckDNSEntry(NotifyModel notify, NotifyDNSDbContext context)
        {
            notify.CurrentIP = GetIPFromDNS(notify.Domain);

            //if(notify.CurrentIP == notify.DestinationIP && !notify.IsNotified)
            //{
            //    Mailer.SendMail();
            //    notify.IsNotified = true;
            //}

            context.Update(notify);
            await context.SaveChangesAsync();
        }

        public static string GetIPFromDNS(string dnsName)
        {
            var endpoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
            var client = new LookupClient(endpoint);
            client.UseCache = false;

            var result = client.Query(dnsName, QueryType.A);

            var record = result.Answers.ARecords().FirstOrDefault();
            var ip = record?.Address;

            if(ip != null)
            {
                return ip.ToString();
            }

            return "";
        }
    }
}
