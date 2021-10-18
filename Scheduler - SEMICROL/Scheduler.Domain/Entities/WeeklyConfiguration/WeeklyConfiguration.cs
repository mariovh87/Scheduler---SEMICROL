using System;
using EnsureThat;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class WeeklyConfiguration
    {
        public int Every { get; private set; }
        private readonly ConfigDaysOfWeek daysOfWeek;
        public WeeklyConfiguration(int every)
        {
            Ensure.That(every).IsGt(0);
            this.Every = every;
            daysOfWeek = new ConfigDaysOfWeek();
        }
        public bool IsMondayChecked()
        {
            return daysOfWeek.IsMondayChecked();
        }

        public bool IsTuesdayChecked()
        {
            return daysOfWeek.IsTuesdayChecked();
        }

        public bool IsWednesdayChecked()
        {
            return daysOfWeek.IsWednesdayChecked();
        }

        public bool IsThursdayChecked()
        {
            return daysOfWeek.IsThursdayChecked();
        }

        public bool IsFridayChecked()
        {
            return daysOfWeek.IsFridayChecked();
        }

        public bool IsSaturdayChecked()
        {
            return daysOfWeek.IsSaturdayChecked();
        }

        public bool IsSundayChecked()
        {
            return daysOfWeek.IsSundayChecked();
        }
    }
}
