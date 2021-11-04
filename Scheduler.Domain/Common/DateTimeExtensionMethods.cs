using EnsureThat;
using Semicrol.Scheduler.Domain.Exceptions;
using System;

namespace Semicrol.Scheduler.Domain.Common
{
    public static class DateTimeExtensionMethods
    {

        public static void EnsureIsValidDate(this DateTime date)
        {
            if (!date.IsValidDate())
            {
                throw new InvalidDateException();
            }
        }

        public static void EnsureIsValidDate(this DateTime? date)
        {
            Ensure.That(date).HasValue();
            date.Value.EnsureIsValidDate();
        }

        public static void EnsureValueIsValidDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                EnsureIsValidDate(date.Value);
            }
        }

        public static bool IsValidDate(this DateTime date)
        {
            return !date.IsMinValue() && !date.IsMaxValue();
        }

        public static bool IsMinValue(this DateTime date)
        {
            return date == DateTime.MinValue;
        }

        public static bool IsMaxValue(this DateTime date)
        {
            return date == DateTime.MaxValue;
        }

        public static bool HasValidValue(this DateTime? date)
        {
            return date.HasValue && !date.IsMinValue() && !date.IsMaxValue();
        }

        public static bool IsMinValue(this DateTime? date)
        {
            return date.HasValue && date.Value == DateTime.MinValue;
        }

        public static bool IsMaxValue(this DateTime? date)
        {
            return date.HasValue && date.Value == DateTime.MaxValue;
        }

        public static bool CheckIfStartDateIsGte(DateTime startDate, DateTime endDate)
        {
            startDate.EnsureIsValidDate();
            endDate.EnsureIsValidDate();

            return startDate >= endDate;
        }

        public static bool IsValidRange(DateTime startDate, DateTime endDate)
        {
            return CheckIfStartDateIsGte(startDate, endDate);
        }

        public static void EnsureIsValidRange(DateTime startDate, DateTime endDate)
        {
            Ensure.That(startDate, nameof(startDate), o=>o.WithException(new DateRangeException("Start date should be greather than end date"))).IsLt(endDate);
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
