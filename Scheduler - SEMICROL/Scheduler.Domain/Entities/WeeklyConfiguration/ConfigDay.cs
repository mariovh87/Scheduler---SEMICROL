using System;
namespace Semicrol.Scheduler.Domain.Entities
{
    public class ConfigDay
    {
        public DayOfWeek Day { get; private set; }

        public bool IsChecked { get; set; }

        public ConfigDay(DayOfWeek day, bool isChecked)
        {
            this.Day = day;
            this.IsChecked = isChecked;
        }
    }
}
