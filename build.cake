#tool nuget:?package=MSBuild.SonarQube.Runner.Tool
#tool nuget:?package=JetBrains.ReSharper.CommandLineTools

#addin nuget:?package=Cake.Sonar

var target = Argument("target", "Default");
var solutionFile = "./src/Demo.Web.sln";
var resharperReport = "resharper-report.xml";
var sonarQubeReport = "sonarqube-report.json";

Task("Build")
  .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = "Release"
    };

    DotNetCoreBuild(solutionFile, settings);
});

Task("ReSharperInspect")
  .IsDependentOn("Build")
  .Does(() =>
{
    InspectCode(solutionFile, new InspectCodeSettings {
         SolutionWideAnalysis = true,
         ArgumentCustomization = args => args.Append("-s=INFO"),
         OutputFile = File(resharperReport)
    });
});

Task("ConverReSharperToSonar")
  .IsDependentOn("ReSharperInspect")
  .Does(() =>
{
    StartProcess("dotnet-reqube", $"-i {resharperReport} -o {sonarQubeReport} -d {Directory("./src/")}");
});

Task("SonarBegin")
  .Does(() => {
     SonarBegin(new SonarBeginSettings {
        Url = "https://sonarcloud.io",
        Login = EnvironmentVariable("sonar:apikey"),
        Key = "resharper-to-sonarqube-example",
        Name = "ReSharper to SonarQube example",
        ArgumentCustomization = args => args
            .Append("/o:olsh-github")
            .Append($"/d:sonar.externalIssuesReportPaths={sonarQubeReport}"),
        Version = "1.0.0.0"
     });
  });

Task("SonarEnd")
  .IsDependentOn("ConverReSharperToSonar")
  .Does(() => {
     SonarEnd(new SonarEndSettings {
        Login = EnvironmentVariable("sonar:apikey")
     });
     

  });

Setup(context =>
{
        CleanTempFiles();
});

Teardown(context =>
{
    CleanTempFiles();
});

// We need to clean up these temp files for .NET Core project types; otherwise they will be analyzed by SonarQube and R#
// For legacy project types this step is not necessary
void CleanTempFiles()
{
     DeleteFiles($"./src/**/{sonarQubeReport}");
     DeleteFiles($"./**/{resharperReport}");
}

Task("Default")
  .IsDependentOn("SonarBegin")
  .IsDependentOn("SonarEnd");

RunTarget(target);
