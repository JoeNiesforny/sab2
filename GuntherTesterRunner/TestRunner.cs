using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuntherTesterRunner
{
    static public class Framework
    {
        public static string mspec = @"MSpecConsole\mspec-clr4.exe";
    }

    [Export(typeof(IRun))]
    public class TestRun : IRun
    {
        [ImportMany]
        private IEnumerable<Lazy<IOperation, IOperationData>> _operations;

        public string Run(string input)
        {
            var inputSplit = input.Split(' ');

            foreach (Lazy<IOperation, IOperationData> i in _operations)
            {
                if (i.Metadata.Symbol.Equals(inputSplit[0]))
                    return i.Value.Operate(input).ToString();
            }
            return "Nierozpoznano operacji";
        }
    }

    //[Export(typeof(IRun))]
    //public class RunAll : IRun
    //{
    //    public string Run(string input)
    //    {
    //        Console.WriteLine("Running all");
    //        return "Successed running all tests";
    //    }
    //}

    public interface IRun
    {
        string Run(string input);
    }

    public interface IOperation
    {
        string Operate(string command);
    }

    public interface IOperationData
    {
        string Symbol { get; }
    }
}
