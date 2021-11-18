using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class MonthlyConfigurationTest
    {
        [Fact]
        public void config_constructor_should_throw_exception_if_day_false()
        {
            bool day = false;
            int everyDay = 1;
            int dayMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void config_constructor_should_not_throw_exception_if_day_true()
        {
            bool day = true;
            int everyDay = 1;
            int dayMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths);

            act.Should().NotThrow();
        }

        [Fact]
        public void config_constructor_should_throw_exception_if_every_day_is_not_positive()
        {
            bool day = true;
            int everyDay = 0;
            int dayMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_if_day_months_is_not_positive()
        {
            bool day = true;
            int everyDay = 1;
            int dayMonths = 0;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_if_day_months_and_every_day_are_not_positive()
        {
            bool day = true;
            int everyDay = 0;
            int dayMonths = 0;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void the_config_constructor_should_throw_exception_if_the_is_false()
        {
            bool the = false;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(the, frecuency, dayType, everyMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void the_config_constructor_should_throw_exception_if_every_months_is_not_positive()
        {
            bool the = true;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 0;

            Action act = () => new MonthlyConfiguration(the, frecuency, dayType, everyMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void the_config_constructor_should_not_throw_exception_if_the_is_true_and_every_months_positive()
        {
            bool the = true;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(the, frecuency, dayType, everyMonths);

            act.Should().NotThrow();
        }

        [Fact]
        public void config_constructor_should_throw_exception_day_and_the_are_true()
        {
            bool day = true;
            int everyDay = 1;
            int dayMonths = 1;
            bool the = true;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day,everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_day_and_the_are_false()
        {
            bool day = false;
            int everyDay = 1;
            int dayMonths = 1;
            bool the = false;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_not_throw_exception_if_only_day_is_true()
        {
            bool day = true;
            int everyDay = 1;
            int dayMonths = 1;
            bool the = false;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().NotThrow<DomainException>();
        }


        [Fact]
        public void config_constructor_should_throw_exception_not_throw_exception_if_only_the_is_true()
        {
            bool day = false;
            int everyDay = 1;
            int dayMonths = 1;
            bool the = true;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().NotThrow<DomainException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_throw_exception_if_only_day_is_true_and_every_day_not_positive()
        {
            bool day = true;
            int everyDay = 0;
            int dayMonths = 1;
            bool the = false;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_throw_exception_if_only_day_is_true_and_day_months_not_positive()
        {
            bool day = true;
            int everyDay = 1;
            int dayMonths = 0;
            bool the = false;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void config_constructor_should_throw_exception_throw_exception_if_only_day_is_true_and_day_months_and_every_day_not_positive()
        {
            bool day = true;
            int everyDay = 0;
            int dayMonths = 0;
            bool the = false;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            Action act = () => new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void properties_should_return_value_passed_in_constructor()
        {
            bool day = false;
            int everyDay = 1;
            int dayMonths = 1;
            bool the = true;
            MonthlyFrecuency frecuency = MonthlyFrecuency.First;
            MonthlyDayType dayType = MonthlyDayType.Day;
            int everyMonths = 1;

            MonthlyConfiguration config = new MonthlyConfiguration(day, everyDay, dayMonths, the, frecuency, dayType, everyMonths);

            config.Day.Should().Be(day);
            config.EveryDay.Should().Be(everyDay);  
            config.The.Should().Be(the);
            config.DayMonths.Should().Be(dayMonths);
            config.Frecuency.Should().Be(frecuency);
            config.DayType.Should().Be(dayType);    
            config.EveryMonths.Should().Be(everyMonths);
        }
    }
}
