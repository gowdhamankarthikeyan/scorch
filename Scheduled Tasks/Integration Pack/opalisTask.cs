using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [ActivityData("Opalis Scheduled Task")]
    class opalisTask
    {
        private String taskName = String.Empty;
        private String AccountName = String.Empty;
        private String ApplicationName = String.Empty;
        private String Comment = String.Empty;
        private String Creator = String.Empty;
        private String ExitCode = String.Empty;
        private String Flags = String.Empty;
        private String Hidden = String.Empty;
        private String IdleWaitDeadlineMinutes = String.Empty;
        private String IdleWaitMinutes = String.Empty;
        private String MaxRunTime = String.Empty;
        private String MaxRunTimeLimited = String.Empty;
        private String MostRecentRunTime = String.Empty;
        private String NextRunTime = String.Empty;
        private String Parameters = String.Empty;
        private String Priority = String.Empty;
        private String Status = String.Empty;
        private String WorkingDirectory = String.Empty;

        internal opalisTask(String taskName, String AccountName, String ApplicationName, String Comment, String Creator, 
                            String ExitCode, String Flags, String Hidden, String IdleWaitDeadlineMinutes, String IdleWaitMinutes,
                            String MaxRunTime, String MaxRunTimeLimited, String MostRecentRunTime, String NextRunTime, 
                            String Parameters, String Priority, String Status, String WorkingDirectory)
        {
            this.taskName = taskName;
            this.AccountName = AccountName;
            this.ApplicationName = ApplicationName;
            this.Comment = Comment;
            this.Creator = Creator;
            this.ExitCode = ExitCode;
            this.Flags = Flags;
            this.Hidden = Hidden;
            this.IdleWaitDeadlineMinutes = IdleWaitDeadlineMinutes;
            this.IdleWaitMinutes = IdleWaitMinutes;
            this.MaxRunTime = MaxRunTime;
            this.MaxRunTimeLimited = MaxRunTimeLimited;
            this.MostRecentRunTime = MostRecentRunTime;
            this.NextRunTime = NextRunTime;
            this.Parameters = Parameters;
            this.Priority = Priority;
            this.Status = Status;
            this.WorkingDirectory = WorkingDirectory;
        }

        [ActivityOutput, ActivityFilter]
        public String Task_Name
        {
            get { return taskName; }
        }
        [ActivityOutput, ActivityFilter]
        public String Account_Name
        {
            get { return AccountName; }
        }
        [ActivityOutput, ActivityFilter]
        public String Application_Name
        {
            get { return ApplicationName; }
        }
        [ActivityOutput, ActivityFilter]
        public String comment
        {
            get { return Comment; }
        }
        [ActivityOutput, ActivityFilter]
        public String creator
        {
            get { return Creator; }
        }
        [ActivityOutput, ActivityFilter]
        public String Exit_Code
        {
            get { return ExitCode; }
        }
        [ActivityOutput, ActivityFilter]
        public String flags
        {
            get { return Flags; }
        }
        [ActivityOutput, ActivityFilter]
        public String hidden
        {
            get { return Hidden; }
        }
        [ActivityOutput, ActivityFilter]
        public String Idle_Wait_Deadline_Minutes
        {
            get { return IdleWaitDeadlineMinutes; }
        }
        [ActivityOutput, ActivityFilter]
        public String Idle_Wait_Minutes
        {
            get { return IdleWaitMinutes; }
        }
        [ActivityOutput, ActivityFilter]
        public String Max_Run_Time
        {
            get { return MaxRunTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String Max_Run_Time_Limited
        {
            get { return MaxRunTimeLimited; }
        }
        [ActivityOutput, ActivityFilter]
        public String Most_Recent_Run_Time
        {
            get { return MostRecentRunTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String Next_Run_Time
        {
            get { return NextRunTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String parameters
        {
            get { return Parameters; }
        }
        [ActivityOutput, ActivityFilter]
        public String priority
        {
            get { return Priority; }
        }
        [ActivityOutput, ActivityFilter]
        public String status
        {
            get { return Status; }
        }
        [ActivityOutput, ActivityFilter]
        public String Working_Directory
        {
            get { return WorkingDirectory; }
        }
    }
}

