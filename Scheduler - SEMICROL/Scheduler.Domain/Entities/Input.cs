using Scheduler.Domain.Common;
using System;

namespace Scheduler.Domain.Entities
{
    public class Input
    {
        private readonly DateTime currentDate;
        public Input(DateTime currentDate)
        {
            currentDate.EnsureIsValidDate();
            this.currentDate = currentDate;
        }

        public DateTime CurrentDate()
        {
            return this.currentDate;
        }

    }
}
