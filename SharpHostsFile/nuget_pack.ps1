$path = $env:APPVEYOR_BUILD_FOLDER + "\SharpHostsFile\SharpHostsFile.nuspec"
nuget pack $path -Version $env:APPVEYOR_BUILD_VERSION