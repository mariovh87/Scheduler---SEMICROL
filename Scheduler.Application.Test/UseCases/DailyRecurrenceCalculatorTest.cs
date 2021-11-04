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
    public class DailyRecurrenceCalculatorTest
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
        public void get_all_recurrences_every_1_should_add_1_day_to_current_date_until_end_limit_date_included()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2021, 01, 10);
            int every = 1;

            var result = DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);

            result[0].Should().Be(new DateTime(2021, 01, 01));
            result[1].Should().Be(new DateTime(2021, 01, 02));
            result[2].Should().Be(new DateTime(2021, 01, 03));
            result[3].Should().Be(new DateTime(2021, 01, 04));
            result[4].Should().Be(new DateTime(2021, 01, 05));
            result[5].Should().Be(new DateTime(2021, 01, 06));
            result[6].Should().Be(new DateTime(2021, 01, 07));
            result[7].Should().Be(new DateTime(2021, 01, 08));
            result[8].Should().Be(new DateTime(2021, 01, 09));
            result[9].Should().Be(new DateTime(2021, 01, 10));
        }

        [Fact]
        public void get_all_recurrences_every_4_should_add_4_days_to_current_date_until_end_limit_date_included()
        {
            DateTime current = new DateTime(2021, 01, 01);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2021, 01, 20);
            int every = 4;

            var result = DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);

            result[0].Should().Be(new DateTime(2021, 01, 01));
            result[1].Should().Be(new DateTime(2021, 01, 05));
            result[2].Should().Be(new DateTime(2021, 01, 09));
            result[3].Should().Be(new DateTime(2021, 01, 13));
            result[4].Should().Be(new DateTime(2021, 01, 17));
        }

        [Fact]
        public void get_all_recurrences_should_add_days_and_change_month()
        {
            DateTime current = new DateTime(2021, 01, 31);
            DateTime startLimit = new DateTime(2020, 01, 01);
            DateTime endLimit = new DateTime(2021, 02, 10);
            int every = 1;

            var result = DailyRecurrenceCalculator.GetAllRecurrences(current, startLimit, endLimit, every);

            result[0].Should().Be(new DateTime(2021, 01, 31));
            result[1].Should().Be(new DateTime(2021, 02, 01));
            result[2].Should().Be(new DateTime(2021, 02, 02));
            result[3].Should().Be(new DateTime(2021, 02, 03));
            result[4].Should().Be(new DateTime(2021, 02, 04));
            result[5].Should().Be(new DateTime(2021, 02, 05));
            result[6].Should().Be(new DateTime(2021, 02, 06));
            result[7].Should().Be(new DateTime(2021, 02, 07));
            result[8].Should().Be(new DateTime(2021, 02, 08));
            result[9].Should().Be(new DateTime(2021, 02, 09));
        }
    }
}
