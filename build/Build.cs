using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.IO.FtpTasks;
using System.Net;
using FluentFTP;
using System.IO;
using Microsoft.Build.Tasks;
using System.Text;
using Nuke.Common.Tools.GitHub;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
[GitHubActions(
    "continuous",
    GitHubActionsImage.WindowsLatest,
    OnPushBranchesIgnore = new[] { MasterBranch, ReleaseBranchPrefix + "/*" },
    OnPullRequestBranches = new[] { DevelopBranch },
    PublishArtifacts = false,
    InvokedTargets = new[] { nameof(Publish) },
     ImportSecrets =
        new[]
        {
            nameof(FTPSERVER),
            nameof(FTPUSER),
            nameof(FTPPWD)
        })]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    const string MasterBranch = "master";
    const string DevelopBranch = "develop";
    const string ReleaseBranchPrefix = "release";
    const string HotfixBranchPrefix = "hotfix";

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter] readonly string FTPSERVER;
    [Parameter] readonly string FTPUSER;
    [Parameter] readonly string FTPPWD;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;
    [CI] readonly GitHubActions GitHubActions;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Clean)
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
        });

    Target Publish => _ => _
    .DependsOn(Compile)
    .Produces(ArtifactsDirectory)
    .OnlyWhenDynamic(() => GitRepository.IsOnDevelopBranch() || GitRepository.IsOnReleaseBranch())
    .Executes(() =>
    {
        DotNetPublish(s => s
        .SetProject(Solution.GetProject("Traccaradora.Web"))
        .SetAssemblyVersion(GitVersion.AssemblySemVer)
        .SetFileVersion(GitVersion.AssemblySemFileVer)
        .SetInformationalVersion(GitVersion.InformationalVersion)
        .SetSelfContained(true)
        .SetRuntime("browser-wasm")
        .SetFramework("net5.0")
        .SetProperty("PublishTrimmed", "true")
        .SetConfiguration(Configuration.Release)
        .SetOutput(ArtifactsDirectory)
        );
    });

    Target UploadDev => _ => _
        .DependsOn(Publish)
        .TriggeredBy(Publish)
        .Consumes(Publish)
        .OnlyWhenDynamic(() => GitRepository.IsOnDevelopBranch())
        .Executes(() =>
        {
            using FtpClient client = new FtpClient(FTPSERVER, FTPUSER, FTPPWD);
            client.ValidateAnyCertificate = true;
            client.Port = 21;
            client.Connect();
            client.SetWorkingDirectory("/dev-traccaradora");
            if (client.DirectoryExists("wwwroot"))
                client.DeleteDirectory("wwwroot", FtpListOption.AllFiles);
            if (client.FileExists("web.config"))
                client.DeleteFile("web.config");

            DeleteFile(ArtifactsDirectory / "web.config");
            var projDir = (AbsolutePath)Path.GetDirectoryName(Solution.GetProject("Traccaradora.Web").Path);
            CopyFile(projDir / "web.config.deploy", ArtifactsDirectory / "web.config");

            client.UploadFile(ArtifactsDirectory / "web.config", "web.config");
            client.UploadDirectory(ArtifactsDirectory / "wwwroot", "wwwroot");
                        
        });


}
