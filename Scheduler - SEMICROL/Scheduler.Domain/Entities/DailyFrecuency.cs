using System;
using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Exceptions;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class DailyFrecuency
    {
        public bool occursOnce{ get; private set;}
        public bool occursEvery{ get; private set; }
        public DateTime? OccursOnceAt { get; private set; }
        public DailyRecurrence Every { get; private set; }
        public DateTime? StartingAt { get; private set; }
        public DateTime? EndsAt { get; private set; }
        public int Frecuency { get; private set; }

        public DailyFrecuency(bool occursOnce, bool occursEvery, DateTime? occursOnceAt, int frecuency, DailyRecurrence every, DateTime? startingAt, DateTime? endsAt)
        {
            EnsureOnlyOneOptionIsChecked(occursOnce, occursEvery);
            EnsureOccursOnceAtIsValidDate(occursOnce, occursOnceAt);
            EnsureStartEndDatesAreValidIfOccursEvery(occursEvery, startingAt, endsAt);
            EnsureEveryIsValidValue(occursEvery, frecuency);

            this.occursOnce = occursOnce;
            this.occursEvery = occursEvery;
            this.OccursOnceAt = occursOnceAt;
            this.Frecuency = frecuency;
            this.Every = every;
            this.StartingAt = startingAt;
            this.EndsAt = endsAt;
        }

        private static void EnsureOccursOnceAtIsValidDate(bool occursOnce, DateTime? occursOnceAt)
        {
            if (occursOnce)
            {
                occursOnceAt.EnsureIsValidDate();
            }
        }

        private static void EnsureStartEndDatesAreValidIfOccursEvery(bool occursEvery, DateTime? startingAt, DateTime? endsAt)
        {
            if (occursEvery)
            {
                startingAt.EnsureIsValidDate();
                endsAt.EnsureIsValidDate();
                DateTimeExtensionMethods.EnsureIsValidRange(startingAt.Value, endsAt.Value);
            }
        }

        private static void EnsureEveryIsValidValue(bool occursEvery, int frecuency)
        {
            if (occursEvery)
            {
                Ensure.That(frecuency).IsGt(0);
            }
        }

        private static void EnsureOnlyOneOptionIsChecked(bool occursOnce, bool occursEvery)
        {
            if (occursOnce && occursEvery
                || !occursOnce && occursEvery)
            {
                throw new DomainException($"At least one of theese options:{nameof(occursOnce)},{nameof(occursEvery)}  must be true");
            }
        }
    }
}
