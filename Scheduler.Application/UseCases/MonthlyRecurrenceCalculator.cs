using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration;
using System;
using System.Collections.Generic;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class MonthlyRecurrenceCalculator
    {
        public static IList<DateTime> GetDayRecurrences(DateTime currentDate, Limits limits, MonthlyConfiguration config)
        {
            DateTimeExtensionMethods.EnsureDateIsInRange(currentDate, limits.StartDate, limits.EndDate);
            Ensure.That(config).IsNotNull();
            Ensure.That(config.Day).IsTrue();
            List<DateTime> recurrenceDays = new List<DateTime>();

            DateTime startDate = GetFirstDate(currentDate, config.EveryDay.Value, config.DayMonths.Value, limits.EndDate.Value);
            if (startDate.Equals(DateTime.MaxValue) == false)
            {
                recurrenceDays.Add(startDate);
            }

            while (startDate <= limits.EndDate)
            {
                startDate = startDate.AddMonths(config.DayMonths.Value);
                if (CheckIfMonthHaveDay(startDate,config.EveryDay.Value))
                {
                    startDate = new(startDate.Year, startDate.Month, config.EveryDay.Value);
                    if (startDate <= limits.EndDate)
                    {
                        recurrenceDays.Add(startDate);
                    }                        
                }
            }

            return recurrenceDays;
        }

        public static DateTime GetFirstDate(DateTime currentDate, int everyDay, int everyMonths, DateTime limitEndDate)
        {
            if (CheckIfConfigDayIsAfterEqualsCurrentStartDate(currentDate, everyDay) == false
                && CheckIfMonthHaveDay(currentDate, everyDay))
            {
                return new(currentDate.Year, currentDate.Month, everyDay);
            }
            DateTime startDate = currentDate;
            startDate = startDate.AddMonths(everyMonths);
            while (startDate < limitEndDate)
            {              
                if (CheckIfMonthHaveDay(startDate, everyDay))
                {
                    return new(startDate.Year, startDate.Month, everyDay);
                }
                startDate = startDate.AddMonths(everyMonths);
            }
            
            return DateTime.MaxValue;
        }

        public static bool CheckIfConfigDayIsAfterEqualsCurrentStartDate(DateTime currentDate, int everyDay)
        {
            currentDate.EnsureIsValidDate();
            Ensure.That(everyDay).IsGt(0);
            Ensure.That(everyDay).IsLte(31);          

            return currentDate.Day>=everyDay;
        }

        public static bool CheckIfMonthHaveDay(DateTime currentDate, int everyDay)
        {
            int lastDayOfMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            return lastDayOfMonth >= everyDay;
        }
    }
}
