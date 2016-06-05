using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuntherTesterRunner
{
    class MainConsole
    {
        [Import(typeof(IRun))]
        public IRun runner;

        private CompositionContainer _container;
        private void InitMef()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(@"."));
            _container = new CompositionContainer(catalog);

            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        static void Main(string[] args)
        {
            var mainRunner = new MainConsole();
            mainRunner.InitMef();

            Console.WriteLine("Hi, I am Gunther. Available options -> [list, filter, run, exit, quit]");

            while (true)
            {
                Console.Write("Gunther -> ");
                var input = Console.ReadLine();
                var inputSplited = input.Split(new char[] { ' ' });
                switch (inputSplited[0])
                {
                    case "list":
                        Console.WriteLine(mainRunner.runner.Run("list"));
                        break;
                    case "filter":
                        if (inputSplited.Length > 1)
                            Console.WriteLine(mainRunner.runner.Run(string.Join(" ", input)));
                        else
                            Console.WriteLine("Please provide tests name to run");
                        break;
                    case "run":
                        if (inputSplited.Length > 1)
                            Console.WriteLine(mainRunner.runner.Run(string.Join(" ", input)));
                        else
                            Console.WriteLine(mainRunner.runner.Run("run"));
                        break;
                    case "quit":
                    case "exit":
                        return;
                }
            }
        }
    }
}
