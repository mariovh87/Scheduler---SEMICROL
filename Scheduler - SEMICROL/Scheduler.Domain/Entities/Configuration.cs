using EnsureThat;
using Scheduler.Domain.Common;
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
        internal RecurringType occurs;

        public Configuration(bool enabled, DateTime? dateTime, int every, ConfigurationType type, RecurringType occurs)
        {
            Ensure.That<ConfigurationType>(type).IsNotDefault();
            Ensure.That<RecurringType>(occurs).IsNotDefault();
            this.ValidateDateTime(enabled, dateTime, type);
            this.ValidateRecurrence(enabled, every, type); 
            this.enabled = enabled;
            this.dateTime = dateTime;
            this.every = every; 
            this.type = type;   
            this.occurs = occurs;
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

        public void ValidateDateTime(bool enabled, DateTime? dateTime, ConfigurationType type)
        {
            if (this.enabled && type.Equals(ConfigurationType.Once))
            {
                dateTime.EnsureIsValidDate();
            }
        }
        public void ValidateRecurrence(bool enabled, int every, ConfigurationType type)
        {
            if (this.enabled && type.Equals(ConfigurationType.Recurring))
            {
                Ensure.That<int>(every).IsGt(0);
            }
        }



    }
}
