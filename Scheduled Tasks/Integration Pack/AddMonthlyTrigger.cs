using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Add Monthly Task Trigger")]
    class AddMonthlyTrigger : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private String weeksInterval = String.Empty;
        private String hour = String.Empty;
        private String minute = String.Empty;
        private String dayList = String.Empty;
        private String month = String.Empty;

        public void Design(IActivityDesigner designer)
        {
            string[] monthList = { "January","Febuary","March","April","May","June","July","August","September","October","November","December" };

            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");
            designer.AddInput("Day List (CSV)");
            designer.AddInput("Hour");
            designer.AddInput("Minute");
            designer.AddInput("Month").WithListBrowser(monthList);
            designer.AddCorellatedData(typeof(OpalisTaskTrigger));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();
            weeksInterval = request.Inputs["Weeks Interval"].AsString();
            hour = request.Inputs["Hour"].AsString();
            minute = request.Inputs["Minute"].AsString();
            month = request.Inputs["Month"].AsString();
            dayList = request.Inputs["Day List (CSV)"].AsString();

            response.WithFiltering().PublishRange(AddTrigger());
        }

        private IEnumerable<OpalisTaskTrigger> AddTrigger()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);

            string[] dayStringArray = dayList.Split(',');

            int[] dayArray = new int[dayStringArray.Length];

            for (int i = 0 ; i < dayArray.Length ; i++)
            {
                dayArray[i] = Convert.ToInt16(dayStringArray[i]);
            }
            
            MonthlyTrigger trigger = new MonthlyTrigger(0,0,dayArray);

            switch (month)
            {
                case "January":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.January);
                    break;
                case "Febuary":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.February);
                    break;
                case "March":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.March);
                    break;
                case "April":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.April);
                    break;
                case "May":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.May);
                    break;
                case "June":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.June);
                    break;
                case "July":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.July);
                    break;
                case "August":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.August);
                    break;
                case "September":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.September);
                    break;
                case "October":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.October);
                    break;
                case "November":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.November);
                    break;
                case "December":
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.December);
                    break;
                default:
                    trigger = new MonthlyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), dayArray, MonthsOfTheYear.August);
                    break;
            }

            t.Triggers.Add(trigger);
            t.Save();

            String Type = trigger.GetType().ToString().Split('.')[1];
            String BeginDay = trigger.taskTrigger.BeginDay.ToString();
            String BeginMonth = trigger.taskTrigger.BeginMonth.ToString();
            String BeginYear = trigger.taskTrigger.BeginYear.ToString();
            String EndDay = trigger.taskTrigger.EndDay.ToString();
            String EndMonth = trigger.taskTrigger.EndMonth.ToString();
            String EndYear = trigger.taskTrigger.EndYear.ToString();
            String flags = trigger.taskTrigger.Flags.ToString();
            String MinutesDuration = trigger.taskTrigger.MinutesDuration.ToString();
            String MinutesInterval = trigger.taskTrigger.MinutesInterval.ToString();
            String RandomMinutesInterval = trigger.taskTrigger.RandomMinutesInterval.ToString();
            String StartHour = trigger.taskTrigger.StartHour.ToString();
            String StartMinute = trigger.taskTrigger.StartMinute.ToString();
            String TriggerSize = trigger.taskTrigger.TriggerSize.ToString();
            String Bound = trigger.Bound.ToString();
            String Disabled = trigger.Disabled.ToString();
            String HasEndDate = trigger.HasEndDate.ToString();
            String KillAtDurationEnd = trigger.KillAtDurationEnd.ToString();

            String DaysInterval = trigger.taskTrigger.Data.daily.DaysInterval.ToString();
            String MonthlyDateDays = trigger.taskTrigger.Data.monthlyDate.Days.ToString();
            String MonthlyDateMonths = trigger.taskTrigger.Data.monthlyDate.Months.ToString();
            String MonthlyDOWDaysOfTheWeek = trigger.taskTrigger.Data.monthlyDOW.DaysOfTheWeek.ToString();
            String MonthlyDOWMonths = trigger.taskTrigger.Data.monthlyDOW.Months.ToString();
            String MonthlyDOWWhichWeek = trigger.taskTrigger.Data.monthlyDOW.WhichWeek.ToString();
            String weeklyDaysOfTheWeek = trigger.taskTrigger.Data.weekly.DaysOfTheWeek.ToString();
            String weeklyWeeksInterval = trigger.taskTrigger.Data.weekly.WeeksInterval.ToString();

            String DisplayString = trigger.ToString();

            yield return new OpalisTaskTrigger(Type, BeginDay, BeginMonth, BeginYear, EndDay, EndMonth, EndYear, flags,
                                               MinutesDuration, MinutesInterval, RandomMinutesInterval, StartHour,
                                               StartMinute, TriggerSize, Bound, Disabled, HasEndDate, KillAtDurationEnd,
                                               DaysInterval, MonthlyDateDays, MonthlyDateMonths, MonthlyDOWDaysOfTheWeek,
                                               MonthlyDOWMonths, MonthlyDOWWhichWeek, weeklyDaysOfTheWeek, weeklyWeeksInterval,
                                               DisplayString);
            t.Close();
            st.Dispose();
        }
    }
}

