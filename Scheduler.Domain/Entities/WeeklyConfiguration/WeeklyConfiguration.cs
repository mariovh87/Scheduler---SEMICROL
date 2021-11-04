using System;
using EnsureThat;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class WeeklyConfiguration
    {
        public int Every { get; private set; }
        public ConfigDaysOfWeek DaysOfWeek { get; private set; }

        public WeeklyConfiguration(int every)
        {
            Ensure.That(every).IsGt(0);
            this.Every = every;
            this.DaysOfWeek = new ConfigDaysOfWeek();
        }

        public WeeklyConfiguration(int every, ConfigDaysOfWeek daysOfWeek)
        {
            Ensure.That(every).IsGt(0);
            this.Every = every;
            this.DaysOfWeek = daysOfWeek;
        }      
    }
}
