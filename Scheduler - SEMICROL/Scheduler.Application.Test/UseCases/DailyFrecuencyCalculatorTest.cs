using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using System;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class DailyFrecuencyCalculatorTest
    {
        private readonly DateTime inputDate = new DateTime(2021,01,01,00,00,00);

        [Fact]
        public void add_time_should_add_seconds_when_daily_recurrence_is_seconds()
        {
            int occursEvery = 20;
            DateTime expected = new DateTime(2021, 01, 01, 00, 00, occursEvery);
            DailyFrecuencyCalculator.AddTime(inputDate, DailyRecurrence.Seconds, occursEvery).Should().Be(expected);
        }

        [Fact]
        public void add_time_should_add_minutes_when_daily_recurrence_is_minutes()
        {
            int occursEvery = 20;
            DateTime expected = new DateTime(2021, 01, 01, 00, occursEvery, 00);
            DailyFrecuencyCalculator.AddTime(inputDate, DailyRecurrence.Minutes, occursEvery).Should().Be(expected);
        }

        [Fact]
        public void add_time_should_add_hours_when_daily_recurrence_is_hours()
        {
            int occursEvery = 20;
            DateTime expected = new DateTime(2021, 01, 01, occursEvery, 00, 00);
            DailyFrecuencyCalculator.AddTime(inputDate, DailyRecurrence.Hours, occursEvery).Should().Be(expected);
        }

        [Fact]
        public void get_executions_should_return_list_adding_every_minutes_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 00, 00);
            TimeOnly end = new TimeOnly(02, 10, 00);
            int occursEvery = 15;
            DailyRecurrence every = DailyRecurrence.Minutes;

            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 15, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 30, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 45, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 02, 00, 00));
        }

        [Fact]
        public void get_executions_should_return_list_adding_every_seconds_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(01, 33, 00);
            int occursEvery = 30;
            DailyRecurrence every = DailyRecurrence.Seconds;

            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 30, 30));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 31, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 31, 30));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 32, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 32, 30));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 33, 00));
        }

        [Fact]
        public void get_executions_should_return_list_adding_every_hour_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 01, 30, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 03, 30, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 05, 30, 00));
            DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery).Should().Contain(new DateTime(2021, 01, 01, 07, 30, 00));

        }
    }
}
