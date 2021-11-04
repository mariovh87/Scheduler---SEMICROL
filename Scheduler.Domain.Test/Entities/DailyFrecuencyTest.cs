using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Exceptions;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class DailyFrecuencyTest
    {
        private readonly TimeOnly? occursOnceAt = new TimeOnly(05, 30, 00);
        private readonly int frecuency = 10;
        private readonly TimeOnly? startingAt = new TimeOnly(01, 00, 00);
        private readonly TimeOnly? endsAt = new TimeOnly(02, 00, 00);
        private readonly DailyRecurrence every = DailyRecurrence.Hours;

        [Fact]
        public void frecuency_positive_and_date_range_correct_should_not_throw_exception()
        {
            Action dailyFrecuency = () =>
            {
                new DailyFrecuency(false, false, this.occursOnceAt, frecuency, every, startingAt, endsAt);
            };
            dailyFrecuency.Should().NotThrow<ArgumentException>();
        }
     
        [Fact]
        public void if_occurs_every_is_false_ensure_start_end_times_only_should_not_throw_exception_if_valid_is_valid_range()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureTimeOnlyRangeIsValid")).First();
            Action dailyFrecuency = () =>
            {
                occursOnceValidator.Invoke(null, new object[] { startingAt, endsAt });
            };
            dailyFrecuency.Should().NotThrow();
        }

        [Fact]
        public void if_occurs_every_is_false_ensure_start_end_dates_are_valid_dates_should_throw_exception_if_valid_dates_and_is_not_valid_range()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureTimeOnlyRangeIsValid")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { endsAt, startingAt }).Should();
            }
            catch (Exception e)
            {
                e.InnerException.Should().BeOfType<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void if_occurs_every_and_frecuency_is_invalid_ensure_every_is_valid_value_should_throw_exception()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureEveryIsValidValue")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { false, 0 });
            }
            catch (TargetInvocationException e)
            {
                e.InnerException.Should().BeOfType<InvalidDateException>();
            }
        }

        [Fact]
        public void if_occurs_every_and_frecuency_is_valid_ensure_every_is_valid_value_should_not_throw_exception()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureEveryIsValidValue")).First();
            Action dailyFrecuency = () =>
            {
                occursOnceValidator.Invoke(null, new object[] { false, 0 });
            };
            dailyFrecuency.Should().NotThrow();
        }
    }
}
