using EnsureThat;
using System;

namespace Scheduler.Domain.Entities
{
    public class Output
    {
        private ExecutionTimes executionTimes;
        public Output(ExecutionTimes executionTimes)
        {
            Ensure.That(executionTimes).IsNotNull();
            this.executionTimes = executionTimes;
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
            return OutputDescription.Description(this);
        }
    }
}
