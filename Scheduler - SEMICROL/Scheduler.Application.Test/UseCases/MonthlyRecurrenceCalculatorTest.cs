using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class MonthlyRecurrenceCalculatorTest
    {
        [Fact]
        public void get_all_recurrences_should_throw_exception_if_every_lt_zero()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = -1;

            Action validate = () =>
            {
                DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);
            };

            validate.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void get_all_recurrences_should_throw_exception_if_every_eq_zero()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = 0;

            Action validate = () =>
            {
                DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);
            };

            validate.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void get_all_recurrences_should_throw_exception_if_current_date_before_limit_start_date()
        {
            DateTime current = new DateTime(2019, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = 1;

            Action validate = () =>
            {
                DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);
            };

            validate.Should().Throw<DateRangeException>();
        }

        [Fact]
        public void get_all_recurrences_should_throw_exception_if_current_date_after_limit_end_date()
        {
            DateTime current = new DateTime(2023, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = 1;

            Action validate = () =>
            {
                DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);
            };

            validate.Should().Throw<DateRangeException>();
        }

        [Fact]
        public void valid_input_parameters_should_not_throw_exception()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = 1;

            Action validate = () =>
            {
                DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);
            };

            validate.Should().NotThrow();
        }

        [Fact]
        public void get_all_recurrences_every_1_should_add_1_month_to_current_date_until_end_limit_date_included()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = 1;

            var result = MonthlyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);

            result[0].Should().Be(new DateTime(2021, 01, 01));
            result[1].Should().Be(new DateTime(2021, 02, 01));
            result[2].Should().Be(new DateTime(2021, 03, 01));
            result[3].Should().Be(new DateTime(2021, 04, 01));
            result[4].Should().Be(new DateTime(2021, 05, 01));
            result[5].Should().Be(new DateTime(2021, 06, 01));
            result[6].Should().Be(new DateTime(2021, 07, 01));
            result[7].Should().Be(new DateTime(2021, 08, 01));
            result[8].Should().Be(new DateTime(2021, 09, 01));
            result[9].Should().Be(new DateTime(2021, 10, 01));
        }

        [Fact]
        public void get_all_recurrences_every_4_should_add_4_months_to_current_date_until_end_limit_date_included()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2022, 01, 01);
            int every = 4;

            var result = MonthlyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);

            result[0].Should().Be(new DateTime(2021, 01, 01));
            result[1].Should().Be(new DateTime(2021, 05, 01));
            result[2].Should().Be(new DateTime(2021, 09, 01));
        }

        [Fact]
        public void get_all_recurrences_should_return_different_year_dates_if_adding_months()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2023, 01, 01);
            int every = 4;

            var result = MonthlyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);

            result[0].Should().Be(new DateTime(2021, 01, 01));
            result[1].Should().Be(new DateTime(2021, 05, 01));
            result[2].Should().Be(new DateTime(2021, 09, 01));
            result[3].Should().Be(new DateTime(2022, 01, 01));
            result[4].Should().Be(new DateTime(2022, 05, 01));
            result[5].Should().Be(new DateTime(2022, 09, 01));
            result[6].Should().Be(new DateTime(2023, 01, 01));
        }
    }
}
