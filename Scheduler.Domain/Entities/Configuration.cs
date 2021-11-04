using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class Configuration
    {
        public bool Enabled { get; private set; }
        public DateTime? OnceTimeAt { get; private set; }
        public ConfigurationType Type { get; private set; }
        public RecurringType Occurs { get; private set; }

        public Configuration(bool enabled, DateTime? dateTime, ConfigurationType type, RecurringType occurs)
        {
            ValidateDateTime(enabled, dateTime, type);
            this.Enabled = enabled;
            this.OnceTimeAt = dateTime;
            this.Type = type;   
            this.Occurs = occurs;
        }

        public static void ValidateDateTime(bool enabled, DateTime? dateTime, ConfigurationType type)
        {
            if (enabled && type.Equals(ConfigurationType.Once))
            {
                dateTime.EnsureIsValidDate();
            }
        }

        public static void ValidateRecurrence(bool enabled, int every, ConfigurationType type)
        {
            if (enabled && type.Equals(ConfigurationType.Recurring))
            {
                Ensure.That<int>(every).IsGt(0);
            }
        }
    }
}
