using System;
using EnsureThat;
using Semicrol.Scheduler.Domain.Common;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class Limits
    {
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public Limits(DateTime startDate, DateTime endDate)
        {
            startDate.EnsureIsValidDate();
            DateTimeExtensionMethods.EnsureIsValidRange(startDate, endDate); 

            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
