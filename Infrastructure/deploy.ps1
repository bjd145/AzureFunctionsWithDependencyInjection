param (
	[Parameter(Mandatory=$true)]
    [string]       $ResourceGroupName,
    [string]       $ResourceLocation           = "southcentralus"
)  

$user = Get-AzADUser -DisplayName "Brian Denicola"
$opts = @{
    Name                  = ("Deployment-{0}-{1}" -f $ResourceGroupName, $(Get-Date).ToString("yyyyMMddhhmmss"))
    ResourceGroupName     = $ResourceGroupName
    TemplateFile          = (Join-Path -Path $PWD.Path -ChildPath "azuredeploy.json")
    TemplateParameterFile = (Join-Path -Path $PWD.Path -ChildPath "azuredeploy.parameters.json")
}

New-AzResourceGroup -Name $ResourceGroupName -Location $ResourceLocation -Verbose
New-AzResourceGroupDeployment @opts -objectid $user.id -verbose   