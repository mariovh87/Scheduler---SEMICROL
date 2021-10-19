using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using Xunit;

namespace Semicrol.Scheduler.Domain.Test.Entities
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
            dateNotValid.Should().Throw<InvalidDateException>();
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
