using System;
using Xunit;
using FluentAssertions;
using Semicrol.Scheduler.Domain.Common;

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
            isValidFunc.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void max_value_is_not_valid_date()
        {
            Action isValidFunc = () =>
            {
                DateTime.MaxValue.EnsureIsValidDate();
            };
            isValidFunc.Should().Throw<ArgumentException>();
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
        public void not_allow_startDate_greater_than_endDate()
        {
            DateTime startDate = new DateTime(2021, 10, 10);
            DateTime endDate = new DateTime(2020, 01, 01);
            Action newRange = () => DateTimeExtensionMethods.EnsureIsValidRange(startDate, endDate);
            newRange.Should().Throw<Exceptions.DateRangeException>();
        }
    }
}
