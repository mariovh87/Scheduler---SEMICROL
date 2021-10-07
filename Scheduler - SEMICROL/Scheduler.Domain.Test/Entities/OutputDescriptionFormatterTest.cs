using FluentAssertions;
using Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using static Scheduler.Domain.Common.SchedulerEnums;

namespace Scheduler.Domain.Tests.Entities
{
    public class OutputDescriptionFormatterTest
    {
        [Fact]
        public void occurs_text_returns_once_if_configuration_type_is_once()
        {
            OutputDescriptionFormatter.GetOccurs(ConfigurationType.Once, RecurringType.Daily, 1).Should().Be(ConfigurationType.Once.ToString());
        }

        [Fact]
        public void occurs_text_returns_get_recurrent_type_string_if_configuration_type_is_recurring()
        {
            OutputDescriptionFormatter.GetOccurs(ConfigurationType.Once, RecurringType.Daily, 1)
                .Should().Be(OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Daily, 1));
        }

        [Fact]
        public void get_recurring_type_should_return_month_if_occurs_monthly()
        {
            OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Monthly, 1)
                .Should().Be("Month");
        }

        [Fact]
        public void get_recurring_type_should_return_months_if_occurs_monthly_with_every_bigger_than_one()
        {
            OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Monthly, 2)
                .Should().Be("Months");
        }

        [Fact]
        public void get_recurring_type_should_return_year_if_occurs_yearly()
        {
            OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Yearly, 1)
                .Should().Be("Year");
        }

        [Fact]
        public void get_recurring_type_should_return_years_if_occurs_yearly_with_every_bigger_than_one()
        {
            OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Monthly, 2)
                .Should().Be("Years");
        }

        [Fact]
        public void get_recurring_type_should_return_day_if_occurs_daily()
        {
            OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Daily, 1)
                .Should().Be("Day");
        }

        [Fact]
        public void get_recurring_type_should_return_days_if_occurs_daily_with_every_bigger_than_one()
        {
            OutputDescriptionFormatter.GetRecurringTypeString(RecurringType.Daily, 2)
                .Should().Be("Days");
        }

        [Fact]
        public void description_should_return_formated_string()
        {
            FieldInfo field = typeof(Domain.Entities.Scheduler).GetField("format");
            
            OutputDescriptionFormatter.Description(new DateTime(01,01,2021), ConfigurationType.Once, RecurringType.Daily, 2, new DateTime(01,01,2020))
                .Should()
                .Be(String.Format(field.GetValue(null).ToString(),
                    OutputDescriptionFormatter.GetOccurs(ConfigurationType.Once, RecurringType.Daily, 2), new DateTime(01, 01, 2021), new DateTime(01, 01, 2020)));
        }
    }
}
