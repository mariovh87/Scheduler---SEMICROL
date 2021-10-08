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
    }
}
