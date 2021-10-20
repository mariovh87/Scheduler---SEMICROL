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
                new DailyFrecuency(false, false, this.occursOnceAt, frecuency, every, startingAt, endsAt);
            };
            dailyFrecuency.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void if_occurs_once_is_true_ensure_occurs_once_at_is_valid_date_should_throw_exception_if_occurs_once_is_not_valid_date()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureOccursOnceAtIsValidDate")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { true, DateTime.MinValue });
            }
            catch (TargetInvocationException e)
            {
                e.InnerException.Should().BeOfType<InvalidDateException>();
            }
        }

        [Fact]
        public void if_occurs_once_is_false_occurs_once_at_should_not_be_validated()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureOccursOnceAtIsValidDate")).First();
            Action dailyFrecuency = () =>
            {
                occursOnceValidator.Invoke(null, new object[] { false, DateTime.MinValue });
            };
            dailyFrecuency.Should().NotThrow();
        }

        [Fact]
        public void if_occurs_every_is_true_ensure_start_end_dates_are_valid_dates_should_throw_exception_if_start_date_is_not_valid_date()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureStartEndDatesAreValidIfOccursEvery")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { true, DateTime.MinValue, endsAt });
            }
            catch (TargetInvocationException e)
            {
                e.InnerException.Should().BeOfType<InvalidDateException>();
            }
        }

        [Fact]
        public void if_occurs_every_is_true_ensure_start_end_dates_are_valid_dates_should_throw_exception_if_end_date_is_not_valid_date()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureStartEndDatesAreValidIfOccursEvery")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { true, startingAt, DateTime.MaxValue });
            }
            catch (TargetInvocationException e)
            {
                e.InnerException.Should().BeOfType<InvalidDateException>();
            }
        }

        [Fact]
        public void if_occurs_every_is_false_ensure_start_end_dates_are_valid_dates_should_not_validate_dates()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureStartEndDatesAreValidIfOccursEvery")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { false, DateTime.MinValue, DateTime.MaxValue });
            }
            catch (TargetInvocationException e)
            {
                e.InnerException.Should().BeOfType<InvalidDateException>();
            }
        }

        [Fact]
        public void if_occurs_every_is_false_ensure_start_end_dates_are_valid_dates_should_not_throw_exception_if_valid_dates_and_is_valid_range()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureStartEndDatesAreValidIfOccursEvery")).First();
            Action dailyFrecuency = () =>
            {
                occursOnceValidator.Invoke(null, new object[] { true, startingAt, endsAt });
            };
            dailyFrecuency.Should().NotThrow();
        }

        [Fact]
        public void if_occurs_every_is_false_ensure_start_end_dates_are_valid_dates_should_throw_exception_if_valid_dates_and_is_not_valid_range()
        {
            var methods = typeof(DailyFrecuency).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var occursOnceValidator = methods.Where(name => name.Name.Equals("EnsureStartEndDatesAreValidIfOccursEvery")).First();
            try
            {
                occursOnceValidator.Invoke(null, new object[] { false, endsAt, startingAt });
            }
            catch (TargetInvocationException e)
            {
                e.InnerException.Should().BeOfType<InvalidDateException>();
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
