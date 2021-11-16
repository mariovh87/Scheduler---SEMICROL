using System;
using System.Collections.Generic;
using System.Text;

namespace Semicrol.Scheduler.Domain.Common
{
    public class SchedulerEnums
    {
        public enum ConfigurationType { Once, Recurring }
        public enum RecurringType { Daily, Weekly}
        public enum DailyRecurrence { Hours, Minutes, Seconds}
        public enum MonthlyFrecuency { First, Second, Third, Fourth, Last }
        public enum MonthlyDayType { Monday, Tuesday, Wednesday,Thursday, Friday, Saturday, Sunday, Day, WeekDay, WeekendDay}
    }
}
