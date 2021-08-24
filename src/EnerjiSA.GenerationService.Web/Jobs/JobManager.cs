using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Web
{
    public class JobManager
    {
        public static void Start()
        {
            RecurringJob.AddOrUpdate<GenerationSuccessJob>(x => x.Generate(), Cron.Hourly(), TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<GenerationFailJob>(x => x.Generate(), Cron.Hourly(), TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<HourlyQueryJob>(x => x.Run(), Cron.Hourly(), TimeZoneInfo.Local);
        }
    }
}
