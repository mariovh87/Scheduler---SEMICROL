using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Exceptions;
using Xunit;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Tests.Entities
{
    public class SchedulerTest
    {
        [Fact]
        public void input_null_should_throw_exception()
        {
            Action inputArgumentNull = () =>
            {
                new Domain.Entities.Scheduler(null
                    , new Configuration(true, new DateTime(01,01,2021), 1, ConfigurationType.Once, RecurringType.Daily)
                    , new Limits(new DateTime(01, 01, 2021), null));
            };
            inputArgumentNull.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void configuration_null_should_throw_exception()
        {
            Action configurationArgumentNull = () =>
            {
                new Domain.Entities.Scheduler(new Input(new DateTime(01,01,2021))
                    , null
                    , new Limits(new DateTime(01, 01, 2021), null));
            };
            configurationArgumentNull.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void limits_null_should_throw_exception()
        {
            Action limitsArgumentNull = () =>
            {
                new Domain.Entities.Scheduler(new Input(new DateTime(01, 01, 2021))
                    , new Configuration(true, new DateTime(01, 01, 2021), 1, ConfigurationType.Once, RecurringType.Daily)
                    , null);
            };
            limitsArgumentNull.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void input_date_outside_limits_should_throw_exception_when_is_recurring()
        {
            Input input = new Input(new DateTime(2020, 01, 01));
            Configuration config = new Configuration(true, new DateTime(2020, 01, 01), 1, ConfigurationType.Recurring, RecurringType.Daily);
            Limits limits = new Limits(new DateTime(2021, 01, 01), new DateTime(2025, 01, 01));
             Action validateInputDateLimits = () =>
             {
                 new Domain.Entities.Scheduler(input, config, limits);
             };
            validateInputDateLimits.Should().Throw<DateRangeException>();
        }

        [Fact]
        public void input_date_inside_limits_should_not_throw_exception_when_is_recurring()
        {
            Input input = new Input(new DateTime(2022, 01, 01));
            Configuration config = new Configuration(true, new DateTime(2022, 01, 01), 1, ConfigurationType.Recurring, RecurringType.Daily);
            Limits limits = new Limits(new DateTime(2021, 01, 01), new DateTime(2025, 01, 01));
            Type type = typeof(Domain.Entities.Scheduler);
            Action validateInputDateLimits = () =>
            {
                new Domain.Entities.Scheduler(input, config, limits);
            };
            validateInputDateLimits.Should().NotThrow();
        }
    }
}
