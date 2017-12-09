using System.Runtime.Diagnostics;
using System.Diagnostics;

var name = "DotNetGet";

Task("Publish").Does(() => {
    CleanDirectory("publish");
    DotNetCorePublish($"src/{name}/{name}.fsproj", new DotNetCorePublishSettings {
        OutputDirectory = $"./publish/{name}",
        Configuration = "Release"
    });
});

Task("Zip")
    .IsDependentOn("Publish")
    .Does(() => {
        Zip($"publish/{name}", $"publish/dotnet-get.0.1.0.zip");
    });


var target = Argument("target", "default");
RunTarget(target);
