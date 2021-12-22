using CherryAya_CommandRoute;
using CherryAya_CommandRoute.Entities;
using CherryAya_CommandRoute_Test.Commands;

CommandRoute route = new(new RouteConfiguration());
route.Register(new HelloCommand());
route.Register(new TestCommand());
route.Register(new RandomCommand());
route.Register(new OptionsCommand());

// 大小写匹配测试
route.Execute("/Hello");
route.Execute("/hello");
// 多前缀匹配测试
route.Execute("#hello");
// 指令值匹配测试
route.Execute("/test 进行一个试的测");
route.Execute("/random 10");
// 多子指令匹配测试
route.Execute("/options optionA A");
route.Execute("/options optionB B");
// 套娃 Option权重大于Value
route.Execute("/options optionB optionA 套娃");
// Option/Value缺失
route.Execute("/options optionA");

// 查询/注销指令
Console.WriteLine(route.Contains(new HelloCommand()));
route.Deregister(new HelloCommand());
route.Execute("/Hello");

// Route ToString方法
Console.WriteLine(route.ToString());

// 清空
route.Clear();
route.Execute("/Hello");
Console.WriteLine(route.ToString());

public class RouteConfiguration : IRouteConfiguration
{
    public string[] CommandPrefix { get; set; } = new string[] { "/", "#" };
    public string CommandSplit { get; set; } = " ";
    public bool IsCaseSensitive { get; set; } = false;
}
