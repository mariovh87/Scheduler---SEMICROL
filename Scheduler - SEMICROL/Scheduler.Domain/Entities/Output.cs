using EnsureThat;
using System;

namespace Scheduler.Domain.Entities
{
    internal class Output
    {
        private ExecutionTimes executionTimes;
        public Output(ExecutionTimes executionTimes)
        {
            Ensure.That(executionTimes).IsNotNull();
            this.executionTimes = executionTimes;
        }

        public ExecutionTimes ExecutionTimes {  get; private set; }

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
            return OutputDescription.Description(this);
        }
    }
}
