using System;
using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class DailyFrecuencyTest
    {
        private readonly DateTime? occursOnceAt = new DateTime(2021, 12, 24);
        private readonly int frecuency = 10;
        private readonly DateTime? startingAt = new DateTime(2021, 12, 1);
        private readonly DateTime? endsAt = new DateTime(2021, 12, 5);
        private readonly DailyRecurrence every = DailyRecurrence.Hours;

        [Fact]
        public void frecuency_positive_and_date_range_correct_should_not_throw_exception()
        {
            Action dailyFrecuency = () =>
            {
                new DailyFrecuency(this.occursOnceAt, frecuency, every, startingAt, endsAt);
            };
            dailyFrecuency.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void occurs_once_at_null_should_not_throw_exception()
        {
            Action dailyFrecuency = () =>
            {
                new DailyFrecuency(null, frecuency, every, startingAt, endsAt);
            };
            dailyFrecuency.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void starting_at_null_and_ends_at_null_should_not_throw_exception()
        {
            Action dailyFrecuency = () =>
            {
                new DailyFrecuency(null, frecuency, every, startingAt, endsAt);
            };
            dailyFrecuency.Should().NotThrow<ArgumentException>();
        }
    }
}
