using EnsureThat;
using Semicrol.Scheduler.Domain.Exceptions;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration
{
    public class MonthlyConfiguration
    {
        public MonthlyConfiguration(bool day, int everyDay, int dayMonths)
        {
            Ensure.That(day).IsTrue();
            EnsureValidDayMonthsValues(everyDay, dayMonths);
            Day = day;
            EveryDay = everyDay;   
            DayMonths = dayMonths;
        }

        public MonthlyConfiguration(bool the, MonthlyFrecuency frecuency, MonthlyDayType dayType, int everyMonths)
        {
            Ensure.That(the).IsTrue();
            Ensure.That(everyMonths).IsGt(0);
            The = the;
            Frecuency = frecuency;
            DayType = dayType;
            EveryMonths = everyMonths;
        }

        public MonthlyConfiguration(bool day, int everyDay, int dayMonths, bool the, MonthlyFrecuency frecuency, MonthlyDayType dayType, int everyMonths)
        {
            EnsureOnlyOneOptionIsChecked(day, the);
            if (day) {
                EnsureValidDayMonthsValues(everyDay, dayMonths);
            }
            else
            {
                Ensure.That(everyMonths).IsGt(0);
            }
            Day = day;
            EveryDay = everyDay;
            DayMonths = dayMonths;      
            The = the;
            Frecuency = frecuency;
            DayType = dayType;
            EveryMonths = everyMonths;
        }

        public bool Day{ get; private set; }
        public int? EveryDay { get; private set; }
        public int? DayMonths { get; private set; }
        public bool The{ get; private set; }
        public MonthlyFrecuency? Frecuency { get; private set; } 
        public MonthlyDayType? DayType{ get; private set; }
        public int? EveryMonths { get; private set; }

        private static void EnsureOnlyOneOptionIsChecked(bool day, bool the)
        {
            if (day && the
                || !day && !the)
            {
                throw new DomainException($"Only one of theese options:{nameof(day)},{nameof(the)}  must be true");
            }
        }

        private static void EnsureValidDayMonthsValues(int days, int months)
        {
            Ensure.That(days).IsGt(0);
            Ensure.That(months).IsGt(0);
        }

    }
}
