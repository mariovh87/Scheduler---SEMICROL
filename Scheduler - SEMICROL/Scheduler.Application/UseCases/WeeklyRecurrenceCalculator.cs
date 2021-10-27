using EnsureThat;
using Semicrol.Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semicrol.Scheduler.Application.UseCases
{
    public class WeeklyRecurrenceCalculator
    {
        private const int ADD_DAYS_EVERY = 7;

        public static IList<DateTime> GetAllRecurrences(DateTime currentDate, WeeklyConfiguration config, DateTime limitDate)
        {
            List<DateTime> recurrenceDays = new List<DateTime>();
            DateTime startDate = currentDate;
            DateTime lastRecurrenceDate = startDate;
            while (CheckAddingWeeksIsBeforeLimitDate(lastRecurrenceDate, config.Every, limitDate))
            {
                recurrenceDays.AddRange(GetNextRecurrence(startDate, config));
                startDate = GetNextRecurrenceStartDate(startDate, config.Every);
                lastRecurrenceDate = recurrenceDays.OrderBy(d=>d).LastOrDefault();
            }
            
            return recurrenceDays;
        }

        public static IList<DateTime> GetNextRecurrence(DateTime currentDate, WeeklyConfiguration config)
        {
            IList<DateTime> recurrenceDays = new List<DateTime>();
            DateTime recurrenceStartDate = currentDate;
            if (config.DaysOfWeek.AreDaysChecked())
            {
                recurrenceDays = AddDaysToRecurrenceList(recurrenceStartDate, config.DaysOfWeek.Days);
            }
            return recurrenceDays;
        }

        public static IList<DateTime> AddDaysToRecurrenceList(DateTime currentDate, IList<ConfigDay> days)
        {
            List<DateTime> recurrenceDays = new List<DateTime>();
            DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;

            foreach (var configDay in SublistDaysOfWeekFromDate(currentDate,days))
            {
                if (configDay.IsChecked)
                {
                    recurrenceDays.Add(GetNextWeekday(currentDate, configDay.Day));
                }
            }

            return recurrenceDays;
        }

        public static DateTime GetNextRecurrenceStartDate(DateTime startDate, int every)
        {
            return startDate.AddDays(every * ADD_DAYS_EVERY);
        }

        public static bool CheckAddingWeeksIsBeforeLimitDate(IList<DateTime> recurrenceDays, int every, DateTime limitDate)
        {
            Ensure.That(recurrenceDays).IsNotNull();
            return recurrenceDays.Count() > 0
                ? CheckAddingWeeksIsBeforeLimitDate(recurrenceDays.Last(), every, limitDate)
                : false;
        }

        public static bool CheckAddingWeeksIsBeforeLimitDate(DateTime currentDate, int every, DateTime limitDate)
        {
            return GetNextRecurrenceStartDate(currentDate, every) <= limitDate;
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public static IList<ConfigDay> SublistDaysOfWeekFromDate(DateTime currentDate, IList<ConfigDay> days)
        {
            return days.Where(d => GetDayOfWeekIntConsideringSundayLast(d.Day) >= GetDayOfWeekIntConsideringSundayLast(currentDate.DayOfWeek)).ToList();
        }

        public static int GetDayOfWeekIntConsideringSundayLast(DayOfWeek day) 
        {
            return day == DayOfWeek.Sunday
                             ? 7
                             : (int)day;
        }

    }
}
