using FluentAssertions;
using Scheduler.Domain.Entities;
using Scheduler.Domain.Exceptions;
using System;
using Xunit;

namespace Scheduler.Domain.Tests.Entities
{
    public class LimitsTest
    {

        [Fact]
        public void end_date_should_be_greather_than_start_date()
        {
            Action newLimits = () =>
            {
                new Limits(new DateTime(2021, 01, 01), new DateTime(2020, 01, 01));
            };
            newLimits.Should().Throw<DateRangeException>();
        }
    }
}
