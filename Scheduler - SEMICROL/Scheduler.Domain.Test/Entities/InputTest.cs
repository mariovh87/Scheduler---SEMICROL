using FluentAssertions;
using Scheduler.Domain.Entities;
using System;
using Xunit;

namespace Scheduler.Domain.Tests.Entities
{
    public class InputTest
    {
        [Fact]
        public void input_date_should_be_a_valid_date()
        {
            Action minValueParameterAction = () =>
            {
                new Input(DateTime.MinValue);
            };
            Action maxValueParameterAction = () =>
            {
                new Input(DateTime.MaxValue);
            };
            minValueParameterAction.Should().Throw<ArgumentException>();
            maxValueParameterAction.Should().Throw<ArgumentException>();
        }
    }
}
