using GuntherTesterRunner;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace MSpecExtensionRunner
{
    public class Extensions
    {
        [Export(typeof(IOperation))]
        [ExportMetadata("Symbol", "list")]
        public class list : IOperation
        {
            public string Operate(string command)
            {
                try
                {
                    return File.ReadAllText("Tests.txt");
                }
                catch (FileNotFoundException err)
                {
                    return err.Message;
                }
            }
        }

        [Export(typeof(IOperation))]
        [ExportMetadata("Symbol", "run")]
        public class run : IOperation
        {
            public string Operate(string command)
            {
                string output = "error";
                ProcessStartInfo processInfo = new ProcessStartInfo(@"MSpecConsole\mspec-clr4.exe");
                processInfo.CreateNoWindow = false;
                processInfo.UseShellExecute = false;
                processInfo.FileName = Framework.mspec;
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.Arguments = "ClusterManagerTests.dll";
                processInfo.RedirectStandardOutput = true;
                try
                {
                    using (Process process = Process.Start(processInfo))
                    {
                        process.WaitForExit();
                        output = process.StandardOutput.ReadToEnd();
                    }
                }
                catch (Exception err)
                {
                    return err.Message;
                }
                return output;
            }
        }

        [Export(typeof(IOperation))]
        [ExportMetadata("Symbol", "filter")]
        public class filter : IOperation
        {
            public string Operate(string command)
            {
                string output = "error";
                ProcessStartInfo processInfo = new ProcessStartInfo(@"MSpecConsole\mspec-clr4.exe");
                processInfo.CreateNoWindow = false;
                processInfo.UseShellExecute = false;
                processInfo.FileName = Framework.mspec;
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                string arguments = command.Remove(0, "filter ".Length).Replace(' ', ',');
                arguments = "-i \"" + arguments + "\"";
                processInfo.Arguments = arguments + " ClusterManagerTests.dll";
                processInfo.RedirectStandardOutput = true;
                try
                {
                    using (Process process = Process.Start(processInfo))
                    {
                        process.WaitForExit();
                        output = process.StandardOutput.ReadToEnd();
                    }
                }
                catch (Exception err)
                {
                    return err.Message;
                }
                return output;
            }
        }

    }
}
