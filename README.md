# CherryAya-CommandRoute
### 简易的C#指令路由
### ~~本人给自己造的屎山轮子~~ 应该没人用吧

## 使用说明

>### .NET 6

### 声明指令
1. 新建类 作为指令声明类（ 推荐命名 `XxxCommand` ）
2. 引用 `CherryAya_CommandRoute.Entities`
3. 实现 `ICommand` 接口
4. 属性 `Name` 赋值为 指令名 （ string ）
5. 属性 `Description` 赋值为 指令描述 （ string? ）
6. 属性 `Structure` 赋值为 根指令的 指令结构实例 （ ICommandStructure ）<br> 指令结构详见下条

### 声明指令结构
1. 新建类 作为指令结构声明类（ 推荐命名 `XxxCommandStructure` ）<br> 或 <br> 指令声明类 同级建立新类 作为指令结构声明类
2. 引用 `CherryAya_CommandRoute.Entities`
3. 实现 `ICommandStructure` 接口
4. 属性 `Key` 赋值为 指令键 （ string ）
5. 如有子指令 属性 `Options` 赋值为 子指令的 指令结构实例 （ List\<ICommandStructure>\? ）<br> 反之 赋值为 `null`
6. 如该级指令键有值 属性 `hasValue` 赋值为 `true` （ bool ）<br> 反之 赋值为`false`
7. 属性 `Value` 赋值为 `null` （ object? ）
8. 在方法 `Handle` 中处理该级指令触发的业务逻辑

### 自定义指令路由配置
指令路由默认使用配置为 <br>  `.Entities.impl.defaultRouteConfiguration` （ internal ） <br> 本节介绍自定义指令路由配置
1. 新建类 作为自定义指令路由配置类 ( 推荐命名 `RouteConfiguration` )
2. 实现 `IRouteConfiguration` 接口
3. 属性 `CommandPrefix` 赋值为 指令匹配前缀 （ string[] ）<br> 注意: 每个指令匹配前缀都不应超过一个字符
4. 属性 `CommandSplit` 赋值为 消息段分割符号（ string ） <br> 推荐符号为空格 不推荐超过一个字符
5. 如需指令匹配严格大小写 属性 `IsCaseSensitive` 赋值为 `true` （ bool ）<br> 反之 赋值为 `false`
6. 在 指令路由 的 有参构造方法 使用 本自定义配置 详见下条

### 实例化指令路由
引用 `CherryAya_CommandRoute`
+ 无参构造方法 <br>
        `CommandRoute route = new();`
+ 有参构造方法 根据上节有自定义指令路由配置类`RouteConfiguration` <br>
        `CommandRoute route = new(new RouteConfiguration());`

### 注册指令
根据上节 有指令路由实例 `route` 和指令声明 `XxxCommand` <br>
使用指令组方法 `Register` 注册指令 <br>
`route.Register(new XxxCommand());`

### 执行路由分发
设已注册指令 `/ping` 并有消息 `/ping` 被业务逻辑存入变量 `message`（ string ） <br>
使用路由方法 `Execute` 执行分发 <br>
`route.Execute(message)` <br>
返回结果为布尔值 <br> `true` 为匹配成功并执行Handle方法 <br> `false` 为匹配失败

<hr>

### Demo
> 引用
````csharp
using CherryAya_CommandRoute.Entities;
````
> 指令声明
````csharp
public class TestCommand : ICommand
{
        public string Name { get; set; } = "Test";
        public string? Description { get; set; } = null;
        public ICommandStructure Structure { get; set; } = new TestCommandStructure();
}
````
> 指令结构
````csharp
public class TestCommandStructure : ICommandStructure
{
        public string Key { get; set; } = "test";
        public bool hasValue { get; set; } = true;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = new List<ICommandStructure>()
        {
                new OptionA()
        };

        public void Handle()
        {
                Console.WriteLine(Value.ToString());
        }
}

public class OptionA : ICommandStructure
{
        public string Key { get; set; } = "optionA";
        public bool hasValue { get; set; } = false;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = null;

        public void Handle()
        {
                Console.WriteLine("location: Test>OptionA");
        }
}
````
> 注册分发
````csharp
using CherryAya_CommandRoute;

CommandRoute route = new();
route.Register(new TestCommand());

route.Execute("/test hello");           // 执行到 TestCommandStructure.Handle()
route.Execute("/test optionA");      // 执行到 TestCommandStructure.Options[0].Handle()
````