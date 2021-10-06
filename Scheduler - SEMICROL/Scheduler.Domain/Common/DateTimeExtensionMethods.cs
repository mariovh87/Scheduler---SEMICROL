using EnsureThat;
using Scheduler.Domain.Exceptions;
using System;

namespace Scheduler.Domain.Common
{
    public static class DateTimeExtensionMethods
    {

        public static void EnsureIsValidDate(this DateTime date)
        {
            Ensure.That(date).IsGt(DateTime.MinValue);
            Ensure.That(date).IsLt(DateTime.MaxValue);
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
                Ensure.That(startDate, nameof(startDate), o=>o.WithException(new DateRangeException("Start date should be greather than end date"))).IsGt(endDate.Value);
            }
        }

        public static void EnsureDateIsInRange(DateTime date, DateTime start, DateTime? end)
        {
            if (end.HasValue)
            {
                Ensure.That(date, nameof(date), o => o.WithException(new DateRangeException("Date should be between start date and end date"))).IsInRange(start, end.Value);
            }
            else
            {
                Ensure.That(date, nameof(date), o => o.WithException(new DateRangeException("Date should be greather start date"))).IsGt(start);
            }
        }
    }
}
