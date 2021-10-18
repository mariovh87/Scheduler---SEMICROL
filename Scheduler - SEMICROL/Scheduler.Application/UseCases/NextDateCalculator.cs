using Semicrol.Scheduler.Domain.Entities;
using System;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.CaseUses
{
    public class NextDateCalculator
    {        

        private static DateTime AddRecurrenceToDate(DateTime date, RecurringType occurs, int every)
        {
            DateTime outputDate = new DateTime();
            switch (occurs)
            {
                case RecurringType.Daily:
                    outputDate = date.AddDays(every);          
                    break;
                case RecurringType.Monthly:
                    outputDate = date.AddMonths(every);
                    break;
                case RecurringType.Yearly:
                    outputDate = date.AddYears(every);
                    break;
            }
            return outputDate;
        }
        
    }
}
