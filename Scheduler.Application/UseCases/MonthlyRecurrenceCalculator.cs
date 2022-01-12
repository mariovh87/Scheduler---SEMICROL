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
        public static IList<DateTime> GetRecurrences(DateTime currentStartDate, Limits limits, MonthlyConfiguration config)
        {
            DateTimeExtensionMethods.EnsureDateIsInRange(currentStartDate, limits.StartDate, limits.EndDate);
            limits.EndDate.EnsureIsValidDate();
            Ensure.That(config).IsNotNull();
            if (config.The)
            {
                return GetTheRecurrences(currentStartDate, limits.EndDate.Value, config.DayType.Value, config.Frecuency.Value, config.EveryMonths.Value);

            }
            else
            {
                return GetDayRecurrences(currentStartDate, limits, config);
            }

        }

        public static IList<DateTime> GetTheRecurrences(DateTime currentStartDate, DateTime endDate, MonthlyDayType dayType, MonthlyFrecuency frecuency, int everyMonths)
        {
            List<DateTime> recurrenceDays = new();
            DateTime currentDate = currentStartDate;
            DateTime currentDay = DateTime.MinValue;
            while (currentDate <= endDate)
            {
                currentDay = GetCurrentDay(currentDate, dayType, frecuency);
                if (currentDay >= currentDate && currentDay <= endDate)
                {
                    recurrenceDays.Add(currentDay);
                }
                currentDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(everyMonths);
            }
            return recurrenceDays;
        }

        public static DateTime GetCurrentDay(DateTime currentDate, MonthlyDayType dayType, MonthlyFrecuency frecuency)
        {
            return dayType switch
            {
                MonthlyDayType.Monday or
                MonthlyDayType.Tuesday or
                MonthlyDayType.Wednesday or
                MonthlyDayType.Thursday or
                MonthlyDayType.Friday or
                MonthlyDayType.Saturday or
                MonthlyDayType.Sunday => GetDayOfWeekInMonthOfFrecuency(currentDate, dayType, frecuency),
                MonthlyDayType.Day => GetDayForMonth(currentDate, frecuency),
                MonthlyDayType.WeekendDay => GetDayOfWeekendInMonthOfFrecuency(currentDate, frecuency),
                _ => throw new ArgumentException(),
            };
        }

        public static DateTime GetDayOfWeekInMonthOfFrecuency(DateTime currentStartDate, MonthlyDayType dayType, MonthlyFrecuency frecuency)
        {
            var days = GetDaysOfWeekInMonth(currentStartDate, 1, ConvertMonthlyFrecuency(dayType));
            return GetDateInMonth(days, frecuency);
        }

        public static DateTime GetDayOfWeekendInMonthOfFrecuency(DateTime currentStartDate, MonthlyFrecuency frecuency)
        {
            var days = GetWeekendDayForMonth(currentStartDate, frecuency);
            return GetDateInMonth(days, frecuency);
        }

        public static DateTime GetDateInMonth(IEnumerable<DateTime> days, MonthlyFrecuency frecuency)
        {
            Ensure.That(days).IsNotNull();
            Ensure.That(days.Any()).IsTrue();
            return frecuency switch
            {
                MonthlyFrecuency.First => days.FirstOrDefault(),
                MonthlyFrecuency.Second => days.ElementAtOrDefault(1),
                MonthlyFrecuency.Third => days.ElementAtOrDefault(2),
                MonthlyFrecuency.Fourth => days.ElementAtOrDefault(3),
                MonthlyFrecuency.Last => days.LastOrDefault(),
                _ => DateTime.MinValue,
            };
        }

        public static IEnumerable<DateTime> GetWeekendDayForMonth(DateTime currentDate, MonthlyFrecuency frecuency)
        {
            currentDate.EnsureIsValidDate();
            if (frecuency == MonthlyFrecuency.Third || frecuency == MonthlyFrecuency.Fourth)
            {
                throw new ArgumentException($"Weekend Day {frecuency} doesnt exist");
            }
            return frecuency switch
            {
                MonthlyFrecuency.First => GetDaysOfWeekInMonth(currentDate, DayOfWeek.Saturday),
                MonthlyFrecuency.Second or MonthlyFrecuency.Last => GetDaysOfWeekInMonth(currentDate, DayOfWeek.Sunday),
                _ => Array.Empty<DateTime>(),
            };
        }

        public static IEnumerable<DateTime> GetWeekDayForMonth(DateTime currentDate, MonthlyFrecuency frecuency)
        {
            currentDate.EnsureIsValidDate();
            return frecuency switch
            {
                MonthlyFrecuency.First => GetDaysOfWeekForMonth(currentDate, DayOfWeek.Monday),
                MonthlyFrecuency.Second => GetDaysOfWeekForMonth(currentDate, DayOfWeek.Tuesday),
                MonthlyFrecuency.Third => GetDaysOfWeekForMonth(currentDate, DayOfWeek.Wednesday),
                MonthlyFrecuency.Fourth => GetDaysOfWeekForMonth(currentDate, DayOfWeek.Thursday),
                MonthlyFrecuency.Last => GetDaysOfWeekForMonth(currentDate, DayOfWeek.Friday),
                _ => Array.Empty<DateTime>(),
            };
        }

        public static DateTime GetDayForMonth(DateTime currentDate, MonthlyFrecuency frecuency)
        {
            currentDate.EnsureIsValidDate();
            return frecuency switch
            {
                MonthlyFrecuency.First => new DateTime(currentDate.Year,currentDate.Month, 1),
                MonthlyFrecuency.Second => new DateTime(currentDate.Year, currentDate.Month, 2),
                MonthlyFrecuency.Third => new DateTime(currentDate.Year, currentDate.Month, 3),
                MonthlyFrecuency.Fourth => new DateTime(currentDate.Year, currentDate.Month, 4),
                MonthlyFrecuency.Last => new DateTime(currentDate.Year, currentDate.Month+1, 1).AddDays(-1),
                _ => DateTime.MinValue,
            };
        }

        public static IEnumerable<DateTime> GetDaysOfWeekForMonth(DateTime currentDate, DayOfWeek weekDay)
        {
            return GetDaysOfWeekInMonth(currentDate, weekDay);
        }
        private static IEnumerable<DateTime> GetDaysOfWeekInMonth(DateTime date, int startDay, DayOfWeek weekDay)
        {
            var dateStart = new DateTime(date.Year, date.Month, startDay);
            while (dateStart.Month == date.Month)
            {
                if(dateStart.DayOfWeek == weekDay)
                {
                    yield return dateStart;
                }               
                dateStart = dateStart.AddDays(1);
            }
        }

        public static DayOfWeek ConvertMonthlyFrecuency(MonthlyDayType frecuency)
        {
            return frecuency switch
            {
                MonthlyDayType.Monday => DayOfWeek.Monday,
                MonthlyDayType.Tuesday => DayOfWeek.Tuesday,
                MonthlyDayType.Wednesday => DayOfWeek.Wednesday,
                MonthlyDayType.Thursday => DayOfWeek.Thursday,
                MonthlyDayType.Friday => DayOfWeek.Friday, 
                MonthlyDayType.Saturday => DayOfWeek.Saturday,
                MonthlyDayType.Sunday => DayOfWeek.Sunday,
                _ => throw new ArgumentException("Invalidad Monthly Day Type"),
            };
        }

        private static IEnumerable<DateTime> GetDaysOfWeekInMonth(DateTime date, DayOfWeek weekDay)
        {
            return GetDaysOfWeekInMonth(date, date.Day, weekDay);
        }

        public static IList<DateTime> GetDayRecurrences(DateTime currentDate, Limits limits, MonthlyConfiguration config)
        {
            DateTimeExtensionMethods.EnsureDateIsInRange(currentDate, limits.StartDate, limits.EndDate);
            Ensure.That(config).IsNotNull();
            Ensure.That(config.Day).IsTrue();
            List<DateTime> recurrenceDays = new();

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
