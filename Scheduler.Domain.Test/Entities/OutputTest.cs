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
        public void output_date_list_should_be_a_not_null_list()
        {
            Output output = new Output();
            output.ExecutionTime.Should().NotBeNull();
        }
    }
}
