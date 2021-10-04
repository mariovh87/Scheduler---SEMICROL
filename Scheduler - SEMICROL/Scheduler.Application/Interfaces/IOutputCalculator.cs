using Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Application.Interfaces
{
    internal interface IOutputCalculator
    {
        Output CalculateOutput(Input input, Configuration config, Limits limits);
    }
}
