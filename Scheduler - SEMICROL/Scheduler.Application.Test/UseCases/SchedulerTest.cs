using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class SchedulerTest
    {
        [Fact]
        public void calculate_output_should_return_output_with_only_one_date_if_executing_once()
        {
            Input input = new Input(new DateTime(2020, 01, 01));
            Configuration config = new Configuration(true, new DateTime(2021, 01, 01), ConfigurationType.Once, RecurringType.Weekly);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, 2, DailyRecurrence.Hours, new TimeOnly(04, 00, 00), new TimeOnly(08, 00, 00));
            WeeklyConfiguration weeklyConfiguration = new WeeklyConfiguration(2);
            weeklyConfiguration.DaysOfWeek.CheckMonday(true);
            weeklyConfiguration.DaysOfWeek.CheckWednesday(true);
            weeklyConfiguration.DaysOfWeek.CheckSaturday(true);
            string expected = "Occurs only once at 01/01/2021";
            Output outputExpected = new Output();
            outputExpected.Description = expected;
            Limits limits = new Limits(new DateTime(2021,01,01), new DateTime(2021,01,15));
            IList<DateTime> executions = new List<DateTime>();
            executions.Add(config.OnceTimeAt.Value);
            outputExpected.ExecutionTime = executions;

            Output output = SchedulerOutputCalculator.CalculateOutput(input, config, dailyFrecuency, weeklyConfiguration,limits);

            outputExpected.Description.Should().Be(output.Description);
            outputExpected.ExecutionTime.Should().BeEquivalentTo(output.ExecutionTime);
        }

        [Fact]
        public void calculate_output_should_return_output_with_execution_dates_if_executing_is_recurring()
        {
            Input input = new Input(new DateTime(2020, 12, 28));
            Configuration config = new Configuration(true, new DateTime(2021, 01, 01), ConfigurationType.Recurring, RecurringType.Weekly);

            Limits limits = new Limits(new DateTime(2021, 01, 01), new DateTime(2021, 01, 15));


            WeeklyConfiguration weeklyConfig = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 02);

            weeklyConfig.DaysOfWeek.CheckMonday(true);
            weeklyConfig.DaysOfWeek.CheckWednesday(true);
            weeklyConfig.DaysOfWeek.CheckFriday(true);
            weeklyConfig.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2020, 12, 28, 01, 30, 00));
            expected.Add(new DateTime(2020, 12, 28, 03, 30, 00));
            expected.Add(new DateTime(2020, 12, 28, 05, 30, 00));
            expected.Add(new DateTime(2020, 12, 28, 07, 30, 00));

            expected.Add(new DateTime(2020, 12, 30, 01, 30, 00));
            expected.Add(new DateTime(2020, 12, 30, 03, 30, 00));
            expected.Add(new DateTime(2020, 12, 30, 05, 30, 00));
            expected.Add(new DateTime(2020, 12, 30, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 03, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 11, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 13, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 15, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 07, 30, 00));


            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            string expectedDescription = "Occurs every 2 weeks on Monday,Wednesday,Friday and Sunday every 2 Hours between 1:30:00 and 8:30:00";

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            Output outputExpected = new Output();
            outputExpected.Description = expectedDescription;
            List<DateTime> executions = new List<DateTime>();
            executions.AddRange(expected);
            outputExpected.ExecutionTime = executions;

            Output output = SchedulerOutputCalculator.CalculateOutput(input, config, dailyFrecuency, weeklyConfig, limits);

            outputExpected.Description.Should().Be(output.Description);
            outputExpected.ExecutionTime.Should().BeEquivalentTo(output.ExecutionTime);
        }
    }
}
