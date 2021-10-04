using EnsureThat;
using System;

namespace Scheduler.Domain.Entities
{
    public class Output
    {
        private ExecutionTimes executionTimes;
        private string description;
        public Output(ExecutionTimes executionTimes, string description)
        {
            Ensure.That(executionTimes).IsNotNull();
            Ensure.That(description).IsNotEmptyOrWhiteSpace();
            this.executionTimes = executionTimes;
            this.description = description;
        }

        public DateTime NextExecutionTime()
        {
            return executionTimes.GetNextExecutionTime();
        }

        public DateTime DequeueNextExecutionTime()
        {
            return executionTimes.DequeueNextExecutionTime();
        }

        public string Description()
        {
            return this.description;
        }
    }
}
