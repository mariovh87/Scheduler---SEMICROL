using System;
using System.Collections.Generic;
using System.Text;

namespace Semicrol.Scheduler.Domain.Common
{
    public class SchedulerEnums
    {
        public enum ConfigurationType { Once, Recurring }
        public enum RecurringType { Daily, Monthly, Yearly}
        public enum DailyRecurrence { Hours, Minutes, Seconds}
    }
}
