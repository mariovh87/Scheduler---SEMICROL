using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Entities
{
    public static class OutputDescriptionFormatter
    {
        private const string occursOnceFormat = "Occurs only once at {0}";
        private const string weeklyConfigFormat = "Occurs every {0} weeks on {1}";
        private const string dailyConfigFormat = " every {0} {1} between {2} and {3}";

        public static string OccursOnceDescription(DateTime date)
        {
            return String.Format(occursOnceFormat, date.ToShortDateString());
        }

        public static string Description(Input input, Configuration config, DailyFrecuency dailyFrecuency, WeeklyConfiguration weeklyConfiguration)
        {
            if (config.Type == ConfigurationType.Recurring)
            {
                return GetWeeklyConfigurationDescription(weeklyConfiguration.Every,weeklyConfiguration.DaysOfWeek.Days) +
                    GetDailyRecurrenceDescription(weeklyConfiguration.Every, dailyFrecuency.Every, dailyFrecuency.StartingAt.Value, dailyFrecuency.EndsAt.Value);
            }
            else
            {
                config.OnceTimeAt.EnsureIsValidDate();
                return OccursOnceDescription(config.OnceTimeAt.Value);
            }
        }

        public static string GetWeeklyConfigurationDescription(int every, IList<ConfigDay> days)
        {
            return String.Format(weeklyConfigFormat, every, GetDaysOfWeekString(days));
        }

        public static string GetDailyRecurrenceDescription(int every, DailyRecurrence dailyRecurrence, TimeOnly startTime, TimeOnly endTime)
        {
            return String.Format(dailyConfigFormat, every, dailyRecurrence.ToString(), startTime.ToLongTimeString(), endTime.ToLongTimeString());
        }

        public static string GetDaysOfWeekString(IList<ConfigDay> days)
        {
            Ensure.That(days).HasItems();
            if (days.Where(d => d.IsChecked).Count() == 1)
            {
                return days.First().Day.ToString();
            }
            StringBuilder builder = new StringBuilder();
            var daysExceptLast = days.Where(d => d.IsChecked);
            daysExceptLast = daysExceptLast.Take(daysExceptLast.Count() - 1);
            builder.Append(string.Join(",", daysExceptLast.Select(d => d.Day.ToString())));          
            builder.Append(" and ");
            builder.Append(days.Last(d => d.IsChecked).Day.ToString());

            return builder.ToString().Trim();
        }
    }
}
