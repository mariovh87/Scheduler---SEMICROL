using System;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Entities
{
    public static class OutputDescriptionFormatter
    {
        private const string format = "Occurs {0}. Schedule will be used on {1} starting on {2}";

        public static string Description(DateTime date, ConfigurationType type, RecurringType occurs, int every, DateTime startDate)
        {
            return String.Format(format, GetOccurs(type,occurs, every), date.ToString(), startDate.ToString());
        }

        public static string GetOccurs(ConfigurationType type, RecurringType occurs, int every)
        {
            return type.Equals(ConfigurationType.Once)
                ? ConfigurationType.Once.ToString()
                : GetRecurringTypeString(occurs, every);
        }

        public static string GetRecurringTypeString(RecurringType ocurs, int every)
        {
            switch (ocurs)
            {
                case RecurringType.Monthly:
                    return every > 1
                        ? "Month"
                        : "Months";
                case RecurringType.Weekly:
                    return every > 1
                        ? "Week"
                        : "Weeks";
                case RecurringType.Daily:
                    return every > 1
                        ? "Day"
                        : "Days";
            }
            return String.Empty;
        }
    }
}
