using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using System.Collections.Generic;
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

            var result = DailyFrecuencyCalculator.AddTime(inputDate, DailyRecurrence.Seconds, occursEvery);

            result.Should().Be(new DateTime(2021, 01, 01, 00, 00, occursEvery));
        }

        [Fact]
        public void add_time_should_add_minutes_when_daily_recurrence_is_minutes()
        {
            int occursEvery = 20;

            var result = DailyFrecuencyCalculator.AddTime(inputDate, DailyRecurrence.Minutes, occursEvery);
            
            result.Should().Be(new DateTime(2021, 01, 01, 00, occursEvery, 00));
        }

        [Fact]
        public void add_time_should_add_hours_when_daily_recurrence_is_hours()
        {
            int occursEvery = 20;

            var result = DailyFrecuencyCalculator.AddTime(inputDate, DailyRecurrence.Hours, occursEvery);
                
            result.Should().Be(new DateTime(2021, 01, 01, occursEvery, 00, 00));
        }

        [Fact]
        public void get_executions_should_return_list_adding_every_minutes_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 00, 00);
            TimeOnly end = new TimeOnly(02, 10, 00);
            int occursEvery = 15;
            DailyRecurrence every = DailyRecurrence.Minutes;

            var result = DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 00, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 01, 15, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 01, 45, 00));
            result[4].Should().Be(new DateTime(2021, 01, 01, 02, 00, 00));
        }

        [Fact]
        public void get_executions_should_return_list_adding_every_seconds_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(01, 33, 00);
            int occursEvery = 30;
            DailyRecurrence every = DailyRecurrence.Seconds;

            var result = DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 01, 30, 30));
            result[2].Should().Be(new DateTime(2021, 01, 01, 01, 31, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 01, 31, 30));
            result[4].Should().Be(new DateTime(2021, 01, 01, 01, 32, 00));
            result[5].Should().Be(new DateTime(2021, 01, 01, 01, 32, 30));
            result[6].Should().Be(new DateTime(2021, 01, 01, 01, 33, 00));
        }

        [Fact]
        public void get_executions_should_return_list_adding_every_hour_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

           var result = DailyFrecuencyCalculator.GetExecutions(executionDate, starting, end, every, occursEvery);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
        }

        [Fact]
        public void get_executions_recieving_daily_frecuency_should_return_list_adding_every_minutes_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 00, 00);
            TimeOnly end = new TimeOnly(02, 10, 00);
            int occursEvery = 15;
            DailyRecurrence every = DailyRecurrence.Minutes;
            DailyFrecuency frecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = DailyFrecuencyCalculator.GetExecutions(executionDate, frecuency);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 00, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 01, 15, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 01, 45, 00));
            result[4].Should().Be(new DateTime(2021, 01, 01, 02, 00, 00));
        }

        [Fact]
        public void get_executions_recieving_daily_frecuency_should_return_list_adding_every_seconds_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(01, 33, 00);
            int occursEvery = 30;
            DailyRecurrence every = DailyRecurrence.Seconds;
            DailyFrecuency frecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = DailyFrecuencyCalculator.GetExecutions(executionDate, frecuency);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 01, 30, 30));
            result[2].Should().Be(new DateTime(2021, 01, 01, 01, 31, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 01, 31, 30));
            result[4].Should().Be(new DateTime(2021, 01, 01, 01, 32, 00));
            result[5].Should().Be(new DateTime(2021, 01, 01, 01, 32, 30));
            result[6].Should().Be(new DateTime(2021, 01, 01, 01, 33, 00));
        }

        [Fact]
        public void get_executions_recieving_daily_frecuency_should_return_list_adding_every_hour_to_time_from_starting_to_end()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency frecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = DailyFrecuencyCalculator.GetExecutions(executionDate, frecuency);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
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

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency);
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

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency);
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

            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, null, occursEvery, every, starting, null);
            Input input = new Input(executionDate);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateDailyFrecuency(dailyFrecuency);
            };

            validate.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void validate_calculate_occurs_once_should_throw_exception_if_occurs_once_is_false_and_occurs_every_is_true()
        {
            TimeOnly occursOnceAt = new TimeOnly(05, 30, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, occursOnceAt, 2, every, starting, end);
            Input input = new Input(new DateTime(2020, 10, 10));

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateOccursOnce(dailyFrecuency);
            };

            validate.Should().Throw<ArgumentException>();
        }


        [Fact]
        public void validate_calculate_occurs_once_should_not_throw_exception_if_occurs_once_is_true_and_occurs_every_is_false()
        {
            TimeOnly occursOnceAt = new TimeOnly(05, 30, 00);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, occursOnceAt, 2, every, starting, end);
            Input input = new Input(new DateTime(2020, 10, 10));

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateOccursOnce(dailyFrecuency);
            };

            validate.Should().NotThrow<DomainException>();
        }


        [Fact]
        public void set_occurs_once_time_to_date_should_set_time_only_as_date_time_time_fraction()
        {
            DateTime executionDate = new DateTime(2021, 01, 01, 00, 00, 00);
            TimeOnly occursOnce = new TimeOnly(01, 30, 00);

            var result = DailyFrecuencyCalculator.SetOccursOnceTimeToDate(executionDate, occursOnce);
                
            result.Should().Be(new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, occursOnce.Hour, occursOnce.Minute, occursOnce.Second));
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
            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, occursOnce, 10, DailyRecurrence.Hours, null, null);

            Action validate = () =>
            {
                DailyFrecuencyCalculator.ValidateCalculateOccursOnce(dailyFrecuency);
            };

            validate.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void calculate_occurs_once_should_contains_one_execution()
        {
            TimeOnly occursOnce = new TimeOnly(10, 30, 00);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, occursOnce, 10, DailyRecurrence.Hours, null, null);
            Input input = new Input(new DateTime(2021, 01, 01));
            
            var output = DailyFrecuencyCalculator.CalculateOccursOnce(dailyFrecuency, input);
            
            output.ExecutionTime.Count.Should().Be(1);
        }

        [Fact]
        public void calculate_occurs_once_should_contains_one_execution_equivalent_to_input_currrent_date_with_time_section_occurs_once()
        {
            TimeOnly occursOnce = new TimeOnly(10, 30, 00);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(true, false, occursOnce, 10, DailyRecurrence.Hours, null, null);
            Input input = new Input(new DateTime(2021, 01, 01));

            var output = DailyFrecuencyCalculator.CalculateOccursOnce(dailyFrecuency, input);

            output.ExecutionTime[0].Should().Be(new DateTime(2021, 01, 01, 10, 30, 00));
        }
    }
}
