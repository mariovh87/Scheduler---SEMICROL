using EnsureThat;
using Scheduler.Domain.Common;
using Scheduler.Domain.Exceptions;
using System;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Entities
{
    public class Scheduler
    {
        private readonly Input input;
        private readonly Configuration config;
        private readonly Limits limits;
        public Scheduler(Input input, Configuration config, Limits limits)
        {
            Ensure.That(input).IsNotNull();
            Ensure.That(config).IsNotNull();
            Ensure.That(limits).IsNotNull(); 
 
            ValidateInputDateLimits(input, config, limits);
            this.input = input;
            this.config = config;
            this.limits = limits;
        }

        private void ValidateInputDateLimits(Input input, Configuration config, Limits limits)
        {
            if (config.Type().Equals(ConfigurationType.Recurring))
            {
                limits.EndDate().EnsureIsValidDate();
                DateTimeExtensionMethods.EnsureDateIsInRange(input.CurrentDate(), limits.StartDate(), limits.EndDate().Value);
            }          
        }

        public Input Input()
        {
            return input;
        }

        public Configuration Configuration()
        {
            return config;
        }

        public Limits Limits()
        {
            return limits;
        }
    }
}
