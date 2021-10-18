using System;
using Xunit;
using FluentAssertions;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Exceptions;

namespace Semicrol.Scheduler.Domain.Tests.Common
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
            DateTimeExtensionMethods.IsValidDate(DateTime.MinValue).Should().BeFalse();
        }

        [Fact]
        public void is_valid_date_false_if_is_max_value()
        {
            DateTimeExtensionMethods.IsValidDate(DateTime.MaxValue).Should().BeFalse();
        }

        [Fact]
        public void is_valid_date_true_if_is_not_min_value_or_max_value()
        {
            DateTimeExtensionMethods.IsValidDate(new DateTime(2021,01,01)).Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_true_if_is_min_value()
        {
            DateTimeExtensionMethods.IsMinValue(DateTime.MinValue).Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_false_if_is_not_min_value()
        {
            DateTimeExtensionMethods.IsMinValue(DateTime.MaxValue).Should().BeFalse();
            DateTimeExtensionMethods.IsMinValue(new DateTime(2021,01,01)).Should().BeFalse();
        }

        [Fact]
        public void is_max_value_should_be_true_if_is_max_value()
        {
            DateTimeExtensionMethods.IsMaxValue(DateTime.MaxValue).Should().BeTrue();
        }

        [Fact]
        public void is_max_value_should_be_false_if_is_not_max_value()
        {
            DateTimeExtensionMethods.IsMaxValue(DateTime.MinValue).Should().BeFalse();
            DateTimeExtensionMethods.IsMaxValue(new DateTime(2021, 01, 01)).Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_false_if_has_value_is_null()
        {
            DateTimeExtensionMethods.HasValidValue(null).Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_false_if_value_is_min_value()
        {
            DateTimeExtensionMethods.HasValidValue(DateTime.MinValue).Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_false_if_value_is_max_value()
        {
            DateTimeExtensionMethods.HasValidValue(DateTime.MaxValue).Should().BeFalse();
        }

        [Fact]
        public void has_valid_value_should_be_true_if_value_is_valid_date()
        {
            DateTimeExtensionMethods.HasValidValue(new DateTime(2021,01,01)).Should().BeTrue();
        }

        [Fact]
        public void is_min_value_should_be_true_if_value_is_min_value()
        {
            DateTime? date = DateTime.MinValue;
            DateTimeExtensionMethods.IsMinValue(date).Should().BeTrue();
        }

        [Fact]
        public void is_max_value_should_be_false_if_value_is_null()
        {
            DateTime? time = new DateTime(2021,01,01);
            DateTimeExtensionMethods.IsMaxValue(null).Should().BeFalse();
            DateTimeExtensionMethods.IsMaxValue(time).Should().BeFalse();
        }

        [Fact]
        public void is_max_value_should_be_true_if_value_is_max_value()
        {
            DateTime? time = DateTime.MaxValue;
            DateTimeExtensionMethods.IsMaxValue(time).Should().BeTrue();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_throw_exception_if_any_date_is_not_valid_valie()
        {
            Action startDateMinValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(DateTime.MinValue, new DateTime(2021, 01, 01));
            };
            startDateMinValue.Should().Throw<InvalidDateException>();

            Action startDateMaxValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(DateTime.MaxValue, new DateTime(2021, 01, 01));
            };
            startDateMaxValue.Should().Throw<InvalidDateException>();

            Action endDateMinValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(new DateTime(1990, 01, 01), DateTime.MinValue);
            };
            endDateMinValue.Should().Throw<InvalidDateException>();

            Action endDateMasValue = () =>
            {
                DateTimeExtensionMethods.CheckIfStartDateIsGte(new DateTime(1990, 01, 01), DateTime.MaxValue);
            };
            endDateMasValue.Should().Throw<InvalidDateException>();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_be_false_if_is__not_greather_than_end_date()
        {
            DateTimeExtensionMethods.CheckIfStartDateIsGte(new DateTime(1990,01,01), new DateTime(2021,01,01)).Should().BeFalse();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_be_true_if_is_greather_than_end_date()
        {
            DateTimeExtensionMethods.CheckIfStartDateIsGte(new DateTime(2021, 01, 01), new DateTime(1990, 01, 01)).Should().BeTrue();
        }

        [Fact]
        public void check_if_start_date_is_gte_should_be_true_if_is_equal_than_end_date()
        {
            DateTime time = new DateTime(2021, 01, 01);
            DateTimeExtensionMethods.CheckIfStartDateIsGte(time, time).Should().BeTrue();
        }

        [Fact]
        public void not_allow_startDate_greater_than_endDate()
        {
            DateTime startDate = new DateTime(2021, 10, 10);
            DateTime endDate = new DateTime(2020, 01, 01);
            Action newRange = () => DateTimeExtensionMethods.EnsureIsValidRange(startDate, endDate);
            newRange.Should().Throw<Exceptions.DateRangeException>();
        }
    }
}
