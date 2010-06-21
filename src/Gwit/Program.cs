using System;
using System.Diagnostics;

namespace Gwit
{
    class Program
    {
        static void Main(string[] args)
        {
            var processInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0\WebDev.WebServer40.EXE")
            { 
                Arguments = @"/port:3361 /nodirlist /path:""c:\dev\Projects\My\gwit\src\Gwit.Web"" /vpath:""/""",
            };
            Process.Start(processInfo);



            var browserProcessInfo = new ProcessStartInfo(@"http://localhost:3361/repo?location=" + Environment.CurrentDirectory);
            Process.Start(browserProcessInfo);

        }
    }
}
