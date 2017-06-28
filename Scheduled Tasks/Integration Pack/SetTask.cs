using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Set Scheduled Task")]
    class SetTask : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private String applicationName = String.Empty;
        private String parameters = String.Empty;
        private String comment = String.Empty;
        private String creator = String.Empty;
        private String workingDirectory = String.Empty;
        private String accountName = String.Empty;
        private String password = String.Empty;
        private String hidden = String.Empty;
        private String idleWaitDeadlineMinutes = String.Empty;
        private String idleWaitMinutes = String.Empty;
        private String maxRuntime = String.Empty;
        private String priority = String.Empty;

        public void Design(IActivityDesigner designer)
        {
            string[] PriorityOptions = new string[6];
            PriorityOptions[0] = "AboveNormal";
            PriorityOptions[1] = "BelowNormal";
            PriorityOptions[2] = "High";
            PriorityOptions[3] = "Idle";
            PriorityOptions[4] = "Normal";
            PriorityOptions[5] = "Realtime";

            string[] hiddenOptions = { "True", "False" };

            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");
            designer.AddInput("Application Name").NotRequired();
            designer.AddInput("Parameters").NotRequired();
            designer.AddInput("Comment").NotRequired();
            designer.AddInput("Creator").NotRequired();
            designer.AddInput("Working Directory").NotRequired();
            designer.AddInput("Account Name").NotRequired();
            designer.AddInput("Password").PasswordProtect().NotRequired();
            designer.AddInput("Hidden").NotRequired().WithListBrowser(hiddenOptions);
            designer.AddInput("IdleWaitDeadlineMinutes").NotRequired();
            designer.AddInput("IdleWaitMinutes").NotRequired();
            designer.AddInput("Max Runtime (Seconds)").NotRequired();
            designer.AddInput("Priority").NotRequired().WithListBrowser(PriorityOptions);

            designer.AddCorellatedData(typeof(opalisTask));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();
            applicationName = request.Inputs["Application Name"].AsString();
            parameters = request.Inputs["Parameters"].AsString();
            comment = request.Inputs["Comment"].AsString();
            creator = request.Inputs["Creator"].AsString();
            workingDirectory = request.Inputs["Working Directory"].AsString();
            accountName = request.Inputs["Account Name"].AsString();
            password = request.Inputs["Password"].AsString();
            hidden = request.Inputs["Hidden"].AsString();
            idleWaitDeadlineMinutes = request.Inputs["IdleWaitDeadlineMinutes"].AsString();
            idleWaitMinutes = request.Inputs["IdleWaitMinutes"].AsString();
            maxRuntime = request.Inputs["Max Runtime (Seconds)"].AsString();
            priority = request.Inputs["Priority"].AsString();

            response.WithFiltering().PublishRange(SetScheduledTasks());
        }

        private IEnumerable<opalisTask> SetScheduledTasks()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);

            if (!(applicationName.Equals(String.Empty))) { t.ApplicationName = applicationName; }
            if (!(parameters.Equals(String.Empty))) { t.Parameters = parameters; }
            if (!(comment.Equals(String.Empty))) { t.Comment = comment; }
            if (!(creator.Equals(String.Empty))) { t.Creator = creator; }
            if (!(workingDirectory.Equals(String.Empty))) { t.WorkingDirectory = workingDirectory; }
            if (!((accountName.Equals(String.Empty)) & (password.Equals(string.Empty)))) { t.SetAccountInformation(accountName, password); }
            if (!(idleWaitDeadlineMinutes.Equals(String.Empty))) { t.IdleWaitDeadlineMinutes = Convert.ToInt16(idleWaitDeadlineMinutes); }
            if (!(idleWaitMinutes.Equals(String.Empty))) { t.IdleWaitMinutes = Convert.ToInt16(idleWaitMinutes); }
            if ((hidden.Equals("True"))) { t.Hidden = true; }
            if (!(maxRuntime.Equals(String.Empty))) { t.MaxRunTime = new TimeSpan(0, 0, Convert.ToInt32(maxRuntime)); }
            if (!(priority.Equals(String.Empty)))
            {
                switch (priority)
                {
                    case "AboveNormal":
                        t.Priority = System.Diagnostics.ProcessPriorityClass.AboveNormal;
                        break;
                    case "BelowNormal":
                        t.Priority = System.Diagnostics.ProcessPriorityClass.BelowNormal;
                        break;
                    case "High":
                        t.Priority = System.Diagnostics.ProcessPriorityClass.High;
                        break;
                    case "Idle":
                        t.Priority = System.Diagnostics.ProcessPriorityClass.Idle;
                        break;
                    case "Normal":
                        t.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
                        break;
                    case "RealTime":
                        t.Priority = System.Diagnostics.ProcessPriorityClass.RealTime;
                        break;
                }
            }

            t.Save();
            t.Close();

            t = st.OpenTask(taskName);

            String AccountName = t.AccountName.ToString();
            String ApplicationName = t.ApplicationName.ToString();
            String Comment = t.Comment.ToString();
            String Creator = t.Creator.ToString();
            String ExitCode = t.ExitCode.ToString();
            String Flags = t.Flags.ToString();
            String Hidden = t.Hidden.ToString();
            String IdleWaitDeadlineMinutes = t.IdleWaitDeadlineMinutes.ToString();
            String IdleWaitMinutes = t.IdleWaitMinutes.ToString();
            String MaxRunTime = t.MaxRunTime.ToString();
            String MaxRunTimeLimited = t.MaxRunTimeLimited.ToString();
            String MostRecentRunTime = t.MostRecentRunTime.ToString();
            String Name = t.Name.ToString();
            String NextRunTime = t.NextRunTime.ToString();
            String Parameters = t.Parameters.ToString();
            String Priority = t.Priority.ToString();
            String Status = t.Status.ToString();
            String WorkingDirectory = t.WorkingDirectory.ToString();

            t.Close();
            yield return new opalisTask(Name, AccountName, ApplicationName, Comment,
                                        Creator, ExitCode, Flags, Hidden, IdleWaitDeadlineMinutes,
                                        IdleWaitMinutes, MaxRunTime, MaxRunTimeLimited, MostRecentRunTime,
                                        NextRunTime, Parameters, Priority, Status, WorkingDirectory);
            st.Dispose();
        }
    }
}

