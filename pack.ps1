$major = $(major);
$minor = $(minor);
$patch = $(rev:.r);
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
            dotnet pack $path.FullName --no-build /p:VersionPrefix=$major'.'$minor'.'$patch /p:VersionSuffix=$beta;
        }
    }
    else
    { 
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
             dotnet pack $path.FullName --no-build /p:VersionSuffix=$beta;
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
            dotnet pack $path.FullName --no-build /p:VersionPrefix=$major'.'$minor'.'$patch;
        }
    }
    else
    {
        ForEach ($path in (Get-Childitem **/**.csproj -Recurse)) 
        {
            $path.FullName;
            dotnet pack $path.FullName --no-build;
        }
    }
}