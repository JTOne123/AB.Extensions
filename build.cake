///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var isLocalBuild        = !AppVeyor.IsRunningOnAppVeyor;
var packPath            = Directory("./src/AB.Extensions");
var sourcePath          = Directory("./src");
var testsPath           = Directory("test");
var buildArtifacts      = Directory("./artifacts/packages");
var configuration   = Argument<string>("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .IsDependentOn("Clean")
    //.IsDependentOn("Version")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(project.GetDirectory().FullPath, settings);
    });

Task("Clean")
    .Does(() => 
	{
        if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }
        CreateDirectory(outputDir);
    });

Task("Restore")
    .Does(() => 
	{
        DotNetCoreRestore("src");
    });

	Task("Restore")
    .Does(() =>
	{
	    var settings = new DotNetCoreRestoreSettings
	    {
	        Sources = new [] { "https://api.nuget.org/v3/index.json" }
	    };
	
	    DotNetCoreRestore(sourcePath, settings);
	    DotNetCoreRestore(testsPath, settings);
	});