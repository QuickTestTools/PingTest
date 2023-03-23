using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.XPath;
using Quick.Build;

var productDir = "PingTest";

//准备目录变量
var appFolder = QbFolder.GetAppFolder();
if (appFolder == Environment.CurrentDirectory)
    Environment.CurrentDirectory = Path.GetFullPath("../../../../../");
var baseFolder = Environment.CurrentDirectory;
var outFolder = Path.GetFullPath("bin");
if (!Directory.Exists(outFolder))
    Directory.CreateDirectory(outFolder);
var publishFolder = $"bin";
var version = XDocument.Load($"src/{productDir}/{productDir}.csproj")
    .XPathSelectElement("Project/PropertyGroup/Version")
    .Value;
Console.WriteLine("Delete folder: Release...");
//Delete folder: Release
QbFolder.DeleteFolders("src", "Release", SearchOption.AllDirectories);

Console.WriteLine("dotnet build...");
QbCommand.Run("dotnet", $"publish -c Release src/{productDir}");
//Delete folder: obj
QbFolder.DeleteFolders($"src/{productDir}", "obj", SearchOption.AllDirectories);

var osList = new[] { "windows", "linux" };
var archList = new[] { "x64", "arm64" };

foreach (var os in osList)
    foreach (var arch in archList)
    {
        var fileExtension = string.Empty;
        if (os == "windows")
            fileExtension = ".exe";
        QbCommand.Run(
            "bflat",
            $"build --os={os} --arch={arch} --no-debug-info --reference ./bin/Release/Quick.Localize.dll",
            $"src/{productDir}"
            );
        var srcFile = $"src/{productDir}/{productDir}{fileExtension}";
        var desFile = $"{publishFolder}/{productDir}-{version}-{os}-{arch}{fileExtension}";
        QbFile.Delete(desFile);
        QbFile.Copy(srcFile, desFile);
        QbFile.Delete(srcFile);
    }

Console.WriteLine("Done");
//如果是在Windows平台，则打开窗口
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    try { QbCommand.Run("Explorer", @"bin"); }
    catch { }
}