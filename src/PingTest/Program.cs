using PingTest;
using Quick.Localize;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;

var currentCultureName = Thread.CurrentThread.CurrentCulture?.Name;
if (currentCultureName == null)
    currentCultureName = "en-US";

var textManager = TextManager.GetInstance(currentCultureName);
var title = textManager.GetText(Texts.Title);
var version = "1.0.1";
Console.Title = title;
Console.WriteLine(textManager.GetText(Texts.WelcomeText, title, version));
Console.WriteLine(textManager.GetText(Texts.RepoUrl, "https://github.com/QuickTestTools/PingTest"));
var dateFormat = textManager.GetText(Texts.DateFormat);
string ipAddress = null;
while (string.IsNullOrEmpty(ipAddress))
{
    Console.Write(textManager.GetText(Texts.PleaseInputIpAddress));
    var line = Console.ReadLine();
    if (IPAddress.TryParse(line, out _))
        ipAddress = line;
}

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
    var currentTimeStr = currentTime.ToString(dateFormat);
    
    string result;
    if (currentIsSuccess)
    {
        long? roundtripTime = null;
        if (t.RoundtripTime > 0)
            roundtripTime = t.RoundtripTime;
        result = textManager.GetText(Texts.SuccessResult, roundtripTime, t.Options?.Ttl);
    }
    else
    {
        result = textManager.GetText(Texts.FailedResult, t.Status);
    }
    var cursorTop = Console.CursorTop;
    Console.SetCursorPosition(0,cursorTop);
    Console.Write(string.Empty.PadRight(Console.BufferWidth));
    Console.SetCursorPosition(0, cursorTop);
    if (isChanged)
    {
        var duration = string.Empty;
        if (lastChangeTime != null)
        {
            duration = textManager.GetText(Texts.ChangeDuration, currentTime - lastChangeTime.Value);
        }
        lastChangeTime = currentTime;
        Console.WriteLine($"{currentTimeStr}: {result}{duration}");
    }
    else
    {
        Console.Write($"[{currentTimeStr}: {result}]");
    }
};
Console.WriteLine(textManager.GetText(Texts.ExitProgramTip));
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
        Console.WriteLine(ex.ToString());
        return -1;
    }
}
