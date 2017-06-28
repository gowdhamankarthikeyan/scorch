using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Create Scheduled Task")]
    class CreateScheduledTask : IActivity
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
            designer.AddInput("Application Name");
            designer.AddInput("Parameters");
            designer.AddInput("Comment");
            designer.AddInput("Creator");
            designer.AddInput("Working Directory");
            designer.AddInput("Account Name");
            designer.AddInput("Password").PasswordProtect();
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

            response.WithFiltering().PublishRange(CreateScheduledTasks());
        }

        private IEnumerable<opalisTask> CreateScheduledTasks()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.CreateTask(taskName);

            t.ApplicationName = applicationName;
            t.Parameters = parameters;
            t.Comment = comment;
            t.Creator = creator;
            t.WorkingDirectory = workingDirectory;
            t.SetAccountInformation(accountName, password);

            if (!(idleWaitDeadlineMinutes.Equals(String.Empty))) { t.IdleWaitDeadlineMinutes = Convert.ToInt16(idleWaitDeadlineMinutes); }
            else { t.IdleWaitDeadlineMinutes = 20; }
            if (!(idleWaitMinutes.Equals(String.Empty))) { t.IdleWaitMinutes = Convert.ToInt16(idleWaitMinutes); }
            else { t.IdleWaitMinutes = 10; }
            if (hidden.Equals("True")) { t.Hidden = true; }
            if (!(maxRuntime.Equals(String.Empty))) { t.MaxRunTime = new TimeSpan(0,0,Convert.ToInt32(maxRuntime)); }
            else { t.MaxRunTime = new TimeSpan(1, 0, 0); }
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
            else { t.Priority = System.Diagnostics.ProcessPriorityClass.Normal; }


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
            t.Save();
            t.Close();
            yield return new opalisTask(Name, AccountName, ApplicationName, Comment,
                                        Creator, ExitCode, Flags, Hidden, IdleWaitDeadlineMinutes,
                                        IdleWaitMinutes, MaxRunTime, MaxRunTimeLimited, MostRecentRunTime,
                                        NextRunTime, Parameters, Priority, Status, WorkingDirectory);
            st.Dispose();
        }
    }
}

