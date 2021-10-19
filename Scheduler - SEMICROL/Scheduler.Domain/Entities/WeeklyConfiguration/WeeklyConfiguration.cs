using System;
using EnsureThat;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class WeeklyConfiguration
    {
        public int Every { get; private set; }
        public ConfigDaysOfWeek daysOfWeek { get; private set; }

        public WeeklyConfiguration(int every)
        {
            Ensure.That(every).IsGt(0);
            this.Every = every;
            this.daysOfWeek = new ConfigDaysOfWeek();
        }

        public WeeklyConfiguration(int every, ConfigDaysOfWeek daysOfWeek)
        {
            Ensure.That(every).IsGt(0);
            this.Every = every;
            this.daysOfWeek = daysOfWeek;
        }      
    }
}
