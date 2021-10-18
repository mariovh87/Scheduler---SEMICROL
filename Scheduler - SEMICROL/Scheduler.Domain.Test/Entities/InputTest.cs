using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using Xunit;

namespace Semicrol.Scheduler.Domain.Tests.Entities
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
            minValueParameterAction.Should().Throw<InvalidDateException>();
            maxValueParameterAction.Should().Throw<InvalidDateException>();
        }
    }
}
