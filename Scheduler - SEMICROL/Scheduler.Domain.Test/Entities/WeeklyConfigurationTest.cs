using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class WeeklyConfigurationTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void every_should_throw_exception_if_lesser_than_zero(int every)
        {
            Action act = () => new WeeklyConfiguration(every);
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void every_should_throw_exception_if_greather_than_zero(int every)
        {
            Action act = () => new WeeklyConfiguration(every);
            act.Should().NotThrow<ArgumentException>();
        }

    }
}
