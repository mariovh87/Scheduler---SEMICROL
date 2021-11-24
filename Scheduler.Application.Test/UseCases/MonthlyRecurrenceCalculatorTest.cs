using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using Xunit;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class MonthlyRecurrenceCalculatorTest
    {
        [Fact]
        public void check_if_config_day_is_after_equals_current_start_date_should_throw_exception_if_date_is_not_valid()
        {
            DateTime current = DateTime.MinValue;
            int every = 1;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void check_if_config_day_is_after_equals_current_start_date_should_throw_exception_if_every_is_not_positive()
        {
            DateTime current = new DateTime(2021, 01, 01);
            int every = -1;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void check_if_config_day_is_after_equals_current_start_date_should_throw_exception_if_every_is_greather_than_31()
        {
            DateTime current = new DateTime(2021, 01, 01);
            int every = 32;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void check_if_config_day_is_after_equals_current_start_date_should_not_throw_exception_if_every_is_day_range()
        {
            DateTime current = new DateTime(2021, 01, 01);
            int every = 31;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().NotThrow();
        }

        [Fact]
        public void check_if_config_day_is_after_equals_current_start_date_should_be_true_if_every_is_gte_current_day()
        {
            DateTime current = new DateTime(2021, 01, 10);
            int every = 31;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().NotThrow();
        }


        [Fact]
        public void check_if_month_have_day_should_return_true_if_last_month_day_is_bigger_than_every_day()
        {
            DateTime current = new DateTime(2021, 01, 10);
            int every = 31;

            var result = MonthlyRecurrenceCalculator.CheckIfMonthHaveDay(current, every);

            result.Should().BeTrue();
        }
        [Fact]
        public void check_if_month_have_day_should_return_false_if_last_month_day_is_lesser_than_every_day()
        {
            DateTime current = new DateTime(2021, 04, 10);
            int every = 31;

            var result = MonthlyRecurrenceCalculator.CheckIfMonthHaveDay(current, every);

            result.Should().BeFalse();
        }

        [Fact]
        public void check_if_month_have_day_should_return_false_if_month_february_and_day_30()
        {
            DateTime current = new DateTime(2021, 02, 10);
            int every = 30;

            var result = MonthlyRecurrenceCalculator.CheckIfMonthHaveDay(current, every);

            result.Should().BeFalse();
        }

        [Fact]
        public void get_first_day_should_return_next_date_with_months_added_if_day_is_before_current_day()
        {
            DateTime current = new DateTime(2021, 01, 30);
            int every = 20;
            int everyMonths = 2;
            DateTime limitDate = new DateTime(2022, 01, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(new DateTime(2021,03,20));
        }

        [Fact]
        public void get_first_day_should_return_max_value_if_not_exist_date_with_added_months()
        {
            DateTime current = new DateTime(2021, 01, 30);
            int every = 20;
            int everyMonths = 5;
            DateTime limitDate = new DateTime(2021, 04, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(DateTime.MaxValue);
        }

        [Fact]
        public void get_first_day_should_return_add_months_to_current_date_with_last_day_month()
        {
            DateTime current = new DateTime(2021, 01, 31);
            int every = 30;
            int everyMonths = 1;
            DateTime limitDate = new DateTime(2021, 04, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(new DateTime(2021, 02, 28));
        }

        [Fact]
        public void get_first_day_should_return_date_in_same_month_if_every_day_is_after_current_date_day()
        {
            DateTime current = new DateTime(2021, 01, 10);
            int every = 30;
            int everyMonths = 1;
            DateTime limitDate = new DateTime(2021, 01, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(new DateTime(2021, 01, 30));
        }

        [Fact]
        public void get_day_recurrences_should_return_dates_with_every_month_added() 
        { 
            DateTime current = new DateTime(2021, 01, 01);
            DateTime limitStart = new DateTime(2021, 01, 01);
            DateTime limitEnd = new DateTime(2022, 01, 01);
            Limits limits = new (limitStart, limitEnd);
            int everyDay = 10;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 10));
            result[1].Should().BeSameDateAs(new DateTime(2021, 02, 10));
            result[2].Should().BeSameDateAs(new DateTime(2021, 03, 10));
            result[3].Should().BeSameDateAs(new DateTime(2021, 04, 10));
            result[4].Should().BeSameDateAs(new DateTime(2021, 05, 10));
            result[5].Should().BeSameDateAs(new DateTime(2021, 06, 10));
            result[6].Should().BeSameDateAs(new DateTime(2021, 07, 10));
            result[7].Should().BeSameDateAs(new DateTime(2021, 08, 10));
            result[8].Should().BeSameDateAs(new DateTime(2021, 09, 10));
            result[9].Should().BeSameDateAs(new DateTime(2021, 10, 10));
            result[10].Should().BeSameDateAs(new DateTime(2021, 11, 10));
            result[11].Should().BeSameDateAs(new DateTime(2021, 12, 10));
        }

        [Fact]
        public void get_day_recurrences_should_return_dates_with_every_3_month_added()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime limitStart = new DateTime(2021, 01, 01);
            DateTime limitEnd = new DateTime(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 10;
            int everyMonths = 3;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(4);
            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 10));
            result[1].Should().BeSameDateAs(new DateTime(2021, 04, 10));
            result[2].Should().BeSameDateAs(new DateTime(2021, 07, 10));
            result[3].Should().BeSameDateAs(new DateTime(2021, 10, 10));
        }

        [Fact]
        public void get_day_recurrences_should_return_dates_month_last_day()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime limitStart = new DateTime(2021, 01, 01);
            DateTime limitEnd = new DateTime(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 31;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(12);
            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 31));
            result[1].Should().BeSameDateAs(new DateTime(2021, 02, 28));
            result[2].Should().BeSameDateAs(new DateTime(2021, 03, 31));
            result[3].Should().BeSameDateAs(new DateTime(2021, 04, 30));
            result[4].Should().BeSameDateAs(new DateTime(2021, 05, 31));
            result[5].Should().BeSameDateAs(new DateTime(2021, 06, 30));
            result[6].Should().BeSameDateAs(new DateTime(2021, 07, 31));
            result[7].Should().BeSameDateAs(new DateTime(2021, 08, 31));
            result[8].Should().BeSameDateAs(new DateTime(2021, 09, 30));
            result[9].Should().BeSameDateAs(new DateTime(2021, 10, 31));
            result[10].Should().BeSameDateAs(new DateTime(2021, 11, 30));
            result[11].Should().BeSameDateAs(new DateTime(2021, 12, 31));
        }

        [Fact]
        public void get_day_recurrences_should_not_return_current_date_month_if_every_is_before_current_date_day()
        {
            DateTime current = new DateTime(2021, 01, 31);
            DateTime limitStart = new DateTime(2021, 01, 01);
            DateTime limitEnd = new DateTime(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 20;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(11);
            result[0].Should().BeSameDateAs(new DateTime(2021, 02, 20));
            result[1].Should().BeSameDateAs(new DateTime(2021, 03, 20));
            result[2].Should().BeSameDateAs(new DateTime(2021, 04, 20));
            result[3].Should().BeSameDateAs(new DateTime(2021, 05, 20));
            result[4].Should().BeSameDateAs(new DateTime(2021, 06, 20));
            result[5].Should().BeSameDateAs(new DateTime(2021, 07, 20));
            result[6].Should().BeSameDateAs(new DateTime(2021, 08, 20));
            result[7].Should().BeSameDateAs(new DateTime(2021, 09, 20));
            result[8].Should().BeSameDateAs(new DateTime(2021, 10, 20));
            result[9].Should().BeSameDateAs(new DateTime(2021, 11, 20));
            result[10].Should().BeSameDateAs(new DateTime(2021, 12, 20));
        }

        [Fact]
        public void get_day_recurrences_should_return_dates_if_adding_months_change_year()
        {
            DateTime current = new DateTime(2021, 10, 31);
            DateTime limitStart = new DateTime(2021, 01, 01);
            DateTime limitEnd = new DateTime(2022, 04, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 20;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(5);
            result[0].Should().BeSameDateAs(new DateTime(2021, 11, 20));
            result[1].Should().BeSameDateAs(new DateTime(2021, 12, 20));
            result[2].Should().BeSameDateAs(new DateTime(2022, 01, 20));
            result[3].Should().BeSameDateAs(new DateTime(2022, 02, 20));
            result[4].Should().BeSameDateAs(new DateTime(2022, 03, 20));
        }
    }
}
