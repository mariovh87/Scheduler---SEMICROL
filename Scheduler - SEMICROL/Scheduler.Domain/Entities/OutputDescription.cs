using EnsureThat;
using System;

namespace Scheduler.Domain.Entities
{
    internal static class OutputDescription
    {
        private const string format = "Occurs {0}. Schedule will be used on {1} {2} starting on {3}";

        public static string Description(Output output)
        {
            return String.Format(format, "", "", "", "");
        }
    }
}
