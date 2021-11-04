using EnsureThat;
using Semicrol.Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class WeeklyRecurrenceCalculator
    {
        private const int ADD_DAYS_EVERY = 7;

        public static IList<DateTime> GetAllRecurrences(DateTime currentDate, WeeklyConfiguration config, DailyFrecuency dailyFrecuency, DateTime limitDate)
        {
            DateTime startDate = currentDate;
            List<DateTime> recurrenceDays = new List<DateTime>();
            while (CheckDateIsBeforeLimitDate(startDate, limitDate))
            {
                IList<DateTime> nextRecurrenceWeek = GetNextRecurrence(startDate, config);
                foreach (var day in nextRecurrenceWeek)
                {
                    if (CheckDateIsBeforeLimitDate(day, limitDate))
                    {
                        recurrenceDays.AddRange(DailyFrecuencyCalculator.GetExecutions(day, dailyFrecuency));
                    }
                }
                startDate = GetRecurrenceNextStartDate(startDate, config.Every);
            }
            return recurrenceDays;
        }

        public static bool CheckDateIsBeforeLimitDate(DateTime startDate, DateTime limitDate)
        {
            return startDate <= limitDate;
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

            foreach (var configDay in SublistDaysOfWeekFromDate(currentDate, days))
            {
                if (configDay.IsChecked)
                {
                    recurrenceDays.Add(GetNextWeekday(currentDate, configDay.Day));
                }
            }

            return recurrenceDays;
        }

        public static DateTime AddEveryBy7DaysToCurrentDate(DateTime currentStartDate, int every)
        {
            return currentStartDate.AddDays(every * ADD_DAYS_EVERY);
        }


        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public static DateTime GetRecurrenceNextStartDate(DateTime currentStartDate, int every)
        {
            DateTime nextMonday = GetCurrentWeekMonday(currentStartDate);
            return AddEveryBy7DaysToCurrentDate(nextMonday, every);
        }

        public static DateTime GetCurrentWeekMonday(DateTime currentStartDate)
        {
            int diff = DayOfWeek.Monday - currentStartDate.DayOfWeek;
            return currentStartDate.AddDays(diff).Date;
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
