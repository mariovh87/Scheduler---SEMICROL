using FluentAssertions;
using Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Tests.Entities
{
    internal class ExecutionTimesQueueTest
    {
        [Fact]
        public void get_next_execution_after_queue_on_empty_queue_should_return_same_date_and_not_remove()
        {
            ExecutionTimesQueue queue = new ExecutionTimesQueue();
            DateTime execution = new DateTime(01, 01, 2021);
            queue.QueueExecution(execution);
            queue.GetNextExecution().Should().Be(execution);
            queue.GetNextExecution().Should().Be(execution);
            queue.DequeueNextExecution().Should().Be(execution);
        }
        [Fact]
        public void dequeue_after_queue_on_empty_queue_should_return_same_date()
        {
            ExecutionTimesQueue queue = new ExecutionTimesQueue();
            DateTime execution = new DateTime(01, 01, 2021);
            queue.QueueExecution(execution);
            queue.DequeueNextExecution().Should().Be(execution);
        }

        [Fact]
        public void queue_should_be_first_in_first_out()
        {
            ExecutionTimesQueue queue = new ExecutionTimesQueue();
            DateTime execution = new DateTime(01, 01, 2021);
            DateTime execution2 = new DateTime(01, 01, 2022);
            DateTime execution3 = new DateTime(01, 01, 2023);
            queue.QueueExecution(execution);
            queue.QueueExecution(execution2);
            queue.QueueExecution(execution3);
            queue.DequeueNextExecution().Should().Be(execution);

        }
    }
}
