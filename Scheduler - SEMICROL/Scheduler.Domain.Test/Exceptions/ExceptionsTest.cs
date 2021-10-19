using FluentAssertions;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Semicrol.Scheduler.Domain.Test.Exceptions
{
    public class ExceptionsTest
    {
        private readonly string message = "Testing Exception";
        private readonly string InvalidDateExceptionDefaultMessage = "Invalid date";

        [Fact]
        public void date_range_exception_message_should_be_parameter_string()
        {
            Action invocation = () => throw new DateRangeException(message);
            invocation.Should().Throw<DateRangeException>().WithMessage(message);
        }

        [Fact]
        public void invalid_date_exception_default_message_should_be_invalid_date()
        {
            Action invocation = () => throw new InvalidDateException();
            invocation.Should().Throw<InvalidDateException>().WithMessage(InvalidDateExceptionDefaultMessage);
        }

        [Fact]
        public void invalid_date_exception_message_should_be_parameter_string()
        {
            Action invocation = () => throw new InvalidDateException(message);
            invocation.Should().Throw<InvalidDateException>().WithMessage(message);
        }

        [Fact]
        public void domain_exception_message_should_be_parameter_string()
        {
            Action invocation = () => throw new DomainException(message);
            invocation.Should().Throw<DomainException>().WithMessage(message);
        }
    }
}
