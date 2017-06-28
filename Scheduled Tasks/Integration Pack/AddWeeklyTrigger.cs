using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Add Weekly Task Trigger")]
    class AddWeeklyTrigger : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private String weeksInterval = String.Empty;
        private String hour = String.Empty;
        private String minute = String.Empty;
        private String dayOfTheWeek = String.Empty;

        public void Design(IActivityDesigner designer)
        {
            string[] DOW = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");
            designer.AddInput("Weeks Interval");
            designer.AddInput("Hour");
            designer.AddInput("Minute");
            designer.AddInput("Day of the Week").WithListBrowser(DOW);
            designer.AddCorellatedData(typeof(OpalisTaskTrigger));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();
            weeksInterval = request.Inputs["Weeks Interval"].AsString();
            hour = request.Inputs["Hour"].AsString();
            minute = request.Inputs["Minute"].AsString();
            dayOfTheWeek = request.Inputs["Day of the Week"].AsString();
            
            response.WithFiltering().PublishRange(AddTrigger());
        }

        private IEnumerable<OpalisTaskTrigger> AddTrigger()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);

            WeeklyTrigger trigger = new WeeklyTrigger(0,0,DaysOfTheWeek.Friday);

            switch (dayOfTheWeek)
            {
                case "Monday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Monday, Convert.ToInt16(weeksInterval));
                    break;
                case "Tuesday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Tuesday, Convert.ToInt16(weeksInterval));
                    break;
                case "Wednesday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Wednesday, Convert.ToInt16(weeksInterval));
                    break;
                case "Thursday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Thursday, Convert.ToInt16(weeksInterval));
                    break;
                case "Friday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Friday, Convert.ToInt16(weeksInterval));
                    break;
                case "Saturday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Saturday, Convert.ToInt16(weeksInterval));
                    break;
                case "Sunday":
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Sunday, Convert.ToInt16(weeksInterval));
                    break;
                default:
                    trigger = new WeeklyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), DaysOfTheWeek.Sunday, Convert.ToInt16(weeksInterval));
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

