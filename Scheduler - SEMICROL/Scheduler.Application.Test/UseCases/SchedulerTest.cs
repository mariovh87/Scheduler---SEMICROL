using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class SchedulerTest
    {
        [Fact]
        public void calculate_output_configuration_type_once_should_throw_exception_if_occurs_once_date_is_out_of_limits_dates()
        {
            Input input = new Input(new DateTime(2020, 01, 01));
            Configuration config = new Configuration(true, new DateTime(2022, 01, 01), ConfigurationType.Once, RecurringType.Weekly);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, 2, DailyRecurrence.Hours, new TimeOnly(04, 00, 00), new TimeOnly(08, 00, 00));
            WeeklyConfiguration weeklyConfiguration = new WeeklyConfiguration(2);
            Limits limits = new Limits(new DateTime(2021, 01, 01), new DateTime(2021, 01, 15));
          
            Action validate = () =>
            {
                SchedulerOutputCalculator.CalculateOutput(input, config, dailyFrecuency, weeklyConfiguration, limits);
            };
            validate.Should().Throw<DateRangeException>();
        }

        [Fact]
        public void calculate_output_configuration_type_once_should_not_throw_exception_if_occurs_once_date_is_in_limits_dates()
        {
            Input input = new Input(new DateTime(2020, 01, 01));
            Configuration config = new Configuration(true, new DateTime(2021, 01, 10), ConfigurationType.Once, RecurringType.Weekly);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, 2, DailyRecurrence.Hours, new TimeOnly(04, 00, 00), new TimeOnly(08, 00, 00));
            WeeklyConfiguration weeklyConfiguration = new WeeklyConfiguration(2);
            Limits limits = new Limits(new DateTime(2021, 01, 01), new DateTime(2021, 01, 15));

            Action validate = () =>
            {
                SchedulerOutputCalculator.CalculateOutput(input, config, dailyFrecuency, weeklyConfiguration, limits);
            };
            validate.Should().NotThrow<DateRangeException>();
        }

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
            Limits limits = new Limits(new DateTime(2021,01,01), new DateTime(2021,01,15));

            var result = SchedulerOutputCalculator.CalculateOutput(input, config, dailyFrecuency, weeklyConfiguration,limits);

            result.Description.Should().Be("Occurs only once at 01/01/2021");
            result.ExecutionTime.Should().ContainEquivalentOf(config.OnceTimeAt.Value);
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
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);


            var result = SchedulerOutputCalculator.CalculateOutput(input, config, dailyFrecuency, weeklyConfig, limits);

            result.ExecutionTime[0].Should().Be(new DateTime(2020, 12, 28, 01, 30, 00));
            result.ExecutionTime[1].Should().Be(new DateTime(2020, 12, 28, 03, 30, 00));
            result.ExecutionTime[2].Should().Be(new DateTime(2020, 12, 28, 05, 30, 00));
            result.ExecutionTime[3].Should().Be(new DateTime(2020, 12, 28, 07, 30, 00));
            result.ExecutionTime[4].Should().Be(new DateTime(2020, 12, 30, 01, 30, 00));
            result.ExecutionTime[5].Should().Be(new DateTime(2020, 12, 30, 03, 30, 00));
            result.ExecutionTime[6].Should().Be(new DateTime(2020, 12, 30, 05, 30, 00));
            result.ExecutionTime[7].Should().Be(new DateTime(2020, 12, 30, 07, 30, 00));
            result.ExecutionTime[8].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result.ExecutionTime[9].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result.ExecutionTime[10].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result.ExecutionTime[11].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result.ExecutionTime[12].Should().Be(new DateTime(2021, 01, 03, 01, 30, 00));
            result.ExecutionTime[13].Should().Be(new DateTime(2021, 01, 03, 03, 30, 00));
            result.ExecutionTime[14].Should().Be(new DateTime(2021, 01, 03, 05, 30, 00));
            result.ExecutionTime[15].Should().Be(new DateTime(2021, 01, 03, 07, 30, 00));
            result.ExecutionTime[16].Should().Be(new DateTime(2021, 01, 11, 01, 30, 00));
            result.ExecutionTime[17].Should().Be(new DateTime(2021, 01, 11, 03, 30, 00));
            result.ExecutionTime[18].Should().Be(new DateTime(2021, 01, 11, 05, 30, 00));
            result.ExecutionTime[19].Should().Be(new DateTime(2021, 01, 11, 07, 30, 00));
            result.ExecutionTime[20].Should().Be(new DateTime(2021, 01, 13, 01, 30, 00));
            result.ExecutionTime[21].Should().Be(new DateTime(2021, 01, 13, 03, 30, 00));
            result.ExecutionTime[22].Should().Be(new DateTime(2021, 01, 13, 05, 30, 00));
            result.ExecutionTime[23].Should().Be(new DateTime(2021, 01, 13, 07, 30, 00));
            result.ExecutionTime[24].Should().Be(new DateTime(2021, 01, 15, 01, 30, 00));
            result.ExecutionTime[25].Should().Be(new DateTime(2021, 01, 15, 03, 30, 00));
            result.ExecutionTime[26].Should().Be(new DateTime(2021, 01, 15, 05, 30, 00));
            result.ExecutionTime[27].Should().Be(new DateTime(2021, 01, 15, 07, 30, 00));
            result.Description.Should().Be("Occurs every 2 weeks on Monday,Wednesday,Friday and Sunday every 2 Hours between 1:30:00 and 8:30:00");
        }
    }
}
