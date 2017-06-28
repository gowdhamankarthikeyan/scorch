using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;

namespace OrchestratorIP.ReturnTypes
{
    [ActivityData("Job Instance")]
    public class JobDetails
    {
        internal JobDetails(JobInstance jobInst)
        {
            this.CreatedBy = jobInst.job.CreatedBy;
            this.CreationTime = jobInst.job.CreationTime;
            this.Id = jobInst.job.Id.ToString();
            this.LastModifiedBy = jobInst.job.LastModifiedBy;
            this.LastModifiedTime = jobInst.job.LastModifiedTime;
            this.ParentId = jobInst.job.ParentId.ToString();
            try { this.ParentIsWaiting = Convert.ToBoolean(jobInst.job.ParentIsWaiting); }
            catch { this.ParentIsWaiting = false; }
            this.RunbookId = jobInst.job.RunbookId.ToString();
            this.RunbookServerId = jobInst.job.RunbookServerId.ToString();
            this.Status = jobInst.job.Status;
            /*
            InputParameterNameString = string.Empty;
            InputParameterValueString = string.Empty;
            OutputParameterNameString = string.Empty;
            OutputParameterValueString = string.Empty;

            int i = 0;
            foreach (string inputParamName in jobInst.InputParameters.Keys)
            {
                InputParameterNameString += inputParamName + ";";
                InputParameterValueString += jobInst.InputParameters[inputParamName] + ";";
                switch (i)
                {
                    case 0:
                        _00InputParameterName = inputParamName;
                        _00InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 1:
                        _01InputParameterName = inputParamName;
                        _01InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 2:
                        _02InputParameterName = inputParamName;
                        _02InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 3:
                        _03InputParameterName = inputParamName;
                        _03InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 4:
                        _04InputParameterName = inputParamName;
                        _04InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 5:
                        _05InputParameterName = inputParamName;
                        _05InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 6:
                        _06InputParameterName = inputParamName;
                        _06InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 7:
                        _07InputParameterName = inputParamName;
                        _07InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 8:
                        _08InputParameterName = inputParamName;
                        _08InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 9:
                        _09InputParameterName = inputParamName;
                        _09InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 10:
                        _10InputParameterName = inputParamName;
                        _10InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 11:
                        _11InputParameterName = inputParamName;
                        _11InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 12:
                        _12InputParameterName = inputParamName;
                        _12InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 13:
                        _13InputParameterName = inputParamName;
                        _13InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 14:
                        _14InputParameterName = inputParamName;
                        _14InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 15:
                        _15InputParameterName = inputParamName;
                        _15InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 16:
                        _16InputParameterName = inputParamName;
                        _16InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 17:
                        _17InputParameterName = inputParamName;
                        _17InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 18:
                        _18InputParameterName = inputParamName;
                        _18InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 19:
                        _19InputParameterName = inputParamName;
                        _19InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 20:
                        _20InputParameterName = inputParamName;
                        _20InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 21:
                        _21InputParameterName = inputParamName;
                        _21InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 22:
                        _22InputParameterName = inputParamName;
                        _22InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 23:
                        _23InputParameterName = inputParamName;
                        _23InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 24:
                        _24InputParameterName = inputParamName;
                        _24InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 25:
                        _25InputParameterName = inputParamName;
                        _25InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 26:
                        _26InputParameterName = inputParamName;
                        _26InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 27:
                        _27InputParameterName = inputParamName;
                        _27InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 28:
                        _28InputParameterName = inputParamName;
                        _28InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 29:
                        _29InputParameterName = inputParamName;
                        _29InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 30:
                        _30InputParameterName = inputParamName;
                        _30InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 31:
                        _31InputParameterName = inputParamName;
                        _31InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 32:
                        _32InputParameterName = inputParamName;
                        _32InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 33:
                        _33InputParameterName = inputParamName;
                        _33InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 34:
                        _34InputParameterName = inputParamName;
                        _34InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 35:
                        _35InputParameterName = inputParamName;
                        _35InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 36:
                        _36InputParameterName = inputParamName;
                        _36InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 37:
                        _37InputParameterName = inputParamName;
                        _37InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 38:
                        _38InputParameterName = inputParamName;
                        _38InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 39:
                        _39InputParameterName = inputParamName;
                        _39InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 40:
                        _40InputParameterName = inputParamName;
                        _40InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 41:
                        _41InputParameterName = inputParamName;
                        _41InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 42:
                        _42InputParameterName = inputParamName;
                        _42InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 43:
                        _43InputParameterName = inputParamName;
                        _43InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 44:
                        _44InputParameterName = inputParamName;
                        _44InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 45:
                        _45InputParameterName = inputParamName;
                        _45InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 46:
                        _46InputParameterName = inputParamName;
                        _46InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 47:
                        _47InputParameterName = inputParamName;
                        _47InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 48:
                        _48InputParameterName = inputParamName;
                        _48InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 49:
                        _49InputParameterName = inputParamName;
                        _49InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                    case 50:
                        _50InputParameterName = inputParamName;
                        _50InputParameterValue = jobInst.InputParameters[inputParamName];
                        break;
                }
                i++;
            }

            i = 0;
            foreach (string outputParamName in jobInst.OutputParameters.Keys)
            {
                OutputParameterNameString += outputParamName + ";";
                OutputParameterValueString += jobInst.OutputParameters[outputParamName] + ";";
                switch (i)
                {
                    case 0:
                        _00outputParameterName = outputParamName;
                        _00outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 1:
                        _01outputParameterName = outputParamName;
                        _01outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 2:
                        _02outputParameterName = outputParamName;
                        _02outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 3:
                        _03outputParameterName = outputParamName;
                        _03outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 4:
                        _04outputParameterName = outputParamName;
                        _04outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 5:
                        _05outputParameterName = outputParamName;
                        _05outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 6:
                        _06outputParameterName = outputParamName;
                        _06outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 7:
                        _07outputParameterName = outputParamName;
                        _07outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 8:
                        _08outputParameterName = outputParamName;
                        _08outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 9:
                        _09outputParameterName = outputParamName;
                        _09outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 10:
                        _10outputParameterName = outputParamName;
                        _10outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 11:
                        _11outputParameterName = outputParamName;
                        _11outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 12:
                        _12outputParameterName = outputParamName;
                        _12outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 13:
                        _13outputParameterName = outputParamName;
                        _13outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 14:
                        _14outputParameterName = outputParamName;
                        _14outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 15:
                        _15outputParameterName = outputParamName;
                        _15outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 16:
                        _16outputParameterName = outputParamName;
                        _16outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 17:
                        _17outputParameterName = outputParamName;
                        _17outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 18:
                        _18outputParameterName = outputParamName;
                        _18outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 19:
                        _19outputParameterName = outputParamName;
                        _19outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 20:
                        _20outputParameterName = outputParamName;
                        _20outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 21:
                        _21outputParameterName = outputParamName;
                        _21outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 22:
                        _22outputParameterName = outputParamName;
                        _22outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 23:
                        _23outputParameterName = outputParamName;
                        _23outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 24:
                        _24outputParameterName = outputParamName;
                        _24outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 25:
                        _25outputParameterName = outputParamName;
                        _25outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 26:
                        _26outputParameterName = outputParamName;
                        _26outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 27:
                        _27outputParameterName = outputParamName;
                        _27outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 28:
                        _28outputParameterName = outputParamName;
                        _28outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 29:
                        _29outputParameterName = outputParamName;
                        _29outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 30:
                        _30outputParameterName = outputParamName;
                        _30outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 31:
                        _31outputParameterName = outputParamName;
                        _31outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 32:
                        _32outputParameterName = outputParamName;
                        _32outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 33:
                        _33outputParameterName = outputParamName;
                        _33outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 34:
                        _34outputParameterName = outputParamName;
                        _34outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 35:
                        _35outputParameterName = outputParamName;
                        _35outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 36:
                        _36outputParameterName = outputParamName;
                        _36outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 37:
                        _37outputParameterName = outputParamName;
                        _37outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 38:
                        _38outputParameterName = outputParamName;
                        _38outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 39:
                        _39outputParameterName = outputParamName;
                        _39outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 40:
                        _40outputParameterName = outputParamName;
                        _40outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 41:
                        _41outputParameterName = outputParamName;
                        _41outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 42:
                        _42outputParameterName = outputParamName;
                        _42outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 43:
                        _43outputParameterName = outputParamName;
                        _43outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 44:
                        _44outputParameterName = outputParamName;
                        _44outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 45:
                        _45outputParameterName = outputParamName;
                        _45outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 46:
                        _46outputParameterName = outputParamName;
                        _46outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 47:
                        _47outputParameterName = outputParamName;
                        _47outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 48:
                        _48outputParameterName = outputParamName;
                        _48outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 49:
                        _49outputParameterName = outputParamName;
                        _49outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                    case 50:
                        _50outputParameterName = outputParamName;
                        _50outputParameterValue = jobInst.OutputParameters[outputParamName];
                        break;
                }
            }
             */ 

        }
        [ActivityOutput, ActivityFilter]
        public String CreatedBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime CreationTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String LastModifiedBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime LastModifiedTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String ParentId
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public Boolean ParentIsWaiting
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String RunbookId
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String RunbookServerId
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Status
        {
            get;
            set;
        }
        /*
        [ActivityOutput, ActivityFilter]
        public String InputParameterNameString
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String InputParameterValueString
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String OutputParameterNameString
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String OutputParameterValueString
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _00InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _00InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _01InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _01InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _02InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _02InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _03InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _03InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _04InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _04InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _05InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _05InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _06InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _06InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _07InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _07InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _08InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _08InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _09InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _09InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _10InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _10InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _11InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _11InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _12InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _12InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _13InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _13InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _14InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _14InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _15InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _15InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _16InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _16InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _17InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _17InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _18InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _18InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _19InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _19InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _20InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _20InputParameterValue
        {
            get;
            set;
        }
        public String _21InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _21InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _22InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _22InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _23InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _23InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _24InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _24InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _25InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _25InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _26InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _26InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _27InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _27InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _28InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _28InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _29InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _29InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _30InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _30InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _31InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _31InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _32InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _32InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _33InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _33InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _34InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _34InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _35InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _35InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _36InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _36InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _37InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _37InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _38InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _38InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _39InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _39InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _40InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _40InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _41InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _41InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _42InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _42InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _43InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _43InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _44InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _44InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _45InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _45InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _46InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _46InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _47InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _47InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _48InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _48InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _49InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _49InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _50InputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _50InputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _00outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _00outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _01outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _01outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _02outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _02outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _03outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _03outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _04outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _04outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _05outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _05outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _06outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _06outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _07outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _07outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _08outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _08outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _09outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _09outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _10outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _10outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _11outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _11outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _12outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _12outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _13outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _13outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _14outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _14outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _15outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _15outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _16outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _16outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _17outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _17outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _18outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _18outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _19outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _19outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _20outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _20outputParameterValue
        {
            get;
            set;
        }
        public String _21outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _21outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _22outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _22outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _23outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _23outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _24outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _24outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _25outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _25outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _26outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _26outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _27outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _27outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _28outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _28outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _29outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _29outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _30outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _30outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _31outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _31outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _32outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _32outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _33outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _33outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _34outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _34outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _35outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _35outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _36outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _36outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _37outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _37outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _38outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _38outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _39outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _39outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _40outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _40outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _41outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _41outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _42outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _42outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _43outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _43outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _44outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _44outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _45outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _45outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _46outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _46outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _47outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _47outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _48outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _48outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _49outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _49outputParameterValue
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _50outputParameterName
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String _50outputParameterValue
        {
            get;
            set;
        }
         */
    }
}