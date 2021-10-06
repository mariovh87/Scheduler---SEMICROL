using Scheduler.Domain.Entities;
using System;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Application.Interfaces
{
    internal interface IOutputCalculator
    {
        Output CalculateOutput(Domain.Entities.Scheduler scheduler);
    }
}
