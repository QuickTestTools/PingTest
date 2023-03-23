using System.Net.NetworkInformation;
using System.Reflection;

var title = "Ping测试工具";
var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
Console.Title = title;
Console.WriteLine($"欢迎使用{title}_v{version}");
Console.WriteLine("仓库地址：https://github.com/QuickTestTools/PingTest");
Console.Write("请输入IP地址：");
var ipAddress = Console.ReadLine();
Console.Title = $"{ipAddress} - {title}";
var ping = new Ping();
bool? success = null;
DateTime? lastChangeTime = null;

Action<PingReply> setResult = t =>
{
    var currentIsSuccess = t.Status == IPStatus.Success;
    var isChanged = success == null || success.Value != currentIsSuccess;
    success = currentIsSuccess;

    var currentTime = DateTime.Now;
    var currentTimeStr = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
    
    string result;
    if (currentIsSuccess)
    {
        result = $"成功,时间={t.RoundtripTime}ms,TTL={t.Options.Ttl}";
    }
    else
    {
        result = $"失败，原因：{t.Status}";
    }
    Console.CursorLeft = 0;
    Console.Write(string.Empty.PadRight(Console.BufferWidth));
    Console.CursorLeft = 0;
    if (isChanged)
    {
        var duration = string.Empty;
        if (lastChangeTime != null)
        {
            duration = $",变化用时={currentTime - lastChangeTime.Value}";
        }
        lastChangeTime = currentTime;
        Console.WriteLine($"{currentTimeStr}: {result}{duration}");
    }
    else
    {
        Console.Write($"[{currentTimeStr}: {result}]");
    }
};
Console.WriteLine("[按Ctrl+C终止程序]");
while (true)
{
    Thread.Sleep(1000);
    try
    {
        var rep = ping.Send(ipAddress);
        setResult(rep);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"测试错误，原因：{ex}");
    }
}
