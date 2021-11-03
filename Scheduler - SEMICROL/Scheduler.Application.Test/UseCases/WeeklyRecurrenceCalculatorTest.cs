using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class WeeklyRecurrenceCalculatorTest
    {
        [Fact]
        public void get_next_week_day_should_return_next_monday_when_day_of_week_monday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Monday);
                
            result.Should().Be(new DateTime(2021, 01, 04));
        }

        [Fact]
        public void get_next_week_day_should_return_next_tuesday_when_day_of_week_tuesday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Tuesday);
                
            result.Should().Be(new DateTime(2021, 01, 05));
        }

        [Fact]
        public void get_next_week_day_should_return_next_wednesday_when_day_of_week_wednesday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Wednesday);
                
            result.Should().Be(new DateTime(2021, 01, 06));
        }
        [Fact]
        public void get_next_week_day_should_return_next_thursday_when_day_of_week_thursday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Thursday);
                
            result.Should().Be(new DateTime(2021, 01, 07));
        }
        [Fact]
        public void get_next_week_day_should_return_next_friday_when_day_of_week_friday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Friday);
                
            result.Should().Be(new DateTime(2021, 01, 01));
        }
        [Fact]
        public void get_next_week_day_should_return_next_saturday_when_day_of_week_saturday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Saturday);
            
            result.Should().Be(new DateTime(2021, 01, 02));
        }
        [Fact]
        public void get_next_week_day_should_return_next_sunday_when_day_of_week_sunday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Sunday);
            
            result.Should().Be(new DateTime(2021, 01, 03));
        }     

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        public void get_next_recurrence_start_date_should_add_every_multiplicated_by_7_days(int every)
        {
            DateTime startDate = new DateTime(2021, 01, 01);

            var result = WeeklyRecurrenceCalculator.AddEveryBy7DaysToCurrentDate(startDate, every);
            
            result.Should().Be(startDate.AddDays(every * 7));
        }


        [Fact]
        public void add_days_to_recurrence_list_should_return_empty_list_if_no_days_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);

            var result = WeeklyRecurrenceCalculator.AddDaysToRecurrenceList(currentdate, config.DaysOfWeek.Days);
            
            result.Should().BeEmpty();
        }

        [Fact]
        public void get_next_recurrence_should_return_empty_list_if_no_days_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);

            var result = WeeklyRecurrenceCalculator.GetNextRecurrence(currentdate, config);
            
            result.Should().BeEmpty();
        }

        [Fact]
        public void get_next_recurrence_should_return_next_recurrence_of_days_checked_from_current_date_monday()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            var result = WeeklyRecurrenceCalculator.GetNextRecurrence(currentdate, config);
            
            result[0].Should().Be(new DateTime(2021, 01, 01));
            result[1].Should().Be(new DateTime(2021, 01, 02));
            result[2].Should().Be(new DateTime(2021, 01, 03));
        }

        [Fact]
        public void get_next_recurrence_should_return_next_recurrence_of_days_checked_from_current_date_friday()
        {
            DateTime currentdate = new DateTime(2020, 12, 28);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            var result = WeeklyRecurrenceCalculator.GetNextRecurrence(currentdate, config);
            
            result[0].Should().Be(new DateTime(2020, 12, 28));
            result[1].Should().Be(new DateTime(2020, 12, 29));
            result[2].Should().Be(new DateTime(2020, 12, 30));
            result[3].Should().Be(new DateTime(2020, 12, 31));
            result[4].Should().Be(new DateTime(2021, 01, 01));
            result[5].Should().Be(new DateTime(2021, 01, 02));
            result[6].Should().Be(new DateTime(2021, 01, 03));
        }

        [Fact]
        public void all_recurrences_should_return_empty_list_check_adding_weeks_to_last_date_is_later_than_limit_date()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 10);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);
            
            result.Should().BeEmpty();
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 15);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 02, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 02, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 02, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 02, 07, 30, 00));
            result[8].Should().Be(new DateTime(2021, 01, 03, 01, 30, 00));
            result[9].Should().Be(new DateTime(2021, 01, 03, 03, 30, 00));
            result[10].Should().Be(new DateTime(2021, 01, 03, 05, 30, 00));
            result[11].Should().Be(new DateTime(2021, 01, 03, 07, 30, 00));
            result[12].Should().Be(new DateTime(2021, 01, 11, 01, 30, 00));
            result[13].Should().Be(new DateTime(2021, 01, 11, 03, 30, 00));
            result[14].Should().Be(new DateTime(2021, 01, 11, 05, 30, 00));
            result[15].Should().Be(new DateTime(2021, 01, 11, 07, 30, 00));
            result[16].Should().Be(new DateTime(2021, 01, 12, 01, 30, 00));
            result[17].Should().Be(new DateTime(2021, 01, 12, 03, 30, 00));
            result[18].Should().Be(new DateTime(2021, 01, 12, 05, 30, 00));
            result[19].Should().Be(new DateTime(2021, 01, 12, 07, 30, 00));
            result[20].Should().Be(new DateTime(2021, 01, 13, 01, 30, 00));
            result[21].Should().Be(new DateTime(2021, 01, 13, 03, 30, 00));
            result[22].Should().Be(new DateTime(2021, 01, 13, 05, 30, 00));
            result[23].Should().Be(new DateTime(2021, 01, 13, 07, 30, 00));
            result[24].Should().Be(new DateTime(2021, 01, 14, 01, 30, 00));
            result[25].Should().Be(new DateTime(2021, 01, 14, 03, 30, 00));
            result[26].Should().Be(new DateTime(2021, 01, 14, 05, 30, 00));
            result[27].Should().Be(new DateTime(2021, 01, 14, 07, 30, 00));
            result[28].Should().Be(new DateTime(2021, 01, 15, 01, 30, 00));
            result[29].Should().Be(new DateTime(2021, 01, 15, 03, 30, 00));
            result[30].Should().Be(new DateTime(2021, 01, 15, 05, 30, 00));
            result[31].Should().Be(new DateTime(2021, 01, 15, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_friday()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 15);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 02, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 02, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 02, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 02, 07, 30, 00));
            result[8].Should().Be(new DateTime(2021, 01, 03, 01, 30, 00));
            result[9].Should().Be(new DateTime(2021, 01, 03, 03, 30, 00));
            result[10].Should().Be(new DateTime(2021, 01, 03, 05, 30, 00));
            result[11].Should().Be(new DateTime(2021, 01, 03, 07, 30, 00));
            result[12].Should().Be(new DateTime(2021, 01, 15, 01, 30, 00));
            result[13].Should().Be(new DateTime(2021, 01, 15, 03, 30, 00));
            result[14].Should().Be(new DateTime(2021, 01, 15, 05, 30, 00));
            result[15].Should().Be(new DateTime(2021, 01, 15, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_friday_with_limit_between_every()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 22);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 02, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 02, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 02, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 02, 07, 30, 00));
            result[8].Should().Be(new DateTime(2021, 01, 03, 01, 30, 00));
            result[9].Should().Be(new DateTime(2021, 01, 03, 03, 30, 00));
            result[10].Should().Be(new DateTime(2021, 01, 03, 05, 30, 00));
            result[11].Should().Be(new DateTime(2021, 01, 03, 07, 30, 00));
            result[12].Should().Be(new DateTime(2021, 01, 15, 01, 30, 00));
            result[13].Should().Be(new DateTime(2021, 01, 15, 03, 30, 00));
            result[14].Should().Be(new DateTime(2021, 01, 15, 05, 30, 00));
            result[15].Should().Be(new DateTime(2021, 01, 15, 07, 30, 00));
            result[16].Should().Be(new DateTime(2021, 01, 16, 01, 30, 00));
            result[17].Should().Be(new DateTime(2021, 01, 16, 03, 30, 00));
            result[18].Should().Be(new DateTime(2021, 01, 16, 05, 30, 00));
            result[19].Should().Be(new DateTime(2021, 01, 16, 07, 30, 00));
            result[20].Should().Be(new DateTime(2021, 01, 17, 01, 30, 00));
            result[21].Should().Be(new DateTime(2021, 01, 17, 03, 30, 00));
            result[22].Should().Be(new DateTime(2021, 01, 17, 05, 30, 00));
            result[23].Should().Be(new DateTime(2021, 01, 17, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_friday_with_limit_in_same_week()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01,02);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);      
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 02, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 02, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 02, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 02, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_monday_with_limit_in_same_week()
        {
            DateTime currentdate = new DateTime(2020, 12, 28);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 02);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2020, 12, 28, 01, 30, 00));
            result[1].Should().Be(new DateTime(2020, 12, 28, 03, 30, 00));
            result[2].Should().Be(new DateTime(2020, 12, 28, 05, 30, 00));
            result[3].Should().Be(new DateTime(2020, 12, 28, 07, 30, 00));
            result[4].Should().Be(new DateTime(2020, 12, 29, 01, 30, 00));
            result[5].Should().Be(new DateTime(2020, 12, 29, 03, 30, 00));
            result[6].Should().Be(new DateTime(2020, 12, 29, 05, 30, 00));
            result[7].Should().Be(new DateTime(2020, 12, 29, 07, 30, 00));
            result[8].Should().Be(new DateTime(2020, 12, 30, 01, 30, 00));
            result[9].Should().Be(new DateTime(2020, 12, 30, 03, 30, 00));
            result[10].Should().Be(new DateTime(2020, 12, 30, 05, 30, 00));
            result[11].Should().Be(new DateTime(2020, 12, 30, 07, 30, 00));
            result[12].Should().Be(new DateTime(2020, 12, 31, 01, 30, 00));
            result[13].Should().Be(new DateTime(2020, 12, 31, 03, 30, 00));
            result[14].Should().Be(new DateTime(2020, 12, 31, 05, 30, 00));
            result[15].Should().Be(new DateTime(2020, 12, 31, 07, 30, 00));
            result[16].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[17].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[18].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[19].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result[20].Should().Be(new DateTime(2021, 01, 02, 01, 30, 00));
            result[21].Should().Be(new DateTime(2021, 01, 02, 03, 30, 00));
            result[22].Should().Be(new DateTime(2021, 01, 02, 05, 30, 00));
            result[23].Should().Be(new DateTime(2021, 01, 02, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_every_week_recurrence()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(1);
            DateTime limit = new DateTime(2021, 01, 15);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 01, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 01, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 01, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 01, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 02, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 02, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 02, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 02, 07, 30, 00));
            result[8].Should().Be(new DateTime(2021, 01, 03, 01, 30, 00));
            result[9].Should().Be(new DateTime(2021, 01, 03, 03, 30, 00));
            result[10].Should().Be(new DateTime(2021, 01, 03, 05, 30, 00));
            result[11].Should().Be(new DateTime(2021, 01, 03, 07, 30, 00));
            result[12].Should().Be(new DateTime(2021, 01, 04, 01, 30, 00));
            result[13].Should().Be(new DateTime(2021, 01, 04, 03, 30, 00));
            result[14].Should().Be(new DateTime(2021, 01, 04, 05, 30, 00));
            result[15].Should().Be(new DateTime(2021, 01, 04, 07, 30, 00));
            result[16].Should().Be(new DateTime(2021, 01, 05, 01, 30, 00));
            result[17].Should().Be(new DateTime(2021, 01, 05, 03, 30, 00));
            result[18].Should().Be(new DateTime(2021, 01, 05, 05, 30, 00));
            result[19].Should().Be(new DateTime(2021, 01, 05, 07, 30, 00));
            result[20].Should().Be(new DateTime(2021, 01, 06, 01, 30, 00));
            result[21].Should().Be(new DateTime(2021, 01, 06, 03, 30, 00));
            result[22].Should().Be(new DateTime(2021, 01, 06, 05, 30, 00));
            result[23].Should().Be(new DateTime(2021, 01, 06, 07, 30, 00));
            result[24].Should().Be(new DateTime(2021, 01, 07, 01, 30, 00));
            result[25].Should().Be(new DateTime(2021, 01, 07, 03, 30, 00));
            result[26].Should().Be(new DateTime(2021, 01, 07, 05, 30, 00));
            result[27].Should().Be(new DateTime(2021, 01, 07, 07, 30, 00));
            result[28].Should().Be(new DateTime(2021, 01, 08, 01, 30, 00));
            result[29].Should().Be(new DateTime(2021, 01, 08, 03, 30, 00));
            result[30].Should().Be(new DateTime(2021, 01, 08, 05, 30, 00));
            result[31].Should().Be(new DateTime(2021, 01, 08, 07, 30, 00));
            result[32].Should().Be(new DateTime(2021, 01, 09, 01, 30, 00));
            result[33].Should().Be(new DateTime(2021, 01, 09, 03, 30, 00));
            result[34].Should().Be(new DateTime(2021, 01, 09, 05, 30, 00));
            result[35].Should().Be(new DateTime(2021, 01, 09, 07, 30, 00));
            result[36].Should().Be(new DateTime(2021, 01, 10, 01, 30, 00));
            result[37].Should().Be(new DateTime(2021, 01, 10, 03, 30, 00));
            result[38].Should().Be(new DateTime(2021, 01, 10, 05, 30, 00));
            result[39].Should().Be(new DateTime(2021, 01, 10, 07, 30, 00));
            result[40].Should().Be(new DateTime(2021, 01, 11, 01, 30, 00));
            result[41].Should().Be(new DateTime(2021, 01, 11, 03, 30, 00));
            result[42].Should().Be(new DateTime(2021, 01, 11, 05, 30, 00));
            result[43].Should().Be(new DateTime(2021, 01, 11, 07, 30, 00));
            result[44].Should().Be(new DateTime(2021, 01, 12, 01, 30, 00));
            result[45].Should().Be(new DateTime(2021, 01, 12, 03, 30, 00));
            result[46].Should().Be(new DateTime(2021, 01, 12, 05, 30, 00));
            result[47].Should().Be(new DateTime(2021, 01, 12, 07, 30, 00));
            result[48].Should().Be(new DateTime(2021, 01, 13, 01, 30, 00));
            result[49].Should().Be(new DateTime(2021, 01, 13, 03, 30, 00));
            result[50].Should().Be(new DateTime(2021, 01, 13, 05, 30, 00));
            result[51].Should().Be(new DateTime(2021, 01, 13, 07, 30, 00));
            result[52].Should().Be(new DateTime(2021, 01, 14, 01, 30, 00));
            result[53].Should().Be(new DateTime(2021, 01, 14, 03, 30, 00));
            result[54].Should().Be(new DateTime(2021, 01, 14, 05, 30, 00));
            result[55].Should().Be(new DateTime(2021, 01, 14, 07, 30, 00));
            result[56].Should().Be(new DateTime(2021, 01, 15, 01, 30, 00));
            result[57].Should().Be(new DateTime(2021, 01, 15, 03, 30, 00));
            result[58].Should().Be(new DateTime(2021, 01, 15, 05, 30, 00));
            result[59].Should().Be(new DateTime(2021, 01, 15, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_alternated_before_limit_every_week_recurrence()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(1);
            DateTime limit = new DateTime(2021, 02, 04);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckWednesday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 04, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 04, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 04, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 04, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 06, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 06, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 06, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 06, 07, 30, 00));
            result[8].Should().Be(new DateTime(2021, 01, 11, 01, 30, 00));
            result[9].Should().Be(new DateTime(2021, 01, 11, 03, 30, 00));
            result[10].Should().Be(new DateTime(2021, 01, 11, 05, 30, 00));
            result[11].Should().Be(new DateTime(2021, 01, 11, 07, 30, 00));
            result[12].Should().Be(new DateTime(2021, 01, 13, 01, 30, 00));
            result[13].Should().Be(new DateTime(2021, 01, 13, 03, 30, 00));
            result[14].Should().Be(new DateTime(2021, 01, 13, 05, 30, 00));
            result[15].Should().Be(new DateTime(2021, 01, 13, 07, 30, 00));
            result[16].Should().Be(new DateTime(2021, 01, 18, 01, 30, 00));
            result[17].Should().Be(new DateTime(2021, 01, 18, 03, 30, 00));
            result[18].Should().Be(new DateTime(2021, 01, 18, 05, 30, 00));
            result[19].Should().Be(new DateTime(2021, 01, 18, 07, 30, 00));
            result[20].Should().Be(new DateTime(2021, 01, 20, 01, 30, 00));
            result[21].Should().Be(new DateTime(2021, 01, 20, 03, 30, 00));
            result[22].Should().Be(new DateTime(2021, 01, 20, 05, 30, 00));
            result[23].Should().Be(new DateTime(2021, 01, 20, 07, 30, 00));
            result[24].Should().Be(new DateTime(2021, 01, 25, 01, 30, 00));
            result[25].Should().Be(new DateTime(2021, 01, 25, 03, 30, 00));
            result[26].Should().Be(new DateTime(2021, 01, 25, 05, 30, 00));
            result[27].Should().Be(new DateTime(2021, 01, 25, 07, 30, 00));
            result[28].Should().Be(new DateTime(2021, 01, 27, 01, 30, 00));
            result[29].Should().Be(new DateTime(2021, 01, 27, 03, 30, 00));
            result[30].Should().Be(new DateTime(2021, 01, 27, 05, 30, 00));
            result[31].Should().Be(new DateTime(2021, 01, 27, 07, 30, 00));
            result[32].Should().Be(new DateTime(2021, 02, 01, 01, 30, 00));
            result[33].Should().Be(new DateTime(2021, 02, 01, 03, 30, 00));
            result[34].Should().Be(new DateTime(2021, 02, 01, 05, 30, 00));
            result[35].Should().Be(new DateTime(2021, 02, 01, 07, 30, 00));
            result[36].Should().Be(new DateTime(2021, 02, 03, 01, 30, 00));
            result[37].Should().Be(new DateTime(2021, 02, 03, 03, 30, 00));
            result[38].Should().Be(new DateTime(2021, 02, 03, 05, 30, 00));
            result[39].Should().Be(new DateTime(2021, 02, 03, 07, 30, 00));
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_only_monday_checked_before_limit_every_week_recurrence()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(1);
            DateTime limit = new DateTime(2021, 02, 04);
            config.DaysOfWeek.CheckMonday(true);
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            var result = WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit);

            result[0].Should().Be(new DateTime(2021, 01, 04, 01, 30, 00));
            result[1].Should().Be(new DateTime(2021, 01, 04, 03, 30, 00));
            result[2].Should().Be(new DateTime(2021, 01, 04, 05, 30, 00));
            result[3].Should().Be(new DateTime(2021, 01, 04, 07, 30, 00));
            result[4].Should().Be(new DateTime(2021, 01, 11, 01, 30, 00));
            result[5].Should().Be(new DateTime(2021, 01, 11, 03, 30, 00));
            result[6].Should().Be(new DateTime(2021, 01, 11, 05, 30, 00));
            result[7].Should().Be(new DateTime(2021, 01, 11, 07, 30, 00));
            result[8].Should().Be(new DateTime(2021, 01, 18, 01, 30, 00));
            result[9].Should().Be(new DateTime(2021, 01, 18, 03, 30, 00));
            result[10].Should().Be(new DateTime(2021, 01, 18, 05, 30, 00));
            result[11].Should().Be(new DateTime(2021, 01, 18, 07, 30, 00));
            result[12].Should().Be(new DateTime(2021, 01, 25, 01, 30, 00));
            result[13].Should().Be(new DateTime(2021, 01, 25, 03, 30, 00));
            result[14].Should().Be(new DateTime(2021, 01, 25, 05, 30, 00));
            result[15].Should().Be(new DateTime(2021, 01, 25, 07, 30, 00));
            result[16].Should().Be(new DateTime(2021, 02, 01, 01, 30, 00));
            result[17].Should().Be(new DateTime(2021, 02, 01, 03, 30, 00));
            result[18].Should().Be(new DateTime(2021, 02, 01, 05, 30, 00));
            result[19].Should().Be(new DateTime(2021, 02, 01, 07, 30, 00));
        }

        [Fact]
        public void get_day_of_week_consider_sunday_last_should_return_7_if_sunday_instead_of_0()
        {
            var result = WeeklyRecurrenceCalculator.GetDayOfWeekIntConsideringSundayLast(DayOfWeek.Sunday);
            
            result.Should().Be(7);
        }

        [Fact]
        public void sublist_of_days_of_week_from_date_should_return_sublist_of_later_days_of_parameter_current_date_friday()
        {
            DateTime currenDate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            IList<ConfigDay> expected = new List<ConfigDay>();
            expected.Add(new ConfigDay(DayOfWeek.Friday, false));
            expected.Add(new ConfigDay(DayOfWeek.Saturday, false));
            expected.Add(new ConfigDay(DayOfWeek.Sunday, false));

            var result = WeeklyRecurrenceCalculator.SublistDaysOfWeekFromDate(currenDate, config.DaysOfWeek.Days);
            
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void sublist_of_days_of_week_from_date_should_return_sublist_of_later_days_of_parameter_current_date_tuesday()
        {
            DateTime currenDate = new DateTime(2020, 12, 29);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            IList<ConfigDay> expected = new List<ConfigDay>();
            expected.Add(new ConfigDay(DayOfWeek.Tuesday, false));
            expected.Add(new ConfigDay(DayOfWeek.Wednesday, false));
            expected.Add(new ConfigDay(DayOfWeek.Thursday, false));
            expected.Add(new ConfigDay(DayOfWeek.Friday, false));
            expected.Add(new ConfigDay(DayOfWeek.Saturday, false));
            expected.Add(new ConfigDay(DayOfWeek.Sunday, false));

            var result = WeeklyRecurrenceCalculator.SublistDaysOfWeekFromDate(currenDate, config.DaysOfWeek.Days);
            
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void check_date_is_before_limit_date_should_return_true_if_current_date_is_before_limit()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 02);
            var result = WeeklyRecurrenceCalculator.CheckDateIsBeforeLimitDate(currentDate, limit);
            
            result.Should().BeTrue();
        }

        [Fact]
        public void check_date_is_before_limit_date_should_return_false_if_current_date_is_after_limit()
        {
            DateTime currentDate = new DateTime(2021, 01, 02);
            DateTime limit = new DateTime(2021, 01, 01);
            var result = WeeklyRecurrenceCalculator.CheckDateIsBeforeLimitDate(currentDate, limit);
            
            result.Should().BeFalse();

        }

        [Fact]
        public void check_date_is_before_limit_date_should_return_false_if_current_date_is_equal_limit()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 01);
            var result = WeeklyRecurrenceCalculator.CheckDateIsBeforeLimitDate(currentDate, limit);
            
            result.Should().BeTrue();
        }

        [Fact]
        public void get_recurrence_next_start_date_should_add_every_by_7_days_to_current_monday_week_day_starting_on_friday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            int every = 2;
            DateTime expected = new DateTime(2021, 01, 11);

            var result = WeeklyRecurrenceCalculator.GetRecurrenceNextStartDate(currentDate, every);
            
            result.Should().Be(expected);
        }

        [Fact]
        public void get_recurrence_next_start_date_should_add_every_by_7_days_to_current_monday_week_day_starting_on_monday()
        {
            DateTime currentDate = new DateTime(2020, 12, 28);
            int every = 3;
            DateTime expected = new DateTime(2021, 01, 18);

            var result = WeeklyRecurrenceCalculator.GetRecurrenceNextStartDate(currentDate, every);
            
            result.Should().Be(expected);
        }

        [Fact]
        public void get_current_week_monday_should_be_monday_date_of_current_week_if_current_day_is_monday()
        {
            DateTime currentDate = new DateTime(2020, 12, 28);
            DateTime expected = new DateTime(2020, 12, 28);

            var result = WeeklyRecurrenceCalculator.GetCurrentWeekMonday(currentDate);
            
            result.Should().Be(expected);
        }

        [Fact]
        public void get_current_week_monday_should_be_monday_date_of_current_week_if_current_day_is_friday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2020, 12, 28);

            var result = WeeklyRecurrenceCalculator.GetCurrentWeekMonday(currentDate);
            
            result.Should().Be(expected);
        }

    }
}
