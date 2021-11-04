using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using Xunit;

namespace Semicrol.Scheduler.Domain.Test.Entities
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
