using EnsureThat;
using Scheduler.Domain.Common;
using Scheduler.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Entities
{
    public class Configuration
    {
        private readonly bool enabled;
        private readonly DateTime? dateTime;
        private readonly int every;
        private readonly ConfigurationType type;
        private readonly RecurringType occurs;

        public Configuration(bool enabled, DateTime? dateTime, int every, ConfigurationType type, RecurringType occurs)
        {
            Ensure.That(every, nameof(every), o => o.WithException(new DomainException("Every should be a positive number"))).IsGt(0);
            ValidateDateTime(enabled, dateTime, type);
            ValidateRecurrence(enabled, every, type); 
            this.enabled = enabled;
            this.dateTime = dateTime;
            this.every = every; 
            this.type = type;   
            this.occurs = occurs;
        }
        public bool Enabled()
        {
            return this.enabled;
        }

        public RecurringType Occurs()
        {
            return this.occurs;
        }

        public ConfigurationType Type()
        {
            return this.type;
        }

        public DateTime? DateTime()
        {
            return this.dateTime;
        }

        public int Every()
        {
            return this.every;
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
