Add-Type @'
public class SPListItem
{
    private string _id;
    private System.DateTime _created;
    private System.DateTime _modified;
    private string _version;
    private System.Collections.Generic.Dictionary<string, object> _properties = new System.Collections.Generic.Dictionary<string, object>();
    private System.Collections.Generic.Dictionary<string, SPListItemImmutable> _linkedItems = new System.Collections.Generic.Dictionary<string, SPListItemImmutable>();
    private System.Collections.ObjectModel.ReadOnlyDictionary<string, SPListItemImmutable> _linkedItemsReadOnly;

    public string Id { get { return _id; } }
    public System.DateTime Created { get { return _created; } }
    public System.DateTime Modified { get { return _modified; } }
    public string Version { get { return _version; } }
    public System.Collections.Generic.Dictionary<string, object> Properties { get { return _properties; } set { _properties = value; } }
    public System.Collections.Generic.IReadOnlyDictionary<string, SPListItemImmutable> LinkedItems { get { return _linkedItemsReadOnly; } }

    public void addLinkedItem(string key, SPListItemImmutable ItemToAdd)
    {
        _linkedItems.Add(key, ItemToAdd);
        _linkedItemsReadOnly = new System.Collections.ObjectModel.ReadOnlyDictionary<string, SPListItemImmutable>(_linkedItems);
    }

    public SPListItem(string Id,
                        System.DateTime Created,
                        System.DateTime Modified,
                        string Version,
                        System.Collections.Generic.Dictionary<string, object> Properties)
    {
        _id = Id;
        _created = Created;
        _modified = Modified;
        _version = Version;
        _properties = Properties;

        _linkedItemsReadOnly = new System.Collections.ObjectModel.ReadOnlyDictionary<string, SPListItemImmutable>(_linkedItems);
    }
    public SPListItem(string Id,
                        System.DateTime Created,
                        System.DateTime Modified,
                        string Version)
    {
        _id = Id;
        _created = Created;
        _modified = Modified;
        _version = Version;

        _linkedItemsReadOnly = new System.Collections.ObjectModel.ReadOnlyDictionary<string, SPListItemImmutable>(_linkedItems);
    }
}
public class SPListItemImmutable
{
    private string _id;
    private System.DateTime _created;
    private System.DateTime _modified;
    private string _version;
    private System.Collections.ObjectModel.ReadOnlyDictionary<string, object> _properties;

    public string Id { get { return _id; } }
    public System.DateTime Created { get { return _created; } }
    public System.DateTime Modified { get { return _modified; } }
    public string Version { get { return _version; } }
    public System.Collections.ObjectModel.ReadOnlyDictionary<string, object> Properties { get { return _properties; } }

    public SPListItemImmutable(string Id,
                                System.DateTime Created,
                                System.DateTime Modified,
                                string Version,
                                System.Collections.Generic.Dictionary<string, object> Properties)
    {
        _id = Id;
        _created = Created;
        _modified = Modified;
        _version = Version;
        _properties = new System.Collections.ObjectModel.ReadOnlyDictionary<string, object> (Properties);
    }
    public SPListItemImmutable(string Id,
                                System.DateTime Created,
                                System.DateTime Modified,
                                string Version)
    {
        _id = Id;
        _created = Created;
        _modified = Modified;
        _version = Version;
    }
}
'@
<#
    .SYNOPSIS
        Runs a rest query and either uses a PSCredential or not

    .OUTPUTS
        Results of the rest query

    .PARAMETER Uri
        Specifies the Uniform Resource Identifier (URI) of the Internet resource to which the web request is sent. This parameter supports HTTP, HTTPS, FTP, and FILE values.

    .PARAMETER Method
        Specifies the method used for the web request. Valid values are Default, Delete, Get, Head, Merge, Options, Patch, Post, Put, and Trace.

    .PARAMETER Body
        Specifies the body of the request. The body is the content of the request that follows the headers. You can also pipe a body value to Invoke-RestMethod.
        
        The Body parameter can be used to specify a list of query parameters or specify the content of the response.
        
        When the input is a GET request, and the body is an IDictionary (typically, a hash table), the body is added to the URI as query parameters. For other request types (such as POST), the body is set as the value of the request body in the standard 
        name=value format.
        
        When the body is a form, or it is the output of another Invoke-WebRequest call, Windows PowerShell sets the request content to the form fields.
        
        For example:
        
        $r = Invoke-WebRequest http://website.com/login.aspx
        $r.Forms[0].Name = "MyName"
        $r.Forms[0].Password = "MyPassword"
        Invoke-RestMethod http://website.com/service.aspx -Body $r
        
        - or -
        
        Invoke-RestMethod http://website.com/service.aspx -Body $r.Forms[0]
    
    .PARAMETER Headers
        Specifies the headers of the web request. Enter a hash table or dictionary.
        
        To set UserAgent headers, use the UserAgent parameter. You cannot use this parameter to specify UserAgent or cookie headers.

    .PARAMETER ContentType
        Specifies the content type of the web request.
        
        If this parameter is omitted and the request method is POST, Invoke-RestMethod sets the content type to "application/x-www-form-urlencoded". Otherwise, the content type is not specified in the call.

    .PARAMETER Credential
        The PSCredential to use for the query. If not passed used default credentials
#>
Function Invoke-RestMethod-Wrapped
{
    Param([Parameter(Mandatory=$True) ][string] $Uri,
          [Parameter(Mandatory=$False)][Microsoft.PowerShell.Commands.WebRequestMethod] $Method,
          [Parameter(Mandatory=$False)][object] $Body,
          [Parameter(Mandatory=$False)][string] $ContentType,
          [Parameter(Mandatory=$False)][System.Collections.IDictionary] $Headers,
          [Parameter(Mandatory=$False)][pscredential] $Credential)
    
    $null = $(
                              $RestMethodParameters  = @{ "URI"         = $URI         }

        If ( $Body        ) { $RestMethodParameters += @{ "Body"        = $Body        } }
        If ( $Method      ) { $RestMethodParameters += @{ "Method"      = $Method      } }
        If ( $Headers     ) { $RestMethodParameters += @{ "Headers"     = $Headers     } }
        If ( $ContentType ) { $RestMethodParameters += @{ "ContentType" = $ContentType } }

        If ( $Credential  ) { $RestMethodParameters += @{ "Credential" = $Credential } }
        Else                { $RestMethodParameters += @{ "UseDefaultCredentials" = $True } }

        $Results = $null
        $Results = Invoke-RestMethod @RestMethodParameters
    )
    return $Results
}
<#
    .SYNOPSIS
        Creates a well formed Uri for a SharePoint site with or without
        a list and list filter

    .OUTPUTS
        [string] -URI of the SharePoint site to query

    .PARAMETER SPFarm
        The farm to generate the Uri for
        Ex: solutions.contoso.com

    .PARAMETER SPSite
        The Site to generate the Uri for
        Ex: gcloud

    .PARAMETER SPList
        The list to generate the Uri for
        Ex: solutions.contoso.com

    .PARAMETER SPList
        The filter query to use in the Uri
        Ex: StatusValue eq 'New'
    
    .PARAMETER UseSSL
        Use ssl (https) or not
        Default is true

    .Example Site
        Format-SPUri -SPFarm 'solutions.contoso.com' `
                     -SPSite 'gcloud'
    
        https://solutions.contoso.com/Sites/gcloud/_vti_bin/listdata.svc

    .Example List
        Format-SPUri -SPFarm 'solutions.contoso.com' `
                     -SPSite 'gcloud' `
                     -SPList 'AddDiskToAVirtualServer'
        
        https://solutions.contoso.com/Sites/gcloud/_vti_bin/listdata.svc/AddDiskToAVirtualServer

    .Example List with Filter
        Format-SPUri -SPFarm   'solutions.contoso.com' `
                     -SPSite   'gcloud' `
                     -SPList   'AddDiskToAVirtualServer' `
                     -SPFilter 'StatusValue eq Failed'

        https://solutions.contoso.com/Sites/gcloud/_vti_bin/listdata.svc/AddDiskToAVirtualServer?$filter=StatusValue eq Failed

    .Example List with Filter and non SSL
        Format-SPUri -SPFarm   'solutions.contoso.com' `
                     -SPSite   'gcloud' `
                     -SPList   'AddDiskToAVirtualServer' `
                     -SPFilter 'StatusValue eq Failed' `
                     -UseSsl   $false

        http://solutions.contoso.com/Sites/gcloud/_vti_bin/listdata.svc/AddDiskToAVirtualServer?$filter=StatusValue eq Failed
#>
Function Format-SPUri
{
    Param ( [Parameter(Mandatory=$true) ][string] $SPFarm,
            [Parameter(Mandatory=$true) ][string] $SPSite,
            [Parameter(Mandatory=$false)][string] $SPList,
            [Parameter(Mandatory=$false)][string] $SPFilter,
            [Parameter(Mandatory=$false)][bool]   $UseSSl = $true
          )
    $null = $(
        if($UseSSl)
        {
            $SPUri = "https"
        }
        else
        {
            $SPUri = "http"
        }
        $SPUri = "$SPUri`://$SPFarm/Sites/$SPSite/_vti_bin/listdata.svc"
        if($SPList) 
        {
            $SPUri = "$SPUri/$($SPList)"
            if($SPFilter)
            {
                $SPUri = "$SPUri`?`$filter=$SPFilter"
            }
        }
    )
    return $SPUri
}
<#
    .SYNOPSIS
        Get all SharePoint list names for the given SharePoint site

    .OUTPUTS
        Arraylist of strings representing all list names

    .PARAMETER SPUri
        The full uri to the sharepoint site
        Ex: https://solutions.contoso.com/Sites/GCloud

    .PARAMETER SPFarm
        The name of the sharepoint farm to query
        Ex: solutions.contoso.com

    .PARAMETER SPSite
        The name of the sharepoint site to query
        Ex: gcloud

    .PARAMETER UseSSL
        Use ssl (https) or not
        Default is true

    .PARAMETER Credential
        Optional PSCredential to use when querying sharepoint

#>
Function Get-SPList
{

    Param( [Parameter(ParameterSetName="ExplicitURI", Mandatory=$True)][string]$SPUri,
           
           [Parameter(ParameterSetName="BuildURI", Mandatory=$true) ][string] $SPFarm,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$true) ][string] $SPSite,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$false)][bool]   $UseSSl = $true,
           
           [Parameter(Mandatory=$false)][PSCredential] $Credential )

    $null = $(
        if(-not $SPUri)
        {
            $SPUri = Format-SPUri -SPFarm $SPFarm -SPSite $SPSite
        }

        $returnLists = New-Object -TypeName System.Collections.ArrayList

        $ListService = Invoke-RestMethod-Wrapped -Uri $SPUri -Credential $Credential
        
        $Lists = $ListService.Service.Workspace.ChildNodes.Title
        Foreach($List in $Lists) { if($List) { $returnLists.Add($list) } }
    )
    return $returnLists
}
<#
    .SYNOPSIS
        Get SharePoint list item(s).

    .OUTPUTS
        Converts SharePoint list items to SPListItem objects

    .PARAMETER SPUri
        URI of the SharePoint list or list item or list item child item to query (optional)

    .PARAMETER SPFarm
        The name of the SharePoint farm to query. Used with SPSite, SPList and UseSSl to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER SPSite
        The name of the SharePoint site. Used with SPFarm, SPList and UseSSl to create SPUri
        Use this parameter set or specifiy SPUri directly
    
    .PARAMETER SPList
        The name of the SharePoint farm to query. Used with SPFarm, SPSite and UseSSl to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER UseSSl
        The name of the SharePoint site. Used with SPFarm, SPSite and SPList to create SPUri
        Use this parameter set or specifiy SPUri directly
        Default Value: True
        Action: Sets either a http or https prefix for the SPUri

    .PARAMETER Filter
        Filter definition

    .PARAMETER Credential
        Credential with rights to query SharePoint list. If not used default credentials will be used

    .EXAMPLE
        Get all list items from a SharePoint list

        Get-SPListItem -SPURI $SPListURI -Credential $SPCred

    .EXAMPLE
        Get all list items from a SharePoint list

        Get-SPListItem -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -Credential $SPCred

    .EXAMPLE
        Get all list items from a SharePoint list that match the specified filter
    
        Sample filters
            $SPFilter = "StageValue eq 'Complete'"
            $SPFilter = "StageValue ne 'Complete' and AssignedId ne HiddenAssignedId"
            $SPFilter = "Enabled and Status ne 'Launching'"  #  'Enabled' is a boolean column
            $DateString = $Date.ToString( "s" ) #  Dates in filters need to be in this format
            $SPFilter = "Status eq 'Pending' and StartTime lt datetime'$DateString'"
            $SPFilter = "substringof('Recycle',Title)"

        Get-SPListItem -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -Credential $SPCred -Filter $SPFilter

    .EXAMPLE
        Expand a linked property such as a linked user

        Get-SPListItem -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -ExpandProperty CreatedBy

    .EXAMPLE
        Expand all linked properties

        Get-SPListItem -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -ExpandProperty *
#>
Function Get-SPListItem
{
    Param( [Parameter(ParameterSetName="ExplicitURI", Mandatory=$True)][string]$SPUri,
           
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPFarm,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPSite,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPList,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$False)][bool]  $UseSsl = $True,
           
           [Parameter(Mandatory=$False)][string]       $Filter,
           [Parameter(Mandatory=$False)][string[]]     $ExpandProperty,
           [Parameter(Mandatory=$False)][PSCredential] $Credential )
    
    $null = $(
        if( -not $SPUri )
        {
            $SPUri = Format-SPUri -SPFarm $SPFarm `
								  -SPSite $SPSite `
	                              -SPList $SPList `
	                              -UseSSl $UseSsl
        }

        if ( $Filter ) { $SPUri += "?`$filter=$($Filter)" }
    
        #  Get the first page of items in the list (up to the SQL defined limit, usually 1000)
        $MoreItems = Invoke-RestMethod-Wrapped -Uri $SPUri -Credential $Credential
        $RawList = @()

        #  As long as we keep getting more items...
        while ( $MoreItems )
        {
            #  Add the items to the list
            $RawList += $MoreItems

            #  Get the next page of item in the list
            if($RawList[-1] -is [System.Xml.XmlElement]) 
            { 
                $LastID = $RawList[-1].Content.Properties.ID.'#text'
                if(-not [System.String]::IsNullOrEmpty($LastID))
                { 
                    If ( $Filter ) { $PageURI = "$SPUri&`$skiptoken=$LastID"  }
                    Else           { $PageURI = "$SPUri/?`$skiptoken=$LastID" }
            
                    $MoreItems = Invoke-RestMethod-Wrapped -Uri $PageURI -Credential $Credential
                }
                else
                {
                    break
                }
            }
            else
            {
                break
            }
        }
	
		$ReturnList = New-Object -TypeName 'System.Collections.Generic.List[SPListItem]'

        foreach( $ListItem in $RawList )
        {
			$SPListItem = Parse-RawSPItem -ListItem $ListItem -Immutable $False
			
			if($ExpandProperty)
			{
				if($ExpandProperty -contains '*')
				{
                    if($ListItem -is [System.Xml.XmlElement]) { $links = $ListItem.      Link }
                    else                                      { $links = $ListItem.Entry.Link }
                    foreach($link in $links)
                    {
                        if($link.rel -ne 'edit')
                        {
                            $LinkedItem = Get-SPListItemImmutable -SPUri "$($SPListItem.Id)/$($link.title)" -Credential $Credential
                            if(-not ($SPListItem.LinkedItems.ContainsKey($link.title)))
                            {
						        $SPListItem.addLinkedItem($link.title, $LinkedItem)
                            }
                        }
                    }
				}
				else
				{
					foreach($Property in $ExpandProperty)
					{
						$LinkedItem = Get-SPListItemImmutable -SPUri "$($SPListItem.Id)/$Property" -Credential $Credential
                        if(-not ($SPListItem.LinkedItems.ContainsKey($Property)))
                        {
						    $SPListItem.addLinkedItem($Property, $LinkedItem)
                        }
					}
				}
			}
            $ReturnList.Add($SPListItem)
        }
    )
    return $ReturnList
}
<#
    .SYNOPSIS
        Get SharePoint list items that are immutable.
        This function is an internal function used by Get-SPListItem to return immutable
        objects that are linked to the target object

    .OUTPUTS
        Converts SharePoint list items to SPListItemImmutable

    .PARAMETER SPUri
        URI of the SharePoint list item

    .PARAMETER Credential
        Credential with rights to query SharePoint list. If not used default credentials will be used
#>
Function Get-SPListItemImmutable
{
	Param( [Parameter(Mandatory=$True) ][string]$SPUri,
           [Parameter(Mandatory=$False)][PSCredential] $Credential )

	$null = $(
		$Item = Invoke-RestMethod-Wrapped -Uri $SPUri -Credential $Credential
        if($Item)
        {
		    $SPItemImmutable = Parse-RawSPItem -ListItem  $Item -Immutable $True
        }
	)
	return $SPItemImmutable
}
<#
    .SYNOPSIS
        Takes the raw output of Invoke-RestMethod and wrapps it into a custom class
        for SharePoint objects (either SPListItem or SPListItemImmutable depending on
        the value of Immutable flag)

    .OUTPUTS
        Converts the output of Invoke-RestMethod into SharePoint list items

    .PARAMETER ListItem
        The Xml output of Invoke-RestMethod for a SharePoint list item
    
    .PARAMETER Immutable
        A flag to determine if the outputed object will be immutable or not
#>
Function Parse-RawSPItem
{
	Param ( [Parameter(Mandatory=$True) ]       $ListItem,
		    [Parameter(Mandatory=$False)][bool] $Immutable = $False )
	$null = $(
		if ( $ListItem -is [System.Xml.XmlElement] ) { $Id = $ListItem.      id; $ListItemProperties = $ListItem.      Content.Properties }
		else                                         { $Id = $ListItem.Entry.id; $ListItemProperties = $ListItem.Entry.Content.Properties }
                
		$ListItemPropertyNames = ( $ListItemProperties | Get-Member -MemberType Property ).Name

		if($ListItemPropertyNames.Contains('Created')) {  }
		else { $ListItemCreated = [System.DateTime]::MinValue }

		if($ListItemPropertyNames.Contains('Modified')) { $ListItemModified = [DateTime]$ListItemProperties.Modified.'#text' }
		else { $ListItemModified = [System.DateTime]::MinValue }

		if($ListItemPropertyNames.Contains('Version')) { $ListItemVersion = $ListItemProperties.Version.'#text' }
		else { $ListItemVersion = [System.String]::Empty }

		$PropertyList = New-Object -TypeName 'System.Collections.Generic.Dictionary[string,object]'
		$ListItemCreated  = [System.DateTime]::MinValue
		$ListItemModified = [System.DateTime]::MinValue
		$ListItemVersion  = [System.String]::Empty

		foreach( $PropertyName in $ListItemPropertyNames )
		{
			$Property = $ListItemProperties."$PropertyName"
			if    ( $Property -is [string]    ) { $Value = [string]$Property }
			elseif( $Property.Null -eq "True" ) { $Value = [string]""        }
			else
			{
				switch -CaseSensitive ( $Property.Type )
				{
					"Edm.DateTime"       { $Value = [datetime]$Property.'#text' }
					"Edm.DateTimeOffset" { $Value = [datetime]$Property.'#text' }
					"Edm.Time"           { $Value = [timespan]$Property.'#text' }
					"Edm.Int16"          { $Value = [int16]   $Property.'#text' }
					"Edm.Int32"          { $Value = [int32]   $Property.'#text' }
					"Edm.Int64"          { $Value = [int64]   $Property.'#text' }
					"Edm.Decimal"        { $Value = [decimal] $Property.'#text' }
					"Edm.Float"          { $Value = [single]  $Property.'#text' }
					"Edm.Double"         { $Value = [double]  $Property.'#text' }
					"Edm.Boolean"        { $Value = [boolean]($Property.'#text' -eq "true") }
					"Edm.Byte"           { $Value = [byte]    $Property.'#text' }
					"Edm.SByte"          { $Value = [sbyte]   $Property.'#text' }
					"Edm.Guid"           { $Value = [guid]    $Property.'#text' }
					default              { $Value = [string]  $Property.'#text' }
				}
			}
   
			$LowerCasePropertyName = $PropertyName.ToLower()
			Switch -CaseSensitive ( $LowerCasePropertyName )
			{
				"created" 
				{
					$ListItemCreated = $Value   
				}
				"modified" 
				{
					$ListItemModified = $Value
				}
				"version" 
				{
					$ListItemVersion = $Value
				}
				default
				{
					$PropertyList.Add($PropertyName,$Value)
				}
			}
		}
		if(-not $Immutable)
		{
			$SPListItem = New-Object -TypeName 'SPListItem' -ArgumentList $Id,
																	      $ListItemCreated,
																	      $ListItemModified,
																	      $ListItemVersion,
																	      $PropertyList
		}
		else
		{
			$SPListItem = New-Object -TypeName 'SPListItemImmutable' -ArgumentList $Id,
																	               $ListItemCreated,
																	               $ListItemModified,
																	               $ListItemVersion,
																	               $PropertyList
		}
		
	)
	return $SPListItem
}

<#
    .SYNOPSIS
        Update SharePoint list item

    .OUTPUTS
        None

    .PARAMETER SPUri
        URI of the SharePoint list or list item or list item child item to update

    .PARAMETER SPFarm
        The name of the SharePoint farm to query. Used with SPSite, SPList, UseSSl and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER SPSite
        The name of the SharePoint site. Used with SPFarm, SPList, UseSSl and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly
    
    .PARAMETER SPList
        The name of the SharePoint farm to query. Used with SPFarm, SPSite, UseSSl and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER UseSSl
        The name of the SharePoint site. Used with SPFarm, SPSite, SPList and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly
        Default Value: True
        Action: Sets either a http or https prefix for the SPUri

    .PARAMETER SPListItemIndex
        Index of a specific SharePoint list item to update. Used with SPFarm, SPSite, SPList and UseSSl to create SPUri

    .PARAMETER Data
        A hashtable representing the data in the SharePoint list item to be updated.
        Each key must match an internal SharePoint column name or column reference.

    .PARAMETER SPListItem
        The SPList item with all modifications made to it to update
    
    .PARAMETER PassThru
        Return an updated version of the SPListItem object or not
        Defaults to False

    .PARAMETER Credential
        Credential with rights to update SharePoint list

    .EXAMPLE
        Update SharePoint list item

        $Data = @{ 'LastRun' = (Get-Date); 'Status' = $FinalStatus }
        Update-SPListItem -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -SPListItemIndex 7 -Data $Data -Credential $SPCred

    .EXAMPLE
        Update SharePoint list item

        $SPUri = "http://q.spis.contoso.com/sites/EApps/Self_Service/_vti_bin/listdata.svc/GroomingSchedule(3)"
        $Data = @{ 'LastRun' = (Get-Date); 'Status' = $FinalStatus }
        Update-SPListItem -SPUri $SPUri -Data $Data -Credential $SPCred

    .EXAMPLE
        Update SharePoint list items assigned to the given team with the new team name

        $SPFilter = "Team eq 'WE-Apps'"
        $ListItems = Get-SPListItem -SPFarm $SPFarm -SPSIte $SPSite -SPList $SPList -Filter $SPFilter -Credential $SPCred
        Foreach($ListItem in $ListItems)
        {
            $ListItem.Properties.Team = 'Web Hosting'
            Update-SPListItem -SPListItem $ListItem -Credential $SPCred
        }
#>
Function Update-SPListItem
{
    Param( [Parameter(ParameterSetName="ExplicitURI", Mandatory=$True)][string]$SPUri,
           
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPFarm,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPSite,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPList,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPListItemIndex,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$False)][bool]  $UseSsl = $True,
           
           [Parameter(ParameterSetName="BuildURI",    Mandatory=$True)]
           [Parameter(ParameterSetName="ExplicitURI", Mandatory=$True)][HashTable]$Data,

           [Parameter(ParameterSetName="SPListItem", Mandatory=$True)][object]$SPListItem,

           [Parameter(Mandatory=$False)][bool] $PassThru = $False,
           [Parameter(Mandatory=$False)][PSCredential] $Credential )

    $null = $(
        If($SPListItem)
        {
            # If a whole item is passed in compare all of the properties. If there are any that don't match
            # the current list item add them to the data hashtable for processing

            # Set SPUri to the item id

            $SPUri = $SPListitem.Id

            $CurrentSPListItem = Get-SPListItem -SPUri $SPUri -Credential $Credential
            $Data = @{}
        
            Foreach($PropertyKey in $SPListItem.Properties.Keys)
            {
                if($CurrentSPListItem.Properties."$($PropertyKey)" -ne $SPListItem.Properties."$($PropertyKey)")
                {
                    $Data.Add($PropertyKey, $SPListItem.Properties."$($PropertyKey)")
                }  
            }
        }
        ElseIf($SPFarm)
        {
            $SPUri = "$(Format-SPUri -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -UseSSl $UseSsl)($SPListItemIndex)"
        }
        ElseIf($SPUri)
        {
            # SPUri was passed no processing to do
        }
        Else
        {
            Write-Error -Message "You must pass one of the following parameter sets`n" + 
                                 "-SPUri `$SPUri -Data `$Data" +
                                 "-SPFarm `$SPFarm -SPSite `$SPSIte -SPList `$SPList -SPListItemIndex " +
                                 "`$SPListItemIndex -Data `$Data`n" +
                                 "-SPListItem `$SPListITem"
        }

        # Convert all datetime values in the Data hashtable to short strings
        $UpdateData = @{}

        ForEach($Key in $Data.Keys)
        {
            if($Data[$Key] -is [DateTime]) { $UpdateData.Add($Key, $Data[$Key].ToString("s")) }
            else { $UpdateData.Add($Key, $Data[$Key]) }
        }

        # Convert the Hashtable to a JSON format
        $RESTBody = ConvertTo-Json -InputObject $UpdateData

        # Update the item using merge
        $Invoke = Invoke-RestMethod-Wrapped -Method Merge `
                                            -Uri    $SPUri `
                                            -Body   $RESTBody `
                                            -ContentType "application/json" `
                                            -Headers @{ "If-Match"="*" } `
                                            -Credential $Credential

        if($PassThru) { $returnItem = Get-SPListItem -SPUri $SPUri -Credential $Credential }
    )
    if($PassThru) { return $returnItem }
}
<#
    .SYNOPSIS
    Add new SharePoint list item(s)

    .OUTPUTS
    None

    .PARAMETER SPUri
        URI of the SharePoint list (optional)

    .PARAMETER SPFarm
        The name of the SharePoint farm to query. Used with SPSite, SPList and UseSSl to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER SPSite
        The name of the SharePoint site. Used with SPFarm, SPList and UseSSl to create SPUri
        Use this parameter set or specifiy SPUri directly
    
    .PARAMETER SPList
        The name of the SharePoint farm to query. Used with SPFarm, SPSite and UseSSl to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER UseSSl
        The name of the SharePoint site. Used with SPFarm, SPSite and SPList to create SPUri
        Use this parameter set or specifiy SPUri directly
        Default Value: True
        Action: Sets either a http or https prefix for the SPUri

    .PARAMETER Data
        A hashtable representing the data in the SharePoint list item to be created.
        Each key must match an internal SharePoint column name or column reference.

    .PARAMETER Credential
        Credential with rights to update SharePoint list

    .EXAMPLE
        Add SharePoint list item.  Any unspecified columns take default values (if any).

        $Data = @{ 'LastRun' = (Get-Date); 'Status' = $FinalStatus }
        Add-SPListItem -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -Data $Data
#>
Function Add-SPListItem
{
    Param( [Parameter(ParameterSetName="ExplicitURI", Mandatory=$True)][string]$SPUri,
           
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPFarm,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPSite,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPList,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$False)][bool]  $UseSsl = $True,
           
           [Parameter(Mandatory=$True) ][HashTable]    $Data,
           [Parameter(Mandatory=$False)][bool]         $PassThru = $False,
           [Parameter(Mandatory=$False)][PSCredential] $Credential )

    if(-not $SPUri)
    {
        $SPUri = Format-SPUri -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -UseSSl $UseSsl
    }
    $UpdateData = @{}

    ForEach($Key in $Data.Keys)
    {
        if($Data[$Key] -is [DateTime]) { $UpdateData.Add($Key, $Data[$Key].ToString("s")) }
        else { $UpdateData.Add($Key, $Data[$Key]) }
    }

    # Convert the Hashtable to a JSON format
    $RESTBody = ConvertTo-Json -InputObject $UpdateData

    $Invoke = Invoke-RestMethod-Wrapped -Method      Post `
                                        -URI         $SPUri `
                                        -Body        $RESTBody `
                                        -ContentType "application/json" `
                                        -Credential  $Credential
}
<#
    .SYNOPSIS
        Delete target SharePoint list item

    .OUTPUTS
        None

    .PARAMETER SPUri
        URI of the SharePoint list item to delete

    .PARAMETER SPFarm
        The name of the SharePoint farm to delete. Used with SPSite, SPList, UseSSl and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER SPSite
        The name of the SharePoint site. Used with SPFarm, SPList, UseSSl and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly
    
    .PARAMETER SPList
        The name of the SharePoint farm to query. Used with SPFarm, SPSite, UseSSl and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly

    .PARAMETER UseSSl
        The name of the SharePoint site. Used with SPFarm, SPSite, SPList and SPListItemIndex to create SPUri
        Use this parameter set or specifiy SPUri directly
        Default Value: True
        Action: Sets either a http or https prefix for the SPUri

    .PARAMETER SPListItemIndex
        Index of a specific SharePoint list item to delete

    .PARAMETER SPListItem
        The SPList item with all modifications made to it to delete

    .PARAMETER Credential
        Credential with rights to the SharePoint list

    .EXAMPLE
    Delete SharePoint list item
#>
Function Delete-SPListItem
{
    Param( [Parameter(ParameterSetName="ExplicitURI", Mandatory=$True)][string]$SPUri,
           
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPFarm,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPSite,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPList,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$True) ][string]$SPListItemIndex,
           [Parameter(ParameterSetName="BuildURI", Mandatory=$False)][bool]  $UseSsl = $True,
           
           [Parameter(ParameterSetName="SPListItem", Mandatory=$True)][object]$SPListItem,
           
           [Parameter(Mandatory=$False)][PSCredential] $Credential  )

    $null = $(
        If($SPListItem)
        {
            # If a whole item is passed in compare delete it based on its Id

            $SPUri = $SPListitem.Id
        }
        ElseIf($SPFarm)
        {
            # If the BuildURI Parameter set is passed created the correct SPUri
            $SPUri = "$(Format-SPUri -SPFarm $SPFarm -SPSite $SPSite -SPList $SPList -UseSSl $UseSsl)($SPListItemIndex)"
        }
        ElseIf($SPUri)
        {
            # SPUri was passed no processing to do
        }
        Else
        {
            Write-Error -Message "You must pass one of the following parameter sets`n" + 
                                 "-SPUri `$SPUri" +
                                 "-SPFarm `$SPFarm -SPSite `$SPSIte -SPList `$SPList -SPListItemIndex " +
                                 "`$SPListItemIndex`n" +
                                 "-SPListItem `$SPListItem"
        }
        $Invoke = Invoke-RestMethod-Wrapped -Method      Delete `
                                            -URI         $SPUri `
                                            -ContentType "application/json" `
                                            -Credential  $Credential
    )
}