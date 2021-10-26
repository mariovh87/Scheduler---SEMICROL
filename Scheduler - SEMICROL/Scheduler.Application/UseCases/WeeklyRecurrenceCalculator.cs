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
            do
            {
                recurrenceDays.AddRange(GetNextRecurrence(currentDate, config, limitDate));
            }
            while (CheckAddingWeeksIsBeforeLimitDate(recurrenceDays, config.Every, limitDate));

            return recurrenceDays;
        }

        private static IList<DateTime> GetNextRecurrence(DateTime currentDate, WeeklyConfiguration config, DateTime limitDate)
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
            IList<DateTime> recurrenceDays = new List<DateTime>();
            foreach (var configDay in days)
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
    }
}
