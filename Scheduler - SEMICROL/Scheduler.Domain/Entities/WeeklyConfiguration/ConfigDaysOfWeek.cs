using System;
using System.Collections.Generic;
using System.Linq;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class ConfigDaysOfWeek
    {
        private readonly IList<ConfigDay> days;

        public ConfigDaysOfWeek()
        {
            days = new List<ConfigDay>();
            PopulateDaysOfWeekCollection();
        }

        private void PopulateDaysOfWeekCollection()
        {
            days.Add(new ConfigDay(DayOfWeek.Monday, false));
            days.Add(new ConfigDay(DayOfWeek.Tuesday, false));
            days.Add(new ConfigDay(DayOfWeek.Wednesday, false));
            days.Add(new ConfigDay(DayOfWeek.Thursday, false));
            days.Add(new ConfigDay(DayOfWeek.Friday, false));
            days.Add(new ConfigDay(DayOfWeek.Saturday, false));
            days.Add(new ConfigDay(DayOfWeek.Sunday, false));
        }

        public ConfigDay GetMonday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Monday));
        }

        public bool IsMondayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Monday)).IsChecked;
        }

        public ConfigDay GetTuesday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Tuesday));
        }

        public bool IsTuesdayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Tuesday)).IsChecked;
        }
        public ConfigDay GetWednesday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Wednesday));
        }

        public bool IsWednesdayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Wednesday)).IsChecked;
        }

        public ConfigDay GetThursday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Thursday));
        }

        public bool IsThursdayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Thursday)).IsChecked;
        }

        public ConfigDay GetFriday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Friday));
        }

        public bool IsFridayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Friday)).IsChecked;
        }

        public ConfigDay GetSaturday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Saturday));
        }

        public bool IsSaturdayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Saturday)).IsChecked;
        }

        public ConfigDay GetSunday()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Sunday));
        }

        public bool IsSundayChecked()
        {
            return days.First(day => day.Day.Equals(DayOfWeek.Sunday)).IsChecked;
        }

        public void CheckMonday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Monday)).IsChecked = check;
        }

        public void CheckTuesday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Tuesday)).IsChecked = check;
        }

        public void CheckWednesday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Wednesday)).IsChecked = check;
        }

        public void CheckThursday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Thursday)).IsChecked = check;
        }

        public void CheckFriday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Friday)).IsChecked = check;
        }

        public void CheckSaturday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Saturday)).IsChecked = check;
        }

        public void CheckSunday(bool check)
        {
            this.days.First(day => day.Day.Equals(DayOfWeek.Sunday)).IsChecked = check;
        }
    }    
}
