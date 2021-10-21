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
        public TimeOnly? OccursOnceAt { get; private set; }
        public DailyRecurrence Every { get; private set; }
        public TimeOnly? StartingAt { get; private set; }
        public TimeOnly? EndsAt { get; private set; }
        public int Frecuency { get; private set; }

        public DailyFrecuency(bool occursOnce, bool occursEvery, TimeOnly? occursOnceAt, int frecuency, DailyRecurrence every, TimeOnly? startingAt, TimeOnly? endsAt)
        {
            EnsureOnlyOneOptionIsChecked(occursOnce, occursEvery);
            EnsureEveryIsValidValue(occursEvery, frecuency);
            EnsureTimeOnlyRangeIsValid(startingAt, endsAt);

            this.occursOnce = occursOnce;
            this.occursEvery = occursEvery;
            this.OccursOnceAt = occursOnceAt;
            this.Frecuency = frecuency;
            this.Every = every;
            this.StartingAt = startingAt;
            this.EndsAt = endsAt;
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

        private static void EnsureTimeOnlyRangeIsValid(TimeOnly? startingAt, TimeOnly? endsAt)
        {
            if (startingAt.HasValue && endsAt.HasValue)
            {
                Ensure.That(startingAt.Value).IsLt(endsAt.Value);
            }
        }
    }
}
