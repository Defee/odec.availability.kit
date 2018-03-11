$major = $(major);
$minor = $(minor);
$patch = $(patch);
$isVersioned = $(isVersioned);
$isBetta = $(isBetta);
$beta = "beta$(build.buildNumber)";
Host-Write $major'.'$minor'.'$patch'-'$beta
if($isBetta)
{
    If ($isVersioned) 
    {

        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
             dotnet build $path.FullName /p:VersionPrefix=$major'.'$minor'.'$patch /p:VersionSuffix=$beta;
        }
    }
    else
    { 
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
             dotnet build $path.FullName /p:VersionSuffix=$beta;
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
            dotnet build $path.FullName /p:VersionPrefix=$major'.'$minor'.'$patch;
        }
    }
    else
    {
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
            dotnet build $path.FullName;
        }
    }
}