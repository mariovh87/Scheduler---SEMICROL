using System;
using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Exceptions;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class DailyFrecuency
    {
        public DateTime? OccursOnceAt { get; private set; }
        public DailyRecurrence Every { get; private set; }
        public DateTime? StartingAt { get; private set; }
        public DateTime? EndsAt { get; private set; }
        public int Frecuency { get; private set; }

        public DailyFrecuency(DateTime? occursOnceAt, int frecuency, DailyRecurrence every, DateTime? startingAt, DateTime? endsAt)
        {
            occursOnceAt.EnsureIsValidDate();
            startingAt.EnsureIsValidDate();
            endsAt.EnsureIsValidDate();
            Ensure.That(frecuency).IsGt(0);
            if(startingAt.HasValue)
            {
                DateTimeExtensionMethods.EnsureIsValidRange(startingAt.Value, endsAt.Value);
            }
            CheckAllDatesInvalidValue(occursOnceAt, startingAt, endsAt);

            this.OccursOnceAt = occursOnceAt;
            this.Frecuency = frecuency;
            this.Every = every;
            this.StartingAt = startingAt;
            this.EndsAt = endsAt;
        }

        private void CheckAllDatesInvalidValue(DateTime? occursOnceAt, DateTime? startingAt, DateTime? endsAt)
        {
            if (occursOnceAt.HasValidValue() ||
                ((startingAt.HasValidValue() && endsAt.HasValidValue())
                && DateTimeExtensionMethods.IsValidRange(startingAt.Value, endsAt.Value)))
            {
                return;
            }
            throw new DomainException("OccursOnceAt should be a valid date or startingAt and endsAt should be a valid range");
        }
    }
}
