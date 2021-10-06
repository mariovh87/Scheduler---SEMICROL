using EnsureThat;
using Scheduler.Application.Interfaces;
using Scheduler.Domain.Common;
using Scheduler.Domain.Entities;
using System;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Application.CaseUses
{
    public class NextDateCalculator:IOutputCalculator
    { 
        public NextDateCalculator()
        {}

        public  Output CalculateOutput(Domain.Entities.Scheduler scheduler)
        {
            if (scheduler.Configuration().Type().Equals(ConfigurationType.Recurring))
            {
                return this.CalculateRecurring(scheduler.Configuration().DateTime().Value,
                    scheduler.Configuration().Occurs(), scheduler.Configuration().Every(),
                    scheduler.Limits().StartDate(), scheduler.Limits().EndDate().Value);
            }

            if (scheduler.Configuration().Type().Equals(ConfigurationType.Once))
            {
                return this.CalculateNext(scheduler.Configuration().DateTime().Value,
                    scheduler.Configuration().Occurs(), scheduler.Configuration().Every(),
                    scheduler.Limits().StartDate());
            }

            return null;
        }

        internal Output CalculateRecurring(DateTime date, RecurringType occurs, int every, DateTime startDate, DateTime endDate)
        {
            return new Output(CalculateExecutionTimesQueue(date, occurs, every, startDate, endDate),
                OutputDescriptionFormatter.Description(date, ConfigurationType.Once, occurs, every, startDate));
        }

        internal ExecutionTimesQueue CalculateExecutionTimesQueue(DateTime date, RecurringType occurs, int every, DateTime startDate, DateTime endDate)
        {
            ExecutionTimesQueue queue = new ExecutionTimesQueue();
            while (date <= endDate)
            {
                AddRecurrenceToDate(date, occurs, every);
                queue.QueueExecution(date);
            }
            return null;
        }

        internal DateTime AddRecurrenceToDate(DateTime date, RecurringType occurs, int every)
        {
            switch (occurs)
            {
                case RecurringType.Daily:
                    date.AddDays(every);          
                    break;
                case RecurringType.Monthly:
                    date.AddMonths(every);
                    break;
                case RecurringType.Yearly:
                    date.AddYears(every);
                    break;
            }
            return date;
        }

        internal Output CalculateNext(DateTime nextDate, RecurringType occurs, int every, DateTime startDate)
        {
            ExecutionTimesQueue executions = new ExecutionTimesQueue();
            executions.QueueExecution(nextDate);
            return new Output(executions, OutputDescriptionFormatter.Description(nextDate, ConfigurationType.Once, occurs, every, startDate)); 
        }
        
    }
}
