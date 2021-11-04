using Semicrol.Scheduler.Domain.Common;
using System;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class Input
    {
        public DateTime CurrentDate { get; private set; }

        public Input(DateTime currentDate)
        {
            currentDate.EnsureIsValidDate();
            this.CurrentDate = currentDate;
        }

    }
}
