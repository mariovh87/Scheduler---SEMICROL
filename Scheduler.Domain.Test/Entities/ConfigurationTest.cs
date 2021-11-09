using FluentAssertions;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;
using System;


namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class ConfigurationTest
    {
        [Fact]
        public void if_config_type_is_once_datetime_is_null_should_validate_date_should_throw_exception()
        {
            Action act = () => Domain.Entities.Configuration.ValidateDateTime(true, null, ConfigurationType.Once);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void if_config_type_is_once_datetime_is_not_null_should_validate_date_should_not_throw_exception()
        {
            Action act = () => Domain.Entities.Configuration.ValidateDateTime(true, new DateTime(2021, 01, 01), ConfigurationType.Once);

            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void if_enabled_is_false_validate_date_should_not_validate_and_not_throw_exception()
        {
            Action act = () => Domain.Entities.Configuration.ValidateDateTime(false, null, ConfigurationType.Once);

            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void if_config_type_is_recurring_every_is_0_should_throw_exception()
        {
            Action act = () => Domain.Entities.Configuration.ValidateRecurrence(true, 0, ConfigurationType.Recurring);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void if_config_type_is_recurring_every_bigger_than_0_should_not_throw_exception()
        {
            Action act = () => Domain.Entities.Configuration.ValidateRecurrence(true, 3, ConfigurationType.Recurring);

            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void validate_recurrence_if_enabled_is_false_should_not_validate()
        {
            bool enabled = false;
            int every = 3;
            ConfigurationType configType = ConfigurationType.Recurring;

            Action act = () => Domain.Entities.Configuration.ValidateRecurrence(enabled, every, configType);

            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void validate_recurrence_if_type_is_once_should_not_validate()
        {
            bool enabled = false;
            int every = 3;
            ConfigurationType configType = ConfigurationType.Once;

            Action act = () => Domain.Entities.Configuration.ValidateRecurrence(enabled, every, configType);

            act.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void configuration_propertys_should_return_value_setted()
        {
            DateTime date = new DateTime(2021, 01, 01);
            ConfigurationType type = ConfigurationType.Once;
            RecurringType recurring = RecurringType.Daily;

            Domain.Entities.Configuration config = new Domain.Entities.Configuration(true, date, type, recurring);

            config.Enabled.Should().BeTrue();
            config.OnceTimeAt.Should().Be(date);    
            config.Type.Should().Be(type);  
            config.Occurs.Should().Be((int)RecurringType.Daily);
        }
    }
}
