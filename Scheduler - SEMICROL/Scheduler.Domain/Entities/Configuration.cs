using System;
using System.Collections.Generic;
using System.Text;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Entities
{
    public class Configuration
    {
        internal bool enabled;
        internal DateTime? dateTime;
        internal int every;
        internal ConfigurationType type;
        internal RecurringType ocurs;
    }
}
