using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Common
{
    internal class SchedulerEnums
    {
        internal enum ConfigurationType { Once, Recurring }
        internal enum RecurringType { Daily, Weekly, Monthly}
    }
}
