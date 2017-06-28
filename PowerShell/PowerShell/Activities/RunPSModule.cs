using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using SCORCHDev.PowerShell;
using SCORCHDev.PowerShell.Classes;

namespace SCORCHDev.PowerShell.Activities
{
    [Activity("Run PowerShell Module CMDLet", ShowFilters = false, ShowInputs = true)]
    class RunPSModule : IActivityWithRedesign
    {
        private string _moduleName;
        private string _cmdletName;
        private string _parameterSetName;
        private bool _onlyOneParameterset = false;
        private Runspace _runspace = null;
        
        public void Design(IActivityDesigner designer)
        {
            // Design is called when the form is opened for the first time (from an unsaved state) or
            // when the connection setting is changed.
            CreateAndOpenRunspace();
            AddModuleNames(designer);
            CloseRunspace();

            designer.AddCorellatedData(typeof(PowerShellOutput));
        }
        public void Redesign(IActivityDesigner designer, IRedesignRequest request)
        {
            CreateAndOpenRunspace();
            NotificationPopup dlg = new NotificationPopup();

            dlg.Show();

            SetCurrentInputValues(request.RedesignInputs);

            if (string.IsNullOrEmpty(request.ChangedPropertyName))
            {
                //Redesign was called because the user opened a saved activity in the UI (no property changed)

                AddModuleNames(designer);
                dlg.ProgressValue = 20;
                AddCmdletNames(designer);
                dlg.ProgressValue = 40;
                AddParametersets(designer);
                dlg.ProgressValue = 70;
                AddCmdletProperties(designer);
                dlg.ProgressValue = 90;

                dlg.Close();
                return;
            }



            switch (request.ChangedPropertyName)
            {
                case ("Module Name"):
                    _cmdletName = string.Empty;
                    _parameterSetName = string.Empty;
                    break;

                case ("Cmdlet Name"):
                    _parameterSetName = string.Empty;
                    break;

                case ("ParameterSet"):
                    break;
                case ("Output Parameter Count"):
                    break;
                default:
                    //should not be here.
                    break;
            }

            AddModuleNames(designer);
            dlg.ProgressValue = 20;
            AddCmdletNames(designer);
            dlg.ProgressValue = 40;
            AddParametersets(designer);
            dlg.ProgressValue = 70;
            AddCmdletProperties(designer);
            dlg.ProgressValue = 90;

            dlg.Close();
            CloseRunspace();
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string _moduleName = string.Empty;
            string _cmdletName = string.Empty;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (string input in request.Inputs.Names)
            {
                string _parameter = string.Empty;
                string _type = string.Empty;
                string _value = string.Empty;
                switch (input)
                {
                    case ("Module Name"):
                        _moduleName = request.Inputs[input].AsString();
                        break;
                    case ("Cmdlet Name"):
                        _cmdletName = request.Inputs[input].AsString();
                        break;
                    case ("ParameterSet"):
                        break;
                    default:
                        parseInputFieldName(input, out _type, out _parameter);
                        switch (_type)
                        {
                            case ("String"):
                                _value = request.Inputs[input].AsString();
                                break;
                            case ("Int32"):
                            case ("UInt32"):
                                _value = Convert.ToString(request.Inputs[input].AsInt32());
                                break;
                            case ("SwitchParameter"):
                                _value = Convert.ToString(request.Inputs[input].AsString());
                                break;
                            case ("ActionPreference"):
                                _value = Convert.ToString(request.Inputs[input].AsString());
                                break;
                            default:
                                _value = request.Inputs[input].AsString();
                                break;
                        }
                        parameters.Add(_parameter, _value);
                        break;
                }
                
            }
            CreateAndOpenRunspace();

            StringBuilder psScript = new StringBuilder(string.Format("import-module {0}; {1}", _moduleName, _cmdletName));

            foreach (string key in parameters.Keys)
            {
                psScript.Append(string.Format(" -{0} {1}", key, parameters[key]));
            }

            Collection<PSObject> objCol = RunPowerShellScript(psScript.ToString());

            response.Publish("Cmdlet Name", _cmdletName);
            response.Publish("Module Name", _moduleName);

            if (objCol.Count == 1) { response.PublishRange(parseResults(objCol[0])); }

            CloseRunspace();
        }
        private IEnumerable<PowerShellOutput> parseResults(PSObject obj)
        {
            PSMemberInfoCollection<PSPropertyInfo> retValueProperties = obj.Properties;
            foreach (PSPropertyInfo pInfo in retValueProperties)
            {
                yield return new PowerShellOutput(pInfo);
            }
        }
        private void SetCurrentInputValues(IInputCollection inputs)
        {
            foreach (KeyValuePair<string, IRuntimeValue> input in inputs)
            {

                switch (input.Key)
                {
                    case ("Module Name"):
                        _moduleName = input.Value.ToString();
                        break;

                    case ("Cmdlet Name"):
                        _cmdletName = input.Value.ToString();
                        break;

                    case ("ParameterSet"):
                        _parameterSetName = input.Value.ToString();
                        break;
                    default:
                        //should not be here!
                        break;
                }
            }
        }
        private void AddModuleNames(IActivityDesigner designer)
        {
            string[] names;

            string psScript = "get-module -ListAvailable ";
            Collection<PSObject> returnValues = RunPowerShellScript(psScript);

            names = new string[returnValues.Count];

            for (int i = 0; i < returnValues.Count; i++)
            {
                names[i] = returnValues[i].Properties["Name"].Value.ToString();
            }


            if (string.IsNullOrEmpty(_moduleName))
            {
                designer.AddInput("Module Name").WithListBrowser(names).WithRedesign();
            }
            else
            {
                designer.AddInput("Module Name").WithListBrowser(names).WithRedesign().WithDefaultValue(_moduleName);
            }
            designer.AddOutput("Module Name");

        }
        private void AddCmdletNames(IActivityDesigner designer)
        {
            if (string.IsNullOrEmpty(_moduleName))
            {
                //skip adding this if the required dependency property is not yet set
                return;
            }
            string[] names;

            
            string psScript = string.Format("import-module {0}; gcm -Module {0}", _moduleName);
            Collection<PSObject> returnValues = RunPowerShellScript(psScript);
            names = new string[returnValues.Count];

            for (int i = 0; i < returnValues.Count; i++)
            {
                names[i] = returnValues[i].Properties["Name"].Value.ToString();
            }

            if (string.IsNullOrEmpty(_cmdletName))
            {
                designer.AddInput("Cmdlet Name").WithListBrowser(names).WithRedesign();
            }
            else
            {
                designer.AddInput("Cmdlet Name").WithListBrowser(names).WithRedesign().WithDefaultValue(_cmdletName);
            }
            designer.AddOutput("Cmdlet Name");
        }
        private void AddParametersets(IActivityDesigner designer)
        {
            if (string.IsNullOrEmpty(_cmdletName))
            {
                //skip adding this if the required dependency property is not yet set
                return;
            }

            string[] names;

            
            string psScript = string.Format("$(import-module {0}; gcm -Name {1} -Module {0} | Select parametersets).ParameterSets ", _moduleName, _cmdletName);
            Collection<PSObject> returnValues = RunPowerShellScript(psScript);
            string defaultSet = string.Empty;

            if (returnValues.Count > 1)
            {
                names = new string[returnValues.Count];
                for (int i = 0; i < returnValues.Count; i++)
                {
                    names[i] = returnValues[i].Properties["Name"].Value.ToString();
                    defaultSet = ((bool)returnValues[i].Properties["IsDefault"].Value) ? names[i] : string.Empty;
                }
            }
            else
            {
                //there is only one parameterset - no need to display a selection for it
                _onlyOneParameterset = true;
                _parameterSetName = returnValues[0].Properties["Name"].Value.ToString();
                names = null;
            }

            if (names != null)
            {
                if (string.IsNullOrEmpty(_parameterSetName))
                {
                    designer.AddInput("ParameterSet").WithListBrowser(names).WithRedesign();

                }
                else
                {
                    designer.AddInput("ParameterSet").WithListBrowser(names).WithRedesign().WithDefaultValue(_parameterSetName);
                }
                designer.AddOutput("ParameterSet");
            }

        }
        private void AddCmdletProperties(IActivityDesigner designer)
        {
            if ((string.IsNullOrEmpty(_parameterSetName)) && (_onlyOneParameterset == false))
            {
                //skip adding this if the required dependency property is not yet set
                return;
            }
            if (_parameterSetName.StartsWith("ParameterSet"))
            {
                return;
            }

            string psScript = string.Format("$($(import-module {0}; gcm -Name {1} -Module {0} | Select parametersets).ParameterSets | ? {{$_.Name -eq '{2}' }}).Parameters ", _moduleName, _cmdletName, _parameterSetName);
            Collection<PSObject> returnValues = RunPowerShellScript(psScript);
            //string[] names = new string[returnValues.Count];

            foreach (PSObject returnValue in returnValues)
            {
                AddInput(designer, returnValue);
            }
        }
        private void AddInput(IActivityDesigner designer, PSObject parameter)
        {
            if ((parameter == null) ||
                (parameter.Properties["Name"] == null) ||
                (parameter.Properties["ParameterType"] == null))
            {
                return;
            }
            string propertyName = parameter.Properties["Name"].Value.ToString();
            string propertyType = parameter.Properties["ParameterType"].Value.ToString();
            bool isMandatory = Boolean.Parse(parameter.Properties["IsMandatory"].Value.ToString());

            switch (propertyType)
            {
                case ("System.String"):
                    if (isMandatory)
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType));
                    }
                    else
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType)).NotRequired();
                    }
                    // designer.AddOutput(propertyName).WithDescription(propertyType).AsString();
                    break;
                case ("System.Int32"):
                case ("System.UInt32"):
                    if (isMandatory)
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType));
                    }
                    else
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType)).NotRequired();
                    }
                    // designer.AddOutput(propertyName).WithDescription(propertyType).AsNumber();
                    break;
                case ("System.Management.Automation.SwitchParameter"):
                    if (isMandatory)
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType)).WithBooleanBrowser();
                    }
                    else
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType)).WithBooleanBrowser().NotRequired();
                    }
                    // designer.AddOutput(propertyName).WithDescription(propertyType).AsString();
                    break;
                case ("System.Management.Automation.ActionPreference"):
                    string[] options = SetListValuesFromEnum(propertyType);
                    if (options == null)
                    {
                        if (isMandatory)
                        {
                            designer.AddInput(CreateInputFieldName(propertyName, propertyType));
                        }
                        else
                        {
                            designer.AddInput(CreateInputFieldName(propertyName, propertyType)).NotRequired();
                        }
                    }
                    else
                    {
                        if (isMandatory)
                        {
                            designer.AddInput(CreateInputFieldName(propertyName, propertyType)).WithListBrowser(options);
                        }
                        else
                        {
                            designer.AddInput(CreateInputFieldName(propertyName, propertyType)).WithListBrowser(options).NotRequired();
                        }
                    }
                    designer.AddOutput(propertyName).WithDescription(propertyType).AsString();
                    break;
                default:
                    if (isMandatory)
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType));
                    }
                    else
                    {
                        designer.AddInput(CreateInputFieldName(propertyName, propertyType)).NotRequired();
                    }
                    designer.AddOutput(propertyName).WithDescription(propertyType).AsString();
                    break;
            }
        }
        private void CreateAndOpenRunspace()
        {
            if (_runspace == null)
            {
                _runspace = RunspaceFactory.CreateRunspace();
            }
            _runspace.Open();
        }
        private void CloseRunspace()
        {
            _runspace.Close();
        }
        private Collection<PSObject> RunPowerShellScript(string scriptText)
        {

            Pipeline pipeline = _runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);
            Collection<PSObject> results = pipeline.Invoke();
            return results;
        }
        private string[] SetListValuesFromEnum(string typeName)
        {
            string[] values = null;

            Type myType = Type.GetType(typeName);
            if (myType != null)
            {
                values = Enum.GetNames(myType);
                return values;
            }
            //get list values for other known types:
            if (typeName == "System.Management.Automation.ActionPreference")
            {
                values = new string[5];
                values[0] = "Continue";
                values[1] = "Ignore";
                values[2] = "Inquire";
                values[3] = "SilentlyContinue";
                values[4] = "Stop";

            }
            return values;
        }
        private string CreateInputFieldName(string propertyName, string typeName)
        {
            string suffix = string.Format("[{0}]", typeName.Substring(typeName.LastIndexOf(".") + 1));
            string name = string.Format("{0}\t{1}", propertyName, suffix);
            return name;
        }
        private void parseInputFieldName(string inputString, out string type, out string parameterName)
        {
            string[] nameArray = inputString.Split(new char[] { '[', ']', '\t' });
            type = nameArray[nameArray.Length - 1];
            parameterName = nameArray[0];
        }
    }
}
