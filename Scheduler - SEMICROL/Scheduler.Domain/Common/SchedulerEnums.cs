using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Common
{
    public class SchedulerEnums
    {
        public enum ConfigurationType { Once, Recurring }
        public enum RecurringType { Daily, Monthly, Yearly}
    }
}
