$path = $env:APPVEYOR_BUILD_FOLDER + "\SharpHostsFile.nuspec"
nuget pack $path -Version $env:APPVEYOR_BUILD_VERSION