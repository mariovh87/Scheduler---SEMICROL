using System;
using System.Collections.Generic;
using System.Linq;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class ConfigDaysOfWeek
    {
        public IList<ConfigDay> Days { get; private set; }

        public ConfigDaysOfWeek()
        {
            Days = new List<ConfigDay>();
            PopulateDaysOfWeekCollection();
        }

        private void PopulateDaysOfWeekCollection()
        {
            Days.Add(new ConfigDay(DayOfWeek.Monday, false));
            Days.Add(new ConfigDay(DayOfWeek.Tuesday, false));
            Days.Add(new ConfigDay(DayOfWeek.Wednesday, false));
            Days.Add(new ConfigDay(DayOfWeek.Thursday, false));
            Days.Add(new ConfigDay(DayOfWeek.Friday, false));
            Days.Add(new ConfigDay(DayOfWeek.Saturday, false));
            Days.Add(new ConfigDay(DayOfWeek.Sunday, false));
        }

        public ConfigDay GetMonday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Monday));
        }

        public bool IsMondayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Monday)).IsChecked;
        }

        public ConfigDay GetTuesday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Tuesday));
        }

        public bool IsTuesdayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Tuesday)).IsChecked;
        }
        public ConfigDay GetWednesday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Wednesday));
        }

        public bool IsWednesdayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Wednesday)).IsChecked;
        }

        public ConfigDay GetThursday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Thursday));
        }

        public bool IsThursdayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Thursday)).IsChecked;
        }

        public ConfigDay GetFriday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Friday));
        }

        public bool IsFridayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Friday)).IsChecked;
        }

        public ConfigDay GetSaturday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Saturday));
        }

        public bool IsSaturdayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Saturday)).IsChecked;
        }

        public ConfigDay GetSunday()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Sunday));
        }

        public bool IsSundayChecked()
        {
            return Days.First(day => day.Day.Equals(DayOfWeek.Sunday)).IsChecked;
        }

        public void CheckMonday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Monday)).IsChecked = check;
        }

        public void CheckTuesday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Tuesday)).IsChecked = check;
        }

        public void CheckWednesday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Wednesday)).IsChecked = check;
        }

        public void CheckThursday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Thursday)).IsChecked = check;
        }

        public void CheckFriday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Friday)).IsChecked = check;
        }

        public void CheckSaturday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Saturday)).IsChecked = check;
        }

        public void CheckSunday(bool check)
        {
            this.Days.First(day => day.Day.Equals(DayOfWeek.Sunday)).IsChecked = check;
        }

        public bool AreDaysChecked()
        {
            return !this.Days.Any(d => d.IsChecked);
        }
    }    
}
