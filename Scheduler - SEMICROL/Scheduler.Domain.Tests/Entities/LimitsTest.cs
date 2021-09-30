using System;
using FluentAssertions;
using Scheduler.Domain.Entities;
using Xunit;

namespace Scheduler.Domain.Tests.Entities
{   
    public class LimitsTest
    {
        [Fact]
        public void not_allow_startDate_greater_than_endDate()
        {
            DateTime startDate = new DateTime(10,10,2021);
            DateTime endDate = new DateTime(01, 01, 2020);
            Action newLimits = () => new Limits(startDate, endDate);
            newLimits.Should().Throw<ArgumentException>();
        }
    }
}
