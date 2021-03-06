$major =$env:major;
$minor = $env:minor;
$patch = $Env:BUILD_BUILDNUMBER;
$isVersioned = $env:isVersioned;
$isBetta = $env:isBetta;
$beta = "beta$(build.buildNumber)";
Host-Write $major'.'$minor'.'$patch'-'$beta
if($isBetta)
{
    If ($isVersioned) 
    {

        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
             dotnet restore $path.FullName /p:VersionPrefix=$major'.'$minor'.'$patch /p:VersionSuffix=$beta;
        }
    }
    else
    { 
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
            dotnet restore $path.FullName /p:VersionSuffix=$beta;
        }
    }
}
else
{
    If ($isVersioned) 
    {
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
            dotnet restore $path.FullName /p:VersionPrefix=$major'.'$minor'.'$patch;
        }
    }
    else
    {
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
            dotnet restore $path.FullName;
        }
    }
}