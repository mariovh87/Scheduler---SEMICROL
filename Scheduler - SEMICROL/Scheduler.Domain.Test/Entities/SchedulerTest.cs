using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Scheduler.Domain.Entities;
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
        public void input_date_outside_limits_should_throw_exception()
        {
            Input input = new Input(new DateTime(01, 01, 2020));
            Configuration config = new Configuration(true, new DateTime(01, 01, 2021), 1, ConfigurationType.Once, RecurringType.Daily);
            Limits limits = new Limits(new DateTime(01, 01, 2021), new DateTime(01, 01, 2025));

            Type type = typeof(Domain.Entities.Scheduler);
            var scheduler = Activator.CreateInstance(type, input, config, limits);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "ValidateInputDateLimits" && x.IsPrivate)
            .First();


            Action validateInputDateLimits = () =>
            {
                method.Invoke(scheduler, new object[] { input, config, limits });
            };
            validateInputDateLimits.Should().Throw<ArgumentException>();

        }
    }
}
