using Scheduler.Domain.Entities;
using System;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Application.CaseUses
{
    public class NextDateCalculator
    { 
        public static Output CalculateOutput(Domain.Entities.Scheduler scheduler)
        {
            return new Output(CalculateNextDate(scheduler),
                            OutputDescriptionFormatter.Description(
                                scheduler.Configuration().DateTime().Value
                                , scheduler.Configuration().Type()
                                , scheduler.Configuration().Occurs()
                                , scheduler.Configuration().Every()
                                , scheduler.Limits().StartDate()
                           )
                       );
        }

        public static DateTime CalculateNextDate(Domain.Entities.Scheduler scheduler)
        {
            return scheduler.Configuration().Type().Equals(ConfigurationType.Once)
                ? scheduler.Configuration().DateTime().Value
                : AddRecurrenceToDate(scheduler.Input().CurrentDate(), scheduler.Configuration().Occurs(), scheduler.Configuration().Every());
        }

        private static DateTime AddRecurrenceToDate(DateTime date, RecurringType occurs, int every)
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
        
    }
}
