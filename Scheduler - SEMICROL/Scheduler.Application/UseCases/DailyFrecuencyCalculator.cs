using System;
using System.Collections.Generic;
using System.Text;
using Semicrol.Scheduler.Domain.Entities;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class DailyFrecuencyCalculator
    {
        public static Output CalculateDailyFrecuencyExecutions(DailyFrecuency dailyFrecuency, Input input)
        {
            return null;
        }

        public static DateTime AddTime(DateTime executionDate, DailyRecurrence every, int occursEvery)
        {
            switch (every)
            {
                case DailyRecurrence.Seconds: return executionDate.AddSeconds(occursEvery);
                case DailyRecurrence.Minutes: return executionDate.AddMinutes(occursEvery);
                default: return executionDate.AddHours(occursEvery);                                
            }
        }

        public static TimeOnly AddTime(TimeOnly startDate, DailyRecurrence every, int occursEvery)
        {
            switch (every)
            {
                case DailyRecurrence.Seconds: return startDate.Add(new TimeSpan(0,0,occursEvery));
                case DailyRecurrence.Minutes: return startDate.AddMinutes(occursEvery);
                default: return startDate.AddHours(occursEvery);
            }
        }

        public static IList<DateTime> GetExecutions(DateTime executionDate, TimeOnly startingAt, TimeOnly endsAt, DailyRecurrence every, int occursEvery)
        {
            IList<DateTime> executions = new List<DateTime>();
            TimeOnly addingTime = startingAt;
            while (addingTime <= endsAt)
            {                        
                executions.Add(new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, addingTime.Hour, addingTime.Minute, addingTime.Second));
                addingTime = AddTime(addingTime, every, occursEvery);
            }
            return executions;
        }
    }
}
