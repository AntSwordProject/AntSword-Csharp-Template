using System;
using System.IO;
using System.Web;
using System.Web.UI;

public class Program
{
    public static void Main(string[] args)
    {
        // var run1 = new Run1();
        // var fileTree = run1.FileTree("d:/");
        // Console.WriteLine(fileTree);

        var executeCmd = new Exec().ExecuteCmd("cmd.exe", "whoami","");
        Console.WriteLine(executeCmd);
    }
}