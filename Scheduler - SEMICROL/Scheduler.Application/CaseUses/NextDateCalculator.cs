using EnsureThat;
using Scheduler.Application.Interfaces;
using Scheduler.Domain.Common;
using Scheduler.Domain.Entities;
using System;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Application.CaseUses
{
    internal class NextDateCalculator:IOutputCalculator
    { 
        public NextDateCalculator()
        {}

        public  Output CalculateOutput(Input input, Configuration config, Limits limits)
        {
            Ensure.That<Input>(input).IsNotNull();
            Ensure.That<Configuration>(config).IsNotNull();
            Ensure.That<Limits>(limits).IsNotNull();

            if (config.Type().Equals(ConfigurationType.Recurring))
            {
                this.ValidateInputDateLimits(input, limits);
                return this.CalculateRecurring(config.DateTime().Value, config.Occurs(), config.Every(), limits.StartDate(), limits.EndDate().Value);
            }

            if (config.Type().Equals(ConfigurationType.Once))
            {
                return this.CalculateNext(config.DateTime().Value, config.Occurs(), config.Every(), limits.StartDate());
            }

            return null;
        }

        private Output CalculateRecurring(DateTime nextDate, RecurringType occurs, int every, DateTime startDate, DateTime endDate)
        {
            //TODO - Calculate ExecutionTimes adding "every" months, years or days in function of occurs
            return null;
        }

        private Output CalculateNext(DateTime nextDate, RecurringType occurs, int every, DateTime startDate)
        {
            ExecutionTimesQueue executions = new ExecutionTimesQueue();
            executions.QueueExecution(nextDate);
            return new Output(executions, OutputDescriptionFormatter.Description(nextDate, ConfigurationType.Once, occurs, every, startDate)); 
        }

        public void ValidateInputDateLimits(Input input, Limits limits)
        {
            limits.EndDate().EnsureIsValidDate();
            Ensure.That<DateTime>(input.CurrentDate()).IsInRange(limits.StartDate(), limits.EndDate().Value);
        }
    }
}
