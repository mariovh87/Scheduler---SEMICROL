using FluentAssertions;
using Scheduler.Domain.Entities;
using System;
using Xunit;

namespace Scheduler.Domain.Tests.Entities
{
    public class OutputTest
    {
        [Fact]
        public void output_date_should_be_a_valid_date()
        {
            Action dateNotValid = () =>
            {
                new Output(DateTime.MinValue, "Description");
            };
            dateNotValid.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void description_should_not_be_empty_or_whitespace()
        {
            Action descriptionNotValid = () =>
            {
                new Output(new DateTime(01, 01, 2021), string.Empty);
            };
            descriptionNotValid.Should().Throw<ArgumentException>();
        }
    }
}
