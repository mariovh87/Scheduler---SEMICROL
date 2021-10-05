using FluentAssertions;
using Scheduler.Domain.Entities;
using System;
using Xunit;

namespace Scheduler.Domain.Tests.Entities
{
    internal class LimitsTest
    {

        [Fact]
        public void end_date_should_be_greather_than_start_date()
        {
            Action newLimits = () =>
            {
                new Limits(new DateTime(01, 01, 20221), new DateTime(01, 01, 2020));
            };
            newLimits.Should().Throw<ArgumentException>();
        }
    }
}
