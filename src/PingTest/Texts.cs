using Quick.Localize;

namespace PingTest
{
    [TextResource]
    internal enum Texts
    {
        [Text("en-US", "MM/dd/yyyy HH:mm:ss")]
        [Text("zh-CN", "yyyy-MM-dd HH:mm:ss")]
        DateFormat,
        [Text("en-US", "Ping Test Tool")]
        [Text("zh-CN", "Ping测试工具")]
        Title,
        [Text("en-US", "Welcome to use {0}_v{1}")]
        [Text("zh-CN", "欢迎使用{0}_v{1}")]
        WelcomeText,
        [Text("en-US", "Repo Url:{0}")]
        [Text("zh-CN", "仓库地址:{0}")]
        RepoUrl,
        [Text("en-US", "Please input IPAddress:")]
        [Text("zh-CN", "请输入IP地址：")]
        PleaseInputIpAddress,
        [Text("en-US", "[Press Ctrl+C to exit]")]
        [Text("zh-CN", "[按Ctrl+C终止程序]")]
        ExitProgramTip,
        [Text("en-US", "Success,Time={0}ms,TTL={1}")]
        [Text("zh-CN", "成功,时间={0}ms,TTL={1}")]
        SuccessResult,
        [Text("en-US", "Failed,Reason：{0}")]
        [Text("zh-CN", "失败,原因：{0}")]
        FailedResult,
        [Text("en-US", ",ChangeDuration={0}")]
        [Text("zh-CN", ",变化用时={0}")]
        ChangeDuration
    }
}
