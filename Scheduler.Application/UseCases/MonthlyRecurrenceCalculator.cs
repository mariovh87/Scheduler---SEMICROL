using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class MonthlyRecurrenceCalculator
    {
        public static IList<DateTime> GetTheCheckRecurrences(DateTime currentDate, Limits limits, MonthlyConfiguration config)
        {
            DateTimeExtensionMethods.EnsureDateIsInRange(currentDate, limits.StartDate, limits.EndDate);
            Ensure.That(config).IsNotNull();
            Ensure.That(config.The).IsTrue();
            List<DateTime> recurrenceDays = new();


            return recurrenceDays;
        }

        public static DateTime GetDateInMonth(IEnumerable<DateTime> days, MonthlyFrecuency frecuency)
        {
            Ensure.That(days).IsNotNull();
            Ensure.That(days.Any()).IsTrue();
            switch (frecuency)
            {
                case MonthlyFrecuency.First:return days.FirstOrDefault();
                case MonthlyFrecuency.Second: return days.ElementAtOrDefault(1);
                case MonthlyFrecuency.Third: return days.ElementAtOrDefault(2);
                case MonthlyFrecuency.Fourth: return days.ElementAtOrDefault(3);
                case MonthlyFrecuency.Last: return days.LastOrDefault();
            }
            return DateTime.MinValue;
        }

        public static IEnumerable<DateTime> GetWeekdaysForMonth(DateTime currentDate, DayOfWeek weekDay)
        {
            return GetDaysInMonth(currentDate, 1).Where(day => day.DayOfWeek == weekDay);
        }
        private static IEnumerable<DateTime> GetDaysInMonth(DateTime date, int startDay)
        {
            var dateStart = new DateTime(date.Year, date.Month, startDay);
            while (dateStart.Month == date.Month)
            {
                yield return dateStart;
                dateStart = dateStart.AddDays(1);
            }
        }

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
                startDate = new(startDate.Year, startDate.Month, 
                    CheckIfMonthHaveDay(startDate, config.EveryDay.Value)
                    ? config.EveryDay.Value
                    : DateTime.DaysInMonth(startDate.Year, startDate.Month));                                       

                if (startDate <= limits.EndDate)
                {
                    recurrenceDays.Add(startDate);
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
            if (startDate < limitEndDate)
            {              
                if (CheckIfMonthHaveDay(startDate, everyDay))
                {
                    return new(startDate.Year, startDate.Month, everyDay);
                }
                else
                {
                    return new(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                }
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
