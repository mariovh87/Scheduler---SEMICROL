using System;
using Xunit;
using FluentAssertions;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Exceptions;

namespace Semicrol.Scheduler.Domain.Test.Common
{
    public class DateTimeExtensionMethodsTest
    {
        [Fact]
        public void min_value_is_not_valid_date()
        {
            Action isValidFunc = () =>
            {
                DateTime.MinValue.EnsureIsValidDate();
            };
            isValidFunc.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void max_value_is_not_valid_date()
        {
            Action isValidFunc = () =>
            {
                DateTime.MaxValue.EnsureIsValidDate();
            };
            isValidFunc.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void null_value_is_not_valid_date()
        {
            Action isValidFunc = () =>
            {
                new DateTime?().EnsureIsValidDate();
            };
            isValidFunc.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void nullable_date_min_value_is_not_valid_date()
        {
            Action isValidFunc = () =>
            {
                new DateTime?(DateTime.MinValue).EnsureIsValidDate();
            };
            isValidFunc.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void nullable_date_max_value_is_not_valid_date()
        {
            Action isValidFunc = () =>
            {
                new DateTime?(DateTime.MaxValue).EnsureIsValidDate();
            };
            isValidFunc.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void ensure_value_valid_date_of_nullable_datetime_should_not_throw_exception_if_null()
        {
            Action isValidFunc = () =>
            {
                new DateTime?().EnsureValueIsValidDate();
            };
            isValidFunc.Should().NotThrow();
        }

        [Fact]
        public void ensure_value_valid_date_of_nullable_datetime_should_throw_exception_if_min_value()
        {
            Action isValidFunc = () =>
            {
                new DateTime?(DateTime.MinValue).EnsureValueIsValidDate();
            };
            isValidFunc.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void ensure_value_valid_date_of_nullable_datetime_should_throw_exception_if_max_value()
        {
            Action isValidFunc = () =>
            {
                new DateTime?(DateTime.MaxValue).EnsureValueIsValidDate();
            };
            isValidFunc.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void ensure_value_valid_date_of_nullable_datetime_should_not_throw_exception_if_valid_date_value()
        {
            Action isValidFunc = () =>
            {
                new DateTime?(new DateTime(2021,1,1)).EnsureValueIsValidDate();
            };
            isValidFunc.Should().NotThrow();
        }

        [Fact]
        public void is_valid_date_false_if_is_min_value()
        {
            var output = DateTimeExtensionMethods.IsValidDate(DateTime.MinValue);
            
            output.Should().BeFalse();
        }

        [Fact]
        public void is_valid_date_false_if_is_max_value()
        {
            var output = DateTimeExtensionMethods.IsValidDate(DateTime.MaxValue);
            
            output.Should().BeFalse();
        }

        [Fact]
        public void is_valid_date_true_if_is_not_min_value_or_max_value()
        {
            var output = DateTimeExtensionMethods.IsValidDate(new DateTime(2021, 01, 01));
            
            output.Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_true_if_is_min_value()
        {
            var output = DateTimeExtensionMethods.IsMinValue(DateTime.MinValue);
            
            output.Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_false_if_is_not_min_value()
        {
            var output = DateTimeExtensionMethods.IsMinValue(DateTime.MaxValue);
            
            output.Should().BeFalse();

            output = DateTimeExtensionMethods.IsMinValue(new DateTime(2021, 01, 01));
            
            output.Should().BeFalse();
        }

        [Fact]
        public void is_max_value_should_be_true_if_is_max_value()
        {
            var output = DateTimeExtensionMethods.IsMaxValue(DateTime.MaxValue);
            
            output.Should().BeTrue();
        }

        [Fact]
        public void is_max_value_should_be_false_if_is_not_max_value()
        {
            var output = DateTimeExtensionMethods.IsMaxValue(DateTime.MinValue);
            
            output.Should().BeFalse();

            output = DateTimeExtensionMethods.IsMaxValue(new DateTime(2021, 01, 01));
            
            output.Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_false_if_has_value_is_null()
        {
            var output = DateTimeExtensionMethods.HasValidValue(null);
            
            output.Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_false_if_value_is_min_value()
        {
            var output = DateTimeExtensionMethods.HasValidValue(DateTime.MinValue);
            
            output.Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_false_if_value_is_max_value()
        {
            var output = DateTimeExtensionMethods.HasValidValue(DateTime.MaxValue);
                
            output.Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_true_if_value_is_valid_date()
        {
            var output = DateTimeExtensionMethods.HasValidValue(new DateTime(2021, 01, 01));
            
            output.Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_true_if_value_is_min_value()
        {
            DateTime? date = DateTime.MinValue;

            var output = DateTimeExtensionMethods.IsMinValue(date);
            
            output.Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_true_if_value_is_null()
        {

            var output = DateTimeExtensionMethods.IsMinValue(null);

            output.Should().BeFalse();
        }

        [Fact]
        public void is_min_value_should_be_true_if_value_is_date()
        {
            DateTime date = new DateTime(2021, 01, 01);

            var output = DateTimeExtensionMethods.IsMinValue(date);

            output.Should().BeFalse();
        }

        [Fact]
        public void is_max_value_should_be_false_if_value_is_null()
        {
            DateTime? time = new DateTime(2021,01,01);

            var output = DateTimeExtensionMethods.IsMaxValue(null);
            
            output.Should().BeFalse();

            output = DateTimeExtensionMethods.IsMaxValue(time);
            
            output.Should().BeFalse();
        }

        [Fact]
        public void is_max_value_should_be_true_if_value_is_max_value()
        {
            DateTime? time = DateTime.MaxValue;

            var output = DateTimeExtensionMethods.IsMaxValue(time);
            
            output.Should().BeTrue();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_throw_exception_if_any_date_is_not_valid_valie()
        {
            DateTime start = new DateTime(1990, 01, 01);
            DateTime end = new DateTime(2021, 01, 01);

            Action startDateMinValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(DateTime.MinValue, end);
            };

            startDateMinValue.Should().Throw<InvalidDateException>();

            Action startDateMaxValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(DateTime.MaxValue, end);
            };

            startDateMaxValue.Should().Throw<InvalidDateException>();

            Action endDateMinValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(start, DateTime.MinValue);
            };

            endDateMinValue.Should().Throw<InvalidDateException>();

            Action endDateMasValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(start, DateTime.MaxValue);
            };

            endDateMasValue.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_be_false_if_is__not_greather_than_end_date()
        {
            DateTime start = new DateTime(1990, 01, 01);
            DateTime end = new DateTime(2021, 01, 01);

            var output = DateTimeExtensionMethods.CheckIfStartDateIsGte(start, end);
            
            output.Should().BeFalse();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_be_true_if_is_greather_than_end_date()
        {
            DateTime start = new DateTime(2021, 01, 01);
            DateTime end = new DateTime(1990, 01, 01);
           
            var output = DateTimeExtensionMethods.CheckIfStartDateIsGte(start,end);
            
            output.Should().BeTrue();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_be_true_if_is_equal_than_end_date()
        {
            DateTime time = new DateTime(2021, 01, 01);

            var output = DateTimeExtensionMethods.CheckIfStartDateIsGte(time, time);
            
            output.Should().BeTrue();
        }

        [Fact]
        public void not_allow_startDate_greater_than_endDate()
        {
            DateTime startDate = new DateTime(2021, 10, 10);
            DateTime endDate = new DateTime(2020, 01, 01);

            Action newRange = () => DateTimeExtensionMethods.EnsureIsValidRange(startDate, endDate);

            newRange.Should().Throw<DateRangeException>();
        }

        [Fact]
        public void is_valid_range_should_return_false_if_start_is_after_end()
        {
            DateTime startDate = new DateTime(2021, 10, 10);
            DateTime endDate = new DateTime(2020, 01, 01);

            var output = DateTimeExtensionMethods.IsValidRange(startDate, endDate);

            output.Should().Be(false);
        }

        [Fact]
        public void is_valid_range_should_return_false_if_start_is_equals_end()
        {
            DateTime startDate = new DateTime(2020, 01, 01);
            DateTime endDate = new DateTime(2020, 01, 01);

            var output = DateTimeExtensionMethods.IsValidRange(startDate, endDate);

            output.Should().Be(false);
        }

        [Fact]
        public void is_valid_range_should_return_false_if_start_is_before_end()
        {
            DateTime startDate = new DateTime(2019, 01, 01);
            DateTime endDate = new DateTime(2020, 01, 01);

            var output = DateTimeExtensionMethods.IsValidRange(startDate, endDate);

            output.Should().Be(true);
        }

        [Fact]
        public void ensure_date_is_in_range_should_ensure_date_is_gt_than_start_if_end_is_null()
        {
            DateTime startDate = new DateTime(2019, 01, 01);
            DateTime date = new DateTime(2020, 01, 01);


            Action ensureIsInRange = () =>
            {
                DateTimeExtensionMethods.EnsureDateIsInRange(date, startDate, null);
            };

            ensureIsInRange.Should().NotThrow();
        }

        [Fact]
        public void ensure_date_is_in_range_should_not_throw_if_is_in_range()
        {
            DateTime startDate = new DateTime(2019, 01, 01);
            DateTime date = new DateTime(2020, 01, 01);
            DateTime endDate = new DateTime(2021, 01, 01);


            Action ensureIsInRange = () =>
            {
                DateTimeExtensionMethods.EnsureDateIsInRange(date, startDate, endDate);
            };

            ensureIsInRange.Should().NotThrow();
        }

        [Fact]
        public void ensure_date_is_in_range_should_throw_if_is_in_range()
        {
            DateTime startDate = new DateTime(2019, 01, 01);
            DateTime date = new DateTime(2020, 01, 01);
            DateTime endDate = new DateTime(2018, 01, 01);


            Action ensureIsInRange = () =>
            {
                DateTimeExtensionMethods.EnsureDateIsInRange(date, startDate, endDate);
            };

            ensureIsInRange.Should().Throw<DateRangeException>();
        }
    }
}
