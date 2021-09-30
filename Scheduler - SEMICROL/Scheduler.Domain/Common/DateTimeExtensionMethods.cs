using EnsureThat;
using System;

namespace Scheduler.Domain.Common
{
    public static class DateTimeExtensionMethods
    {

        public static void EnsureIsValidDate(this DateTime date)
        {
            Ensure.That<DateTime>(date).IsGt(DateTime.MinValue);
            Ensure.That<DateTime>(date).IsLt(DateTime.MaxValue);
        }

        public static void EnsureIsValidDate(this DateTime? date)
        {
            Ensure.That(date).HasValue();
            date.EnsureIsValidDate();
        }

        public static void ValidateRangeStartEnd(DateTime startDate, DateTime? endDate)
        {
            Ensure.That(startDate).HasValue();
            if (endDate.HasValue)
            {
                Ensure.That<DateTime>(startDate).IsGt(endDate.Value);
            }
        }
    }
}
