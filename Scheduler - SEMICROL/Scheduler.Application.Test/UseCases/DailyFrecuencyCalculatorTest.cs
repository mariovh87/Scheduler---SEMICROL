using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
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

        [Fact]
        public void validate_calculate_daily_frecuency_should_return_throw_exception_if_occurs_once_is_true()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, null, occursEvery, every, starting, end);
            Input input = new Input(executionDate);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency, input);
            };
            
            validate.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void validate_calculate_daily_frecuency_should_return_throw_exception_if_starting_is_not_valid_value()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, null, end);
            Input input = new Input(executionDate);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency, input);
            };

            validate.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void validate_calculate_daily_frecuency_should_return_throw_exception_if_ends_is_not_valid_value()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, null);
            Input input = new Input(executionDate);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency, input);
            };

            validate.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void calculate_daily_frecuency_should_return_throw_exception_if_ends_is_not_valid_value()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, null);
            Input input = new Input(executionDate);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.CalculateDailyFrecuencyExecutions(dailyFrecuency, input);
            };

            validate.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void calculate_daily_frecuency_should_return_throw_exception_if_starting_is_not_valid_value()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, null, end);
            Input input = new Input(executionDate);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency, input);
            };

            validate.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void calculate_daily_frecuency_should_return_output_with_populated_execution_list()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);
            Input input = new Input(executionDate);

            Output output = DailyFrecuencyCalculator.CalculateDailyFrecuencyExecutions(dailyFrecuency, input);
            output.ExecutionTime.Should().Contain(new DateTime(2021, 01, 01, 01, 30, 00));
            output.ExecutionTime.Should().Contain(new DateTime(2021, 01, 01, 03, 30, 00));
            output.ExecutionTime.Should().Contain(new DateTime(2021, 01, 01, 05, 30, 00));
            output.ExecutionTime.Should().Contain(new DateTime(2021, 01, 01, 07, 30, 00));
        }

        [Fact]
        public void set_occurs_once_time_to_date_should_set_time_only_as_date_time_time_fraction()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly occursOnce = new TimeOnly(01, 30, 00);
            DateTime expected = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, occursOnce.Hour, occursOnce.Minute, occursOnce.Second);

            DailyFrecuencyCalculator.SetOccursOnceTimeToDate(executionDate, occursOnce).Should().Be(expected);
        }

        [Fact]
        public void validate_calculate_occurs_once_should_throw_exception_if_occurs_once_at_is_null()
        {
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, 10, DailyRecurrence.Hours, null, null);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateOccursOnce(dailyFrecuency);
            };

            validate.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void validate_calculate_occurs_once_should_not_throw_exception_if_occurs_once_at_is_null()
        {
            TimeOnly occursOnce = new TimeOnly(10, 30, 00);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, occursOnce, 10, DailyRecurrence.Hours, null, null);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateOccursOnce(dailyFrecuency);
            };

            validate.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void calculate_output_should_return_output_calculate_occurs_once_if_once()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            TimeOnly occursOnce = new TimeOnly(10, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, occursOnce, occursEvery, every, starting, end);
            Input input = new Input(executionDate);

            Output expected = DailyFrecuencyCalculator.CalculateOccursOnce(dailyFrecuency, input);
            DailyFrecuencyCalculator.CalculateOutput(dailyFrecuency, input).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void calculate_output_should_return_output_calculated_by_daily_frecuency_executions_if_if_occurs_every()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);
            Input input = new Input(executionDate);

            Output expected = DailyFrecuencyCalculator.CalculateDailyFrecuencyExecutions(dailyFrecuency, input);
            DailyFrecuencyCalculator.CalculateOutput(dailyFrecuency, input).Should().BeEquivalentTo(expected);
        }
    }
}
