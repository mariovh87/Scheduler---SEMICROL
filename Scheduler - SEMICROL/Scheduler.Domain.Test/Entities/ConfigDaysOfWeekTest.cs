using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class ConfigDaysOfWeekTest
    {
        [Fact]
        public void get_monday_should_return_config_day_monday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetMonday().Day.Should().Be(DayOfWeek.Monday);
        }

        [Fact]
        public void get_tuesday_should_return_config_day_tuesday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetTuesday().Day.Should().Be(DayOfWeek.Tuesday);
        }

        [Fact]
        public void get_wednesday_should_return_config_day_wednesday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetWednesday().Day.Should().Be(DayOfWeek.Wednesday);
        }

        [Fact]
        public void get_thursday_should_return_config_day_thursday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetThursday().Day.Should().Be(DayOfWeek.Thursday);
        }

        [Fact]
        public void get_friday_should_return_config_day_friday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetFriday().Day.Should().Be(DayOfWeek.Friday);
        }


        [Fact]
        public void get_saturday_should_return_config_day_saturday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetSaturday().Day.Should().Be(DayOfWeek.Saturday);
        }

        [Fact]
        public void get_sunday_should_return_config_day_sunday()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetSunday().Day.Should().Be(DayOfWeek.Sunday);
        }

        [Fact]
        public void is_monday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.IsMondayChecked().Should().Be(false);
        }

        [Fact]
        public void is_tuesday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.GetTuesday().Should().Be(false);
        }

        [Fact]
        public void is_wednesday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.IsWednesdayChecked().Should().Be(false);
        }

        [Fact]
        public void is_thursday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.IsThursdayChecked().Should().Be(false);
        }

        [Fact]
        public void is_friday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.IsFridayChecked().Should().Be(false);
        }


        [Fact]
        public void is_saturday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.IsSaturdayChecked().Should().Be(false);
        }

        [Fact]
        public void is_sunday_checked_should_return_false_after_initialize()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.IsSundayChecked().Should().Be(false);
        }

        [Fact]
        public void monday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);
            days.IsMondayChecked().Should().Be(true);
        }

        [Fact]
        public void tuesday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckTuesday(true);
            days.IsTuesdayChecked().Should().Be(true);
        }

        [Fact]
        public void wednesday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckWednesday(true);
            days.IsWednesdayChecked().Should().Be(true);
        }

        [Fact]
        public void thursday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckThursday(true);
            days.IsThursdayChecked().Should().Be(true);
        }

        [Fact]
        public void friday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckFriday(true);
            days.IsFridayChecked().Should().Be(true);
        }

        [Fact]
        public void saturday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckSaturday(true);
            days.IsSaturdayChecked().Should().Be(true);
        }

        [Fact]
        public void sunday_checked_should_be_true_after_check()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckSunday(true);
            days.IsSundayChecked().Should().Be(true);
        }
    }
}
