using FluentAssertions;
using Scheduler.Application.CaseUses;
using Scheduler.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Application.Test.UseCases
{
    public class NextDateCalculatorTest
    {
        [Fact]
        public void calculate_output_should_return_configuration_date_when_configuration_type_is_once()
        {
            Domain.Entities.Scheduler scheduler = new Domain.Entities.Scheduler(new Input(new DateTime(2020, 01, 01))
                   , new Configuration(true, new DateTime(2021, 10, 10), 1, ConfigurationType.Once, RecurringType.Daily)
                   , new Limits(new DateTime(2015, 02, 02), null));

            Output output = NextDateCalculator.CalculateOutput(scheduler);
            output.NextExecution().Should().Be(new DateTime(2021, 10, 10));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(20)]
        public void calculate_output_should_add_days_when_configuration_type_is_recurring_daily(int every)
        {
            Domain.Entities.Scheduler scheduler = new Domain.Entities.Scheduler(new Input(new DateTime(2020, 01, 01))
                   , new Configuration(true, new DateTime(2021, 10, 10), every, ConfigurationType.Recurring, RecurringType.Daily)
                   , new Limits(new DateTime(2015, 02, 02), new DateTime(2022, 02, 02)));

            Output output = NextDateCalculator.CalculateOutput(scheduler);
            output.NextExecution().Should().Be(new DateTime(2020, 01, 01).AddDays(every));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(20)]
        public void calculate_output_should_add_months_when_configuration_type_is_recurring_monthly(int every)
        {
            Domain.Entities.Scheduler scheduler = new Domain.Entities.Scheduler(new Input(new DateTime(2020, 01, 01))
                   , new Configuration(true, new DateTime(2021, 10, 10), every, ConfigurationType.Recurring, RecurringType.Monthly)
                   , new Limits(new DateTime(2015, 02, 02), new DateTime(2022, 02, 02)));

            Output output = NextDateCalculator.CalculateOutput(scheduler);
            output.NextExecution().Should().Be(new DateTime(2020, 01, 01).AddMonths(every));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(20)]
        public void calculate_output_should_add_years_when_configuration_type_is_recurring_yearly(int every)
        {
            Domain.Entities.Scheduler scheduler = new Domain.Entities.Scheduler(new Input(new DateTime(2020, 01, 01))
                   , new Configuration(true, new DateTime(2021, 10, 10), every, ConfigurationType.Recurring, RecurringType.Yearly)
                   , new Limits(new DateTime(2015, 02, 02), new DateTime(2022, 02, 02)));

            Output output = NextDateCalculator.CalculateOutput(scheduler);
            output.NextExecution().Should().Be(new DateTime(2020, 01, 01).AddYears(every));
        }
    }
}
